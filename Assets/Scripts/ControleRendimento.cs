﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControleRendimento : MonoBehaviour {
    public float max;
    public float current;
    public float time; 
    public float cooldown;
    public float perda;

    public static bool fim;

    public static bool bebendo;//buffs
    public static bool lavando;
    public static float duracao_bebendo=3;
    public static float duracao_lavando = 3;
    private float tempobebendo;
    private float tempolavando;
    private float perdatemp;//poder restaurar depois de lavar
    public Text textobebendo;
    public Text textolavando;
    public Text Rendimento;

    public static float ganho = 6;//habilidade
    public static float obj;
    public static bool perder;

    public Image content;
    private Color inicial;
    //animação de que começou a dormir
    private SpriteRenderer PlayerSprite;
    public Sprite dormindo;
    public Sprite acordado;

	// Use this for initialization
	void Start ()
    {
        fim = false;
        bebendo = false;
        tempobebendo = 0;
        current = max;
        InvokeRepeating("Decrease", time, cooldown);
        inicial = content.color;
        PlayerSprite = GetComponent<SpriteRenderer>();
        Rendimento.text = current.ToString() + " / " + max.ToString();
        textobebendo.text = Player_Controler.quantidade_de_agua.ToString() + " passes";
        textolavando.text = Player_Controler.LavadasPossiveis.ToString() + " passes";
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!fim)
        {
            if (lavando)
            {
                if (tempolavando == 0)
                {
                    perdatemp = perda;
                    perda = 0;
                }
                tempolavando += Time.deltaTime;
                int temp = (int)(duracao_lavando - tempolavando);
                textolavando.text = temp.ToString();
                if (tempolavando >= duracao_lavando)
                {
                    tempolavando = 0;
                    lavando = false;
                    perda = perdatemp;
                    textolavando.text = Player_Controler.LavadasPossiveis.ToString() + " passes";
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
                    textobebendo.text = Player_Controler.quantidade_de_agua.ToString() + " passes";
                }
            }
            if (Input.GetButtonDown("Fire1") && current < max)
            {

                current += ganho;
                if (current > max)
                {
                    current = max;
                }
            }
            float calc_health = current / max;
            SetHealthBar(calc_health);
            if (calc_health < 0.75)
            {
                content.color = new Color(1f, 0f, 0f);
                PlayerSprite.sprite = dormindo;
            }
            else if (calc_health >= 0.75)
            {
                content.color = inicial;
                PlayerSprite.sprite = acordado;
            }
        }
	}
    public void Decrease()
    {
        if (current > 0 && !fim)
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
    public void SetHealthBar(float calc)
    {
        content.fillAmount = calc;
        Rendimento.text = current.ToString() + " / " + max.ToString();
    }
    public void Perda(float qnt)
    {
        perder = true;
        obj = qnt;
    }

}
