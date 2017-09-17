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
    public static float ganho = 6;
    public static float obj;
    public static bool perder;
    public GameObject healthbar;
    public Text UpdateGanhoButton;
	// Use this for initialization
	void Start () {
        current = max;
        InvokeRepeating("Decrease", time, cooldown);
    }
	
	// Update is called once per frame
	void Update ()
    {
        print(ganho);
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

    public void UpdateGanho()
    {
        print(ganho);
        ganho++;
        print(ganho);
        //UpdateGanhoButton.text = "Ganho = " + ganho; //Como escrever isso no botão
    }

}
