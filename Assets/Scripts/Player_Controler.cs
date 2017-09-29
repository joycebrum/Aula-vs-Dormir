using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Controler : MonoBehaviour {
    public float leveltimeMax;
    private float leveltime;
    public string UpdateSceane;
    public ControleRendimento rend;
    public static float num;
    public Text tempo;
    public static int quantidade_de_agua = 0;
    public int aguatemporaria;

	// Use this for initialization
	void Start () {
        leveltime = 0;
        rend = GetComponent<ControleRendimento>();
        num = 1;
        int convertido = (int)leveltimeMax;
        tempo.text = convertido.ToString();
	}
    public void Awake()
    {
        aguatemporaria = quantidade_de_agua;
    }

    // Update is called once per frame
    void Update ()
    {
        leveltime += Time.deltaTime;

        int convertido = (int)(leveltimeMax-leveltime);
        if(convertido<0)
        {
            convertido = 0;
        }
        tempo.text = convertido.ToString();

        if (leveltime>=leveltimeMax)
        {
            num = rend.current / rend.max;
            quantidade_de_agua = aguatemporaria;
            SceneManager.LoadScene(UpdateSceane);
        }
	}

}
