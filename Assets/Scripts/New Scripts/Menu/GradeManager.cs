using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GradeManager : MonoBehaviour {
    const int semesterTotal = 8;
    [Header("Importants")]
	public float[,] grade = new float[semesterTotal,3];
    public float[] finalTest = new float[semesterTotal];

    [Header("Save Objects")]
    public string gradeDataSave;
    public string finalTestDataSave;

    [Header("UI Objects")]
    public GameObject classUi;
    public GameObject finalClassUi;
    public Transform classGrid;
    public GameObject charPanel;

    private void Start()
    {
        grade[0, 0] = 2.3f;
        grade[0, 1] = 5.4f;
        StartUiObjects();
    }
    public void SaveData()
    {
        string s;
        s = JsonUtility.ToJson(grade);
        PlayerPrefs.SetString(gradeDataSave,s);
        s = JsonUtility.ToJson(finalTest);
        PlayerPrefs.SetString(finalTestDataSave, s);
    }
    public void LoadData()
    {
        grade = JsonUtility.FromJson<float[,]>(PlayerPrefs.GetString(gradeDataSave));
        finalTest = JsonUtility.FromJson<float[]>(PlayerPrefs.GetString(finalTestDataSave));
    }
    public int GetCurrentSemester()
    {
        for(int i = 0; i < semesterTotal; i++)
        {
            if(grade[i,0] == 0)
            {
                return i;
            }
        }
        return semesterTotal-1;
    }
    public int GetCurrentTest(int semester)
    {
        for(int i = 0; i < 3; i++)
        {
            if(grade[semester,i] != 0)
            {
                return i;
            }
        }
        return 2;
    }
    public void StartUiObjects()
    {
        //Char Panel
        charPanel.transform.DOScale(new Vector3(0, 0, 0), 0);
        charPanel.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        charPanel.transform.GetChild(1).GetComponent<Text>().text = "7.1";
        charPanel.transform.GetChild(2).GetComponent<Text>().text = "Name";
        charPanel.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "2 Semester";
        //Class Grid
        CreateClassGrid();
    }
    public void CreateClassGrid()
    {
        string[] names = { "Matematica", "História", "Ciências" };
        for (int i = 0; i < GetCurrentTest(GetCurrentSemester()); i++){
            var temp = Instantiate(classUi,Vector3.up,Quaternion.identity,classGrid);
            temp.transform.DOScale(new Vector3(0, 0, 0), 0);
            temp.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
            temp.transform.GetChild(0).GetComponent<Text>().text = names[i];
            temp.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "7";
        }
    }
}
