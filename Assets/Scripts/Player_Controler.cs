using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Controler : MonoBehaviour {
    public float leveltimeMax;
    private float leveltime;
    private float conttempo;
    public string UpdateSceane;
    public ControleRendimento rend;
    public static float num;
    public Text tempo;
    public static int quantidade_de_agua = 0;
    public static int LavadasPossiveis = 0;
    public Text textoDeVitoria;
    public GameObject popup;

	// Use this for initialization
	void Start () {
        leveltime = 0;
        conttempo = 0;
        rend = GetComponent<ControleRendimento>();
        num = 1;
        int convertido = (int)leveltimeMax;
        tempo.text = convertido.ToString();
	}

    // Update is called once per frame
    void Update ()
    {
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
                num = rend.current / rend.max;
                popup.SetActive(true);
                if(num>=0.75)
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
