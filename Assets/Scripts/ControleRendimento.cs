using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControleRendimento : MonoBehaviour {
    public float max;
    public float current;
    public float time; 
    public float cooldown;
    public float perda;

    public static bool bebendo;//buffs
    public static bool lavando;
    public static float duracao_bebendo=3;
    public static float duracao_lavando = 3;
    private float tempobebendo;
    private float tempolavando;
    private float perdatemp;//poder restaurar depois de lavar
    public Text textobebendo;
    public Text textolavando;

    public static float ganho = 6;//habilidade
    public static float obj;
    public static bool perder;

    

    public GameObject healthbar;
    public Image script;
    private Color inicial;
    //animação de que começou a dormir
    private SpriteRenderer PlayerSprite;
    public Sprite dormindo;
    public Sprite acordado;

	// Use this for initialization
	void Start ()
    {
        bebendo = false;
        tempobebendo = 0;
        current = max;
        InvokeRepeating("Decrease", time, cooldown);
        inicial = script.color;
        PlayerSprite = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(lavando)
        {
            if (tempolavando == 0)
            {
                perdatemp = perda;
                perda = 0;
            }
            tempolavando += Time.deltaTime;
            int temp = (int)(duracao_lavando - tempolavando);
            textolavando.text = temp.ToString();
            if(tempolavando>=duracao_lavando)
            {
                tempolavando = 0;
                lavando = false;
                perda = perdatemp;
                textolavando.text = "Lavar o Rosto";
            }
        }
        if (bebendo)
        {
            if (tempobebendo == 0)
            {
                ganho = ganho * 2;
            }
            tempobebendo += Time.deltaTime;
            int temp = (int)(duracao_bebendo - tempobebendo);
            textobebendo.text = temp.ToString();
            if (tempobebendo >= duracao_bebendo)
            {
                tempobebendo = 0;
                bebendo = false;
                ganho = ganho / 2;
                textobebendo.text = "Beber água";
            }
        }
        if (Input.GetButtonDown("Fire1") && current<max)
        {

            current += ganho;
            if(current>max)
            {
                current = max;
            }
        }
        float calc_health = current / max;
        SetHealthBar(calc_health);
        if(calc_health<0.75)
        {
            script.color = new Color(1f, 0f, 0f);
            PlayerSprite.sprite = dormindo;
        }
        else if(calc_health>=0.75)
        {
            script.color = inicial;
            PlayerSprite.sprite = acordado;
        }
	}
    public void Decrease()
    {
        if (current > 0)
        {
            current -= perda;
            
            if (perder== true) 
            {
                current -= obj;
                perder = false;
                obj = 0;
            }
            if (current < 0)
            {
                current = 0;
            }
        }
        float calc_health = current / max;
        SetHealthBar(calc_health);
    }
    public void SetHealthBar(float myhealth)
    {
        healthbar.transform.localScale = new Vector3( myhealth, healthbar.transform.localScale.y, healthbar.transform.localScale.z);
    }
    public void Perda(float qnt)
    {
        perder = true;
        obj = qnt;
    }

}
