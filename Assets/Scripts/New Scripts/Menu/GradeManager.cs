using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class GradeManager : MonoBehaviour {
    const int semesterTotal = 8;
    [Header("Importants")]
    public static string gradeDataSave = "gradeSave";
    public static string finalTestDataSave = "finalTestSave";

	[HideInInspector]
    public float[,] grade = new float[semesterTotal,3];
    [HideInInspector]
    public float[] finalTest = new float[semesterTotal];


    [Header("UI Objects")]
    public LevelManager levelManager;
    public CanvasGroup menu;

    public Text charName;
    public Text charSemester;
    public Text crSoFar;

    public Transform classGrid;
    public GameObject charPanel;

    public List<GameObject> classBox;
    public GameObject finalTestBox;

    private string errorGrade;

    private void Start()
    {
        //BUILD ERRORGRADE
        InitializeGrade();
        grade[0, 0] = 4.3f;
        grade[0, 1] = 5.3f;
        //grade[0, 2] = 1.1f;
        errorGrade = JsonUtility.ToJson(grade);
        
        //INIT
        grade = GradeManager.LoadGrade();
        finalTest = GradeManager.LoadFinalTest();

        StartUiObjects();
    }
    public static void SaveData(float[,] grade, float[] finalTest)
    {
        string s;
        print(grade[0, 2]);
        s = JsonConvert.SerializeObject(grade);
        PlayerPrefs.SetString(gradeDataSave,s);
        s = JsonConvert.SerializeObject(finalTest);
        PlayerPrefs.SetString(finalTestDataSave, s);
        PlayerPrefs.Save();
    }
    public static float[,] LoadGrade()
    {
        return JsonConvert.DeserializeObject<float[,]>(PlayerPrefs.GetString(gradeDataSave));
    }
    public static float[] LoadFinalTest()
    {
        return JsonConvert.DeserializeObject<float[]>(PlayerPrefs.GetString(finalTestDataSave));
    }
    public int GetCurrentSemester()
    {
        return PlayerPrefs.GetInt("currentSemester", 1);
    }
    public int GetCurrentTest(int semester)
    {
        for(int i = 0; i < 3; i++)
        {
            if(grade[semester,i] == -1)
            {         
                return i;
            }
        }
        return 3;
    }
    public void StartUiObjects()
    {
        charName.text = "Joyce Brum";
        charSemester.text = GetCurrentSemester().ToString();
        crSoFar.text = GetAverage(GetCurrentSemester()-1).ToString();
        //Class Grid
        CreateClassGrid();
    }
    public void CreateClassGrid()
    {
        string[] names = { "Matematica", "História", "Ciências" };
        int i = 0;
        int current = GetCurrentTest(GetCurrentSemester() - 1);
        for (i = 0; i < 3; i++){
            Vector3 targetScale;
            if (i == 3) break;
            if (i == current) { targetScale = new Vector3(1f, 1.1f, 1f); } else { targetScale = new Vector3(1f, 0.9f, 1f); }
            var temp = classBox[i];
            temp.transform.DOScale(new Vector3(0, 0, 0), 0);
            temp.transform.DOScale(targetScale, 0.5f);
            temp.transform.GetChild(0).GetComponent<Text>().text = names[i];
            temp.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = grade[GetCurrentSemester()-1,i].ToString();
        }
        if(current == 3)
        {
            if(GetAverage(GetCurrentSemester() - 1) < 5.0)
            {
                ShowFinalTest(GetCurrentSemester() - 1);
                if (finalTest[GetCurrentSemester()-1] != -1 && GetAverageWithFinalTest(GetCurrentSemester()-1) < 5.0)
                {
                    print("REPROVADO");
                    ResetSemester();
                    SceneManager.LoadScene("MenuDeFases");
                }
                else if(finalTest[GetCurrentSemester() - 1] != -1)
                {
                    print("PASSAR DE SEMESTRE COM FINAL");
                    NextSemester();
                    SceneManager.LoadScene("MenuDeFases");
                }
            }
            else
            {
                finalTest[GetCurrentSemester() - 1] = 0;
                print("PASSAR DE SEMESTRE");
                NextSemester();
                SceneManager.LoadScene("MenuDeFases");
            }
        }
    }
    public float GetAverage(int semester)
    {
        if(grade[semester, 0] != -1 && grade[semester, 1] != -1 && grade[semester, 2] != -1)
        {
            return (grade[semester, 0] + grade[semester, 1] + grade[semester, 2]) / 3;
        }
        return 0;
    }
    public float GetAverageWithFinalTest(int semester)
    {
        return (GetAverage(semester) + finalTest[semester]) / 2;
    }
    public void InitializeGrade()
    {
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < semesterTotal; j++)
            {
                grade[j,i] = -1;
            }
            finalTest[i] = -1;
        }
    }
    public void ShowFinalTest(int semester)
    {
        var temp = finalTestBox;
        temp.transform.DOScale(new Vector3(0, 0, 0), 0);
        temp.transform.DOScale(new Vector3(1, 0.9f, 1), 0.5f);
        temp.transform.GetChild(0).GetComponent<Text>().text = "Prova Final";

        if (finalTest[semester] == -1)
        {
            temp.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "??";
        }
        else
        {
            temp.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = finalTest[semester].ToString();
        }
    }
    public void NextSemester()
    {
        PlayerPrefs.SetInt("currentSemester",GetCurrentSemester()+1);
        PlayerPrefs.Save();
    }
    public void ResetSemester()
    {
        PlayerPrefs.SetInt("currentSemester", 1);
        PlayerPrefs.Save();
    }
    public void StartLevel()
    {

        int test = GetCurrentTest(GetCurrentSemester()-1);
        if (test < 3)
        {
            print("AULA DE "+(LevelType)test);
            levelManager.transform.parent.gameObject.SetActive(true);
            menu.DOFade(0, 1f);
            levelManager.InitLevelFromIndex(test);
            levelManager.currentSemester = GetCurrentSemester() - 1;
            levelManager.currentTest = GetCurrentTest(GetCurrentSemester() - 1);
            StartCoroutine(levelManager.LevelLoop());
        }
        else
        {
            print("PROVA FINAL");
        }
    }
}
