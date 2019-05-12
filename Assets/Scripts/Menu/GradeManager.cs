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
    public static string currentSemesterDataSave = "currentSemester";

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
    public Text bPlayText;
    public Text bUpdateText;
    
    private void Start()
    {
        //UpdateText
        this.bUpdateText.text = "Upgrade (" + PlayerController.GetPlayerAttributes().updates + ")";
        PlayerController.LoadPlayerAttributes();        
        //INIT
        grade = GradeManager.LoadGrade();
        finalTest = GradeManager.LoadFinalTest();
        SaveData(grade, finalTest);
       
        StartUiObjects();
    }
    public static void SaveData(float[,] grade, float[] finalTest)
    {
        string s;
        s = JsonConvert.SerializeObject(grade);
        PlayerPrefs.SetString(gradeDataSave,s);
        s = JsonConvert.SerializeObject(finalTest);
        PlayerPrefs.SetString(finalTestDataSave, s);
        PlayerPrefs.Save();
    }
    public static float[,] LoadGrade()
    {
        if(PlayerPrefs.HasKey(gradeDataSave))
        {
            return JsonConvert.DeserializeObject<float[,]>(PlayerPrefs.GetString(gradeDataSave));
        }
        else
        {
            float[,] gradeLocal = new float[semesterTotal, 3];
            for(int i = 0; i < semesterTotal; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    gradeLocal[i, j] = -1f;
                }
            }

            return gradeLocal;
        }
    }
    public static float[] LoadFinalTest()
    {
        if (PlayerPrefs.HasKey(finalTestDataSave))
        {
            return JsonConvert.DeserializeObject<float[]>(PlayerPrefs.GetString(finalTestDataSave));
        }
        else
        {
            float[] finalTestLocal = new float[semesterTotal];
            for(int i = 0; i < semesterTotal; i++)
            {
                finalTestLocal[i] = -1f;
            }
            return finalTestLocal;
        }
        
    }
    public int GetCurrentSemester()
    {
        return PlayerPrefs.GetInt(currentSemesterDataSave, 1);
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
        charName.text = PlayerController.playerAttributes.nome;
        charSemester.text = GetCurrentSemester().ToString() + "º Semestre";
        crSoFar.text = GetAverage(GetCurrentSemester()-1).ToString("F2");
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
            if(grade[GetCurrentSemester() - 1, i] == -1)
            {
                temp.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "??";
            } else
            {
                temp.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = grade[GetCurrentSemester()-1,i].ToString();
            }
            
        }
        if(current == 3)
        {
            if(GetAverage(GetCurrentSemester() - 1) < 5.0)
            {
                ShowFinalTest(GetCurrentSemester() - 1);
                this.bPlayText.text = "JOGAR";
                if (finalTest[GetCurrentSemester()-1] != -1)
                {
                    this.bPlayText.text = GetCurrentSemester() < 8 ? "Próximo Semestre" : "Resetar o Jogo";
                }
            }
            else
            {
                this.bPlayText.text = "Próximo Semestre";
                finalTest[GetCurrentSemester() - 1] = -1;
            }
        }
    }
    public static float GetAverage(int semester)
    {
        float[,] grade = GradeManager.LoadGrade();
        if(grade[semester, 0] != -1 && grade[semester, 1] != -1 && grade[semester, 2] != -1)
        {
            return (grade[semester, 0] + grade[semester, 1] + grade[semester, 2]) / 3;
        }
        return 0;
    }
    public static float GetAverageWithFinalTest(int semester)
    {
        if (GradeManager.LoadFinalTest()[semester] == -1) return 0;
        return (GetAverage(semester) + GradeManager.LoadFinalTest()[semester]) / 2;
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
        for(int i = 0; i < 3; i++)
        {
            grade[GetCurrentSemester() - 1, i] = -1;
        }
        finalTest[GetCurrentSemester() - 1] = -1;
        GradeManager.SaveData(grade, finalTest);
        PlayerPrefs.Save();
    }

    //mexer aqui para abrir a fase final
    public void StartLevel()
    {

        int test = GetCurrentTest(GetCurrentSemester() - 1);
        if (test <= 3 && this.bPlayText.text.CompareTo("JOGAR") == 0)
        {
            menu.interactable = false;
            levelManager.transform.parent.gameObject.SetActive(true);
            menu.DOFade(0, 1f);
            levelManager.InitLevelFromIndex(test);
            levelManager.currentSemester = GetCurrentSemester() - 1;
            levelManager.currentTest = GetCurrentTest(GetCurrentSemester() - 1);
            StartCoroutine(levelManager.LevelLoop());
        } 
        else
        {
            if(GetCurrentSemester() < 8)
            {
                NextSemester();
                SceneManager.LoadScene("MenuDeFases");
            }
            else
            {
                GradeHistory.resetAllSemesters();
                PlayerController.ResetPlayerAttributes();
            }
        }
    }
}
