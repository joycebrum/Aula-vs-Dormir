using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Controler : MonoBehaviour {

    public GameObject popupInicial;
    public Text popupText;
    public static bool popupActive;

    public float leveltimeMax;
    private float leveltime;
    private float conttempo;
    public string UpdateSceane;
    public ControleRendimento rend;
    public static float num;
    public Text tempo;
    public static int quantidade_de_agua = 100;
    public static int LavadasPossiveis = 100;
    public Text textoDeVitoria;
    public GameObject popup;

    public static bool usoubuff;
    public Button botaoDaAgua;
    public Button botaoDoBanheiro;

    // Use this for initialization
    void Start ()
    {
        popupActive = true;

        ControleRendimento.fim = false;
        ObjetosPegaveis.fim = false;
        CriadorDeObjetos.fim = false;
        leveltime = 0;
        conttempo = 0;
        rend = GetComponent<ControleRendimento>();
        num = 1;
        int convertido = (int)leveltimeMax;
        tempo.text = convertido.ToString();
        usoubuff = false;
        if (LavadasPossiveis > 0)
        {
            botaoDoBanheiro.interactable = true;
        }
        if(quantidade_de_agua>0)
        {
            botaoDaAgua.interactable = true;
        }
	}

    // Update is called once per frame
    void Update ()
    {
        if (popupActive)
        {
            if (Input.GetButtonDown("Fire1")) 
            {
                Destroy(popupText);
                popupInicial.SetActive(false);
                popupActive = false;
            }
        }
        else
        {
            if (usoubuff)
            {

            }
            if (leveltime >= leveltimeMax)
            {
                conttempo += Time.deltaTime;
                if (conttempo >= 2)
                {
                    SceneManager.LoadScene(UpdateSceane);
                }
            }
            else
            {
                leveltime += Time.deltaTime;

                int convertido = (int)(leveltimeMax - leveltime);
                if (convertido < 0)
                {
                    convertido = 0;
                }
                tempo.text = convertido.ToString();

                if (leveltime >= leveltimeMax)
                {
                    ControleRendimento.fim = true;
                    ObjetosPegaveis.fim = true;
                    CriadorDeObjetos.fim = true;
                    num = rend.current / rend.max;
                    popup.SetActive(true);
                    if (num >= 0.75)
                    {
                        textoDeVitoria.text = "Ganhou";
                    }
                    else
                    {
                        textoDeVitoria.text = "Perdeu";
                    }
                }
            }
        }
	}

}
