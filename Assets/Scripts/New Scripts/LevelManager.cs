using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LevelManager : MonoBehaviour {
    [Header("Level")]
    public LevelData level;
    [SerializeField] private List<ObjectCreator> criadoresObj;

    [Header("Elementos da UI")]
    public GameObject uiElements;
    [SerializeField] private Text timeText;
    [SerializeField] private Text coffeeText;

    [SerializeField] private Image tutorialText;

    [SerializeField] private Image beginText;
    [SerializeField] private Image endText;
    [SerializeField] private Image transition;

    private Coroutine levelLoop;
    private Coroutine timer;

    public float initTime;
    public float tempoRestante;
    public float currentTime = 0;
    public int target;

    private bool canCreate = true;

    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject touchArea;
    [SerializeField] private PlayerController playerController;

    public void InitLevel(LevelData lvl)
    {
        level = lvl;
        InitializeLevelValues();
    }
    public IEnumerator LevelLoop()
    {
        touchArea.transform.DOScaleY(0, 0);
        uiElements.transform.DOScaleY(5, 0);

        InitializeLevelValues();
        //transition.GetComponent<Animator>().Play("transition_off");
        yield return ShowLittleTutorial();
        yield return ShowBeginScreen();
        ShowUI(true);
        ShowTouchArea(true);
        StartCoroutine(Timer());
        while ( Won(level.levelType) )
        {
            float time = Random.Range(level.intervaloTempo[0], level.intervaloTempo[1]);
            yield return new WaitForSeconds(time);
            int i = Random.Range(0, criadoresObj.Count);
            if(canCreate) criadoresObj[i].CriaObjeto();
        }
        FinishObjects();
        yield return ShowEndingScreen();
        Save();
    }
    private bool Won(LevelType lType)
    {
        switch (lType)
        {
            case LevelType.MATHMATIC:
                if (currentTime > 0 && playerController.rendimento < target) return true;
                break;
            case LevelType.SCIENCE:
                if (currentTime > 0 && playerController.rendimento > 0) return true;
                break;
            case LevelType.HISTORY:
                if (currentTime > 0) return true;
                break;
        }
        return false;
    }
    public IEnumerator StopCreatorForSeconds(float seconds)
    {
        canCreate = false;
        yield return new WaitForSeconds(seconds);
        canCreate = true;
    }
    private void InitializeLevelValues()
    {
        target = level.targetScore;
        initTime = level.initTime;
        tempoRestante = level.duracao;
    }
    private IEnumerator ShowLittleTutorial()
    {
        tutorialText.transform.DOScale(0f, 0);
        tutorialText.gameObject.SetActive(true);
        tutorialText.transform.GetChild(0).GetComponent<Text>().text = level.description;
        tutorialText.transform.DOScale(1f, 0.5f);
        yield return new WaitForSeconds(2f);
        tutorialText.transform.DOScale(0f, 0.1f);
        yield return new WaitForSeconds(0.2f);
        tutorialText.gameObject.SetActive(false);
    }
    private IEnumerator ShowBeginScreen()
    {
        beginText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        beginText.gameObject.SetActive(false);
    }
    private IEnumerator ShowEndingScreen()
    {
        ShowUI(false);
        endText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        endText.gameObject.SetActive(false);
    }
    private IEnumerator Timer()
    {
        currentTime = tempoRestante;
        while(currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            timeText.text = ((int)currentTime).ToString();
            yield return new WaitForEndOfFrame();
        }
    }
    private void Save()
    {
        print("Saved");
    }
    private void ShowUI(bool b)
    {
        if (b)
        {
            uiElements.transform.DOScaleY(5,0);
            uiElements.transform.DOScaleY(1, 1f);
        }
        else
        {
            uiElements.transform.DOScaleY(1, 0);
            uiElements.transform.DOScaleY(5, 0.5f);
        }
    }
    private void ShowTouchArea(bool b)
    {
        if (b)
        {
            touchArea.transform.DOScaleY(0, 0);
            touchArea.transform.DOScaleY(6, 0.5f);
        }
        else
        {
            touchArea.transform.DOScaleY(6, 0);
            touchArea.transform.DOScaleY(0, 0.5f);
        }
    }
    private void FinishObjects()
    {
        var list = FindObjectsOfType<FlyingObjects>();
        Destroy(GameObject.Find("TouchAreas"));
        foreach (FlyingObjects fo in list)
        {
            Destroy(fo.gameObject, 0.1f);
            Instantiate(explosion, fo.transform.position, Quaternion.identity);
        }
    }
}
