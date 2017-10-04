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
    private int RandomNumber(int min, int max)
    {
        System.Random random = new System.Random();
        return random.Next(min, max);
    }

    public void CriaObjeto()
    {
        int position = RandomNumber(0, ObjetosPrefab.Length);
        float velx = RandomNumber(2, 5);
        Vector2 direction = new Vector2(sinal*velx,0f);
        var objTransform = Instantiate(ObjetosPrefab[position]) as Transform;
        objTransform.position = transform.position; 
        ObjetosPegaveis script = objTransform.GetComponent<ObjetosPegaveis>();
        script.setDirection(direction);

    }
}
