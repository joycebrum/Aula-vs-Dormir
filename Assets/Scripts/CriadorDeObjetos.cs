using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CriadorDeObjetos : MonoBehaviour {
    public Transform[] ObjetosPrefab = new Transform[1];
    private float cooldown;
    public float intervalo;
    public int minimo;
    public int maximo;
    public int sinal;
    public int velxmin;
    public int velxmax;
	// Use this for initialization
	void Start ()
    {
        cooldown = RandomNumber(2, 4);
        intervalo = RandomNumber(minimo, maximo);
	}
	
	// Update is called once per frame
	void Update ()
    {
        print(cooldown);
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
        float velx = RandomNumber(2, 5);
        float vely = RandomNumber(-1, 1);
        vely = vely * 2;
        Vector2 direction = new Vector2(sinal*velx,vely);
        var objTransform = Instantiate(ObjetosPrefab[position]) as Transform;
        objTransform.position = transform.position; 
        ObjetosPegaveis script = objTransform.GetComponent<ObjetosPegaveis>();
        script.setDirection(direction);

    }
}
