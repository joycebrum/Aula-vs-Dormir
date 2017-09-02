using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleRendimento : MonoBehaviour {
    public GUISkin textButton;
    public float posx;
    public float posy;
    public float altura;
    public float largura;
    public float QntRendimento;
    public float MaxQnt;
    public float time;
    public float cooldown;
    public float perda;
    public float ganho;
	// Use this for initialization
	void Start () {
        QntRendimento = 150;
        MaxQnt = 150;
        posx = Screen.width / 4;
        posy = Screen.height / 8;
        altura = Screen.height / 12;
        time = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        time += Time.deltaTime;
       largura = Screen.width / 2 * (QntRendimento/MaxQnt);
        if(QntRendimento>0 && time>=cooldown)
        {
            time = 0;
            QntRendimento -= perda;
            if(QntRendimento<0)
            {
                QntRendimento = 0;
            }
        }
        if(Input.GetButtonDown("Fire1") && QntRendimento<MaxQnt)
        {
            QntRendimento += ganho;
            if(QntRendimento>MaxQnt)
            {
                QntRendimento = MaxQnt;
            }
        }
		
	}
    public void Perda(float qnt)
    {
        QntRendimento -= qnt;
        if (QntRendimento < 0)
        {
            QntRendimento = 0;
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        QntRendimento = MaxQnt;
    }
    private void OnGUI()
    {
        string quantidade = QntRendimento.ToString();
        string total = MaxQnt.ToString();
        string botao = quantidade + "/" + total;
        GUI.skin = textButton;
        GUI.Button(new Rect(posx, posy, largura, altura),botao);
    }
}
