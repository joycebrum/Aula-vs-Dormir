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

    public static bool fim;

    //variaveis para efeito de cafe
    public Button botaoDaAgua;
    public Button botaoDoBanheiro;
    private bool EstadoAntigoAgua;
    private float ganhoAntigo;
    public static bool cafeEfeito;
    private bool efeitoCafe;
    private int countcafe;
    private float timeCurrentCafe;
    public static float valorCafe;
    public static float timeCafe;
    public Text textocafe;

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
        //salva o ganho para restaurá-lo
        ganhoAntigo = ganho;

        //inicialização das variaveis do efeito cafe
        countcafe = 1;
        cafeEfeito = false;
        timeCurrentCafe = 0;
        efeitoCafe = false;
        textocafe.text = (countcafe-1) + " cafés tomados";

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
        if (!fim && !Player_Controler.popupActive)
        {
            if(cafeEfeito)
            {
                if(timeCurrentCafe==0)
                {
                    EstadoAntigoAgua = botaoDaAgua.interactable;
                    botaoDaAgua.interactable = false;
                    ganho += valorCafe;
                }
                timeCurrentCafe += Time.deltaTime;
                if(!efeitoCafe)
                {
                    SetCafeText(timeCurrentCafe + " de " + timeCafe);
                }
                if (timeCurrentCafe >= timeCafe && !efeitoCafe)
                {
                    ganho -= valorCafe;
                    ganho = ganho - valorCafe/2;
                    SetCafeText("EFEITO COLATERAL GANHO REDUZIDO PARA " + ganho);
                    efeitoCafe = true;
                }
                else
                {
                    if (timeCurrentCafe >= timeCafe + timeCafe * countcafe / 4)//acabou o efeito negativo
                    {
                        cafeEfeito = false;
                        efeitoCafe = false;
                        timeCurrentCafe = 0;
                        countcafe++;
                        textocafe.text = (countcafe - 1) + " cafés tomados";
                        ganho = ganho + valorCafe / 2;
                        botaoDaAgua.interactable = EstadoAntigoAgua;
                    }
                }
            }
            if (lavando)
            {
                if (tempolavando == 0)//primeira vez que entra no if
                {
                    botaoDoBanheiro.interactable = false;
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
                    if(Player_Controler.LavadasPossiveis>0)
                    {
                        botaoDoBanheiro.interactable = true;
                    }
                }
            }
            if (bebendo)
            {
                if (tempobebendo == 0)//primeira vez que entra no if
                {
                    botaoDaAgua.interactable = false;
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
                    if (Player_Controler.quantidade_de_agua > 0)
                    {
                        botaoDaAgua.interactable = true;
                    }
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
        else
        {
            ganho = ganhoAntigo;
        }
	}
    public void Decrease()
    {
        if (current > 0 && !fim && !Player_Controler.popupActive)
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

    public void SetCafeText (string texto)
    {
        textocafe.text = texto;
    }

}
