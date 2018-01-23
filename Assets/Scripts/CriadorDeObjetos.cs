using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CriadorDeObjetos : MonoBehaviour {
    public Transform[] ObjetosPrefab = new Transform[1];
    public static bool fim;
    private float cooldown;
    public float intervalo;//valor onde eh armazenado o valor aleatorio entre minimo e maximo
    public int minimo;//tempo minimo entre o sugimento de dois objetos
    public int maximo;//tempo maximo entre o surgimento de dois objetos
    public int sinal;// para qual direção o objeto vai (direita ou esquerda)
    public int velxmin;//velocidade minima dos objetos criados
    public int velxmax;// velocidade maxima
	// Use this for initialization
	void Start ()
    {
        cooldown = RandomNumber(2, 4);
        intervalo = RandomNumber(minimo, maximo);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!fim && !Player_Controler.popupActive)
        {
            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
            }
            else
            {
                CriaObjeto();
                intervalo = RandomNumber(minimo, maximo);
                cooldown = intervalo;
            }
        }
	}
    private float RandomNumber(int min, int max)
    {
        float mynumber =  UnityEngine.Random.Range(min,max);
        return mynumber;
    }
    private int RandomintNumber(int min, int max)
    {
        int mynumber = UnityEngine.Random.Range(min, max);
        return mynumber;
    }

    public void CriaObjeto()
    {
        int position = RandomintNumber(0, ObjetosPrefab.Length);
        if (ControleRendimento.cafeEfeito || ControleRendimento.bebendo)// se está sob efeito do café ou da agua nao gera mais cafe
        {
            if (ObjetosPrefab[position].gameObject.tag == "Cafe")
            {
                print("bla");
                return;
            }
        }
        float velx = RandomNumber(2, 5);
        Vector2 direction = new Vector2(sinal * velx, 0);
        var objTransform = Instantiate(ObjetosPrefab[position]) as Transform;
        objTransform.position = transform.position;
        ObjetosPegaveis script = objTransform.GetComponent<ObjetosPegaveis>();
        script.setDirection(direction);
    }
}
