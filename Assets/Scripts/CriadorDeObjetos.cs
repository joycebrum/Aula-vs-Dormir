using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CriadorDeObjetos : MonoBehaviour {
    public Transform[] ObjetosPrefab = new Transform[1];
    private float cooldown;
    public float intervalo;
	// Use this for initialization
	void Start ()
    {
        cooldown = RandomNumber(2, 4);
        intervalo = RandomNumber(5, 8);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            CriaObjeto();
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
        Vector2 direction = new Vector2(2f,0f);
        var objTransform = Instantiate(ObjetosPrefab[position]) as Transform;
        objTransform.position = transform.position;
        ObjetosPegaveis script = objTransform.GetComponent<ObjetosPegaveis>();
        script.setDirection(direction);

    }
}
