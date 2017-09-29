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
    public static float duracao_bebendo=3;
    private float tempobebendo;
    public static float ganho = 6;//habilidade
    public static float obj;
    public static bool perder;

    public GameObject healthbar;
    public Image script;
    private Color inicial;
	// Use this for initialization
	void Start ()
    {
        bebendo = false;
        tempobebendo = 0;
        current = max;
        InvokeRepeating("Decrease", time, cooldown);
        inicial = script.color;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(bebendo)
        {
            if (tempobebendo == 0)
            {
                ganho = ganho * 2;
            }
            tempobebendo += Time.deltaTime;
            if(tempobebendo>=duracao_bebendo)
            {
                tempobebendo = 0;
                bebendo = false;
                ganho = ganho / 2;
            }
        }
        if(Input.GetButtonDown("Fire1") && current<max)
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
        }
        else if(calc_health>=0.75)
        {
            script.color = inicial;
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
        print(perder);
        obj = qnt;
    }

}
