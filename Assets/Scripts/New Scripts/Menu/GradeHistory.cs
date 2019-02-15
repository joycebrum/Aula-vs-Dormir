using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GradeHistory : MonoBehaviour {

    const int semesterTotal = 8;
    public Transform grid;
    public GameObject semesterPrefab;

    private List<GameObject> oldSemesters = new List<GameObject>();

    public void Start()
    {
        StartUI();
    }
    public void StartUI()
    {
        for(int i = 0; i < semesterTotal; i++)
        {
            var t = Instantiate(semesterPrefab, grid);
            oldSemesters.Add(t);
            t.transform.GetChild(0).GetComponent<Text>().text = (i + 1) + "º Semestre";
            t.transform.DOScale(new Vector3(0, 0, 0), 0);
            t.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
            float[] finalTest = GradeManager.LoadFinalTest();
            if (finalTest[i] < 0)
            {
                t.transform.GetChild(1).GetComponent<Text>().text = GradeManager.GetAverage(i).ToString();
            }
            else
            {
                t.transform.GetChild(1).GetComponent<Text>().text = GradeManager.GetAverageWithFinalTest(i).ToString();
            }
        }
    }
    public void ResetWithAnimation()
    {
        GradeHistory.resetAllSemesters();
        StartCoroutine(ResetAnimation());
    }
    private IEnumerator ResetAnimation()
    {
        foreach(GameObject go in oldSemesters)
        {
            go.transform.DOScale(new Vector3(0, 0, 0), 0.5f);
            Destroy(go, 0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        oldSemesters = new List<GameObject>();
        StartUI();
    }

    public static void resetAllSemesters()
    {
        float[,] grade = new float[semesterTotal, 3];
        float[] finalTest = new float[semesterTotal];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < semesterTotal; j++)
            {
                grade[j, i] = -1;
            }
            finalTest[i] = -1;
        }
        GradeManager.SaveData(grade, finalTest);
        PlayerPrefs.SetInt(GradeManager.currentSemesterDataSave, 1);
        PlayerPrefs.Save();
    }
}
