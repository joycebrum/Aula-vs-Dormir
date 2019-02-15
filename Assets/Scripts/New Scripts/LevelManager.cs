using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEditor;

public class LevelManager : MonoBehaviour {
    [Header("Level")]
    public List<LevelData> lData;
    public LevelData level;
    [SerializeField] private List<ObjectCreator> criadoresObj;
    [SerializeField] private ObjectCreator coffeCreator;
    [SerializeField] private ObjectCreator coffeCreator2;

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
    private bool victory;

    public float initTime;
    public float tempoRestante;
    public float currentTime = 0;
    public int target;

    private bool canCreate = true;

    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject touchArea;
    [SerializeField] private PlayerController playerController;

    public int currentSemester, currentTest;
    public GameObject gradeManager;

    private bool isTouchAreaActivate;

    public void InitLevel(LevelData lvl)
    {
        victory = false;
        isTouchAreaActivate = false;
        level = lvl;
        InitializeLevelValues();
    }
    public void InitLevelFromIndex(int i)
    {
        InitLevel(lData[i]);
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
        StartCoroutine(RendimentoLoop());
        StartCoroutine(Timer());
        isTouchAreaActivate = true;
        while ( Won(level.levelType) )
        {
            float time = Random.Range(level.intervaloTempo[0], level.intervaloTempo[1]);
            yield return new WaitForSeconds(time);
            int i = Random.Range(0, criadoresObj.Count);
            if (canCreate)
            {
                criadoresObj[i].CriaObjeto();
            }
        }
        FinishObjects();
        yield return ShowEndingScreen();
        Save();
        SceneManager.LoadScene("Historia");
    }
    private bool Won(LevelType lType)
    {
        switch (lType)
        {
            case LevelType.MATHMATIC:
                if (currentTime > 0 && playerController.rendimento < target) return true;
                else if (playerController.rendimento >= target)// se for retornar false, ve se ganhou ou perdeu
                {
                    PlayerController.playerAttributes.updates++;
                    PlayerController.SavePlayerAttributes();
                    victory = true;
                    HistoryScript.textValue = "Parabéns, sua nota nessa prova será proporcional ao quão rápido você atingiu o objetivo";//mostra texto de vitoria
                }
                else
                {
                    victory = false;
                    HistoryScript.textValue = "Infelizmente sua nota não será boa nessa avaliação, mas será proporcional ao valor do seu rendimento"+
                                                " ao final da partida";//mostra texto de derrota
                }
                break;
            case LevelType.SCIENCE:
                if (currentTime > 0 && playerController.rendimento > 0) return true;
                else if(playerController.rendimento > 0)// se for retornar false, ve se ganhou ou perdeu
                {
                    victory = true;
                    PlayerController.playerAttributes.updates++;
                    PlayerController.SavePlayerAttributes();
                    HistoryScript.textValue = "Parabéns, você conseguiu sobreviver, sua pontuação será proporcional ao seu rendimento no final da partida";//mostra texto de vitoria
                }
                else
                {
                    victory = false;
                    HistoryScript.textValue = "Infelizmente você não conseguiu sobreviver e a nota zero assombrará seu histórico escolar";//mostra texto de derrota
                }
                break;
            case LevelType.HISTORY:
                if (currentTime > 0 && playerController.rendimento > 0) return true;
                else if(playerController.rendimento > 0)
                {
                    victory = true;
                    PlayerController.playerAttributes.updates++;
                    PlayerController.SavePlayerAttributes();
                    HistoryScript.textValue = "Parabéns, você conseguiu sobreviver, sua pontuação será proporcional ao seu rendimento no final da partida";//mostra texto de vitoria

                }
                else
                {
                    victory = false;
                    HistoryScript.textValue = "Infelizmente você não conseguiu sobreviver e a nota zero assombrará seu hístórico escolar";//mostra texto de derrota
                }
                break;
            case LevelType.FINAL:
                if (currentTime > 0 && playerController.rendimento > 0 && playerController.rendimento < target) return true;
                else if(playerController.rendimento >= target)
                {
                    victory = true;
                    PlayerController.playerAttributes.updates++;
                    PlayerController.SavePlayerAttributes();
                    HistoryScript.textValue = "Parabéns, sua nota nessa prova será proporcional ao quão rápido você atingiu o objetivo";//mostra texto de vitoria

                }
                else
                {
                    victory = false;
                    HistoryScript.textValue = "Infelizmente sua nota não será boa nessa avaliação, mas será proporcional ao valor do seu rendimento" +
                                                " ao final da partida";//mostra texto de derrota
                }
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
        playerController.rendimento = level.initScore;
        
    }
    private IEnumerator ShowLittleTutorial()
    {
        tutorialText.transform.DOScale(0f, 0);
        tutorialText.gameObject.SetActive(true);
        tutorialText.transform.GetChild(0).GetComponent<Text>().text = level.description;
        tutorialText.transform.DOScale(1f, 0.5f);
        yield return new WaitForSeconds(4f);
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
        float[,] grade = GradeManager.LoadGrade();
        float[] finalTest = GradeManager.LoadFinalTest();
        if(level.levelType != LevelType.FINAL)
        {
            grade[currentSemester, currentTest] = GetGrade();
        }
        else
        {
            finalTest[currentSemester] = GetGrade();
        }
        
        GradeManager.SaveData(grade,finalTest);
    }
    private float GetGrade()
    {
        switch (this.level.levelType)
        {
            case LevelType.MATHMATIC:
                if (victory)
                {
                    return notaVitoriaPorTempo();
                }
                else
                {
                    return notaDerrotaPorRendimentoAoFinal();
                }
            case LevelType.SCIENCE:
                if (victory)
                {
                    return (playerController.rendimento * 5 / 100.0f) + 5;
                }
                else
                {
                    return 0.0f;
                }
            case LevelType.HISTORY:
                if (victory)
                {
                    return (playerController.rendimento * 5 / 100.0f) + 5;
                }
                else
                {
                    return 0.0f;
                }
            case LevelType.FINAL:
                if(victory)
                {
                    if (playerController.rendimento >= target)
                    {
                        return notaVitoriaPorTempo();
                    }
                    else
                    {
                        return notaDerrotaPorRendimentoAoFinal();
                    }
                }
                else
                {
                    return 0.0f;
                }
        }
        //Criar criterio
        return 4.0f;
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
    public bool canActivateTouchArea()
    {
        return isTouchAreaActivate;
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

    private float notaDerrotaPorRendimentoAoFinal()
    {
        float medida = target / 5;
        if (playerController.rendimento <= medida)
        {
            return 0.0f;
        }
        else if (playerController.rendimento < 2 * medida)
        {
            return 1.0f;
        }
        else if (playerController.rendimento < 3 * medida)
        {
            return 2.0f;
        }
        else if (playerController.rendimento < 4 * medida)
        {
            return 3.0f;
        }
        return 4.0f;
    }

    private float notaVitoriaPorTempo()
    {
        float time = tempoRestante - currentTime;
        float medida = tempoRestante / 6;
        if (time <= medida)
        {
            return 10.0f;
        }
        else if (time < 2 * medida)
        {
            return 9.0f;
        }
        else if (time < 3 * medida)
        {
            return 8.0f;
        }
        else if (time < 4 * medida)
        {
            return 7.0f;
        }
        else if (time < 5 * medida)
        {
            return 6.0f;
        }
        return 5.0f;
    }

    private IEnumerator RendimentoLoop()
    {
        while (tempoRestante >= 0)
        {
            if (level.levelType != LevelType.HISTORY && level.levelType != LevelType.FINAL)
            {
                playerController.PerderRendimento();
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
