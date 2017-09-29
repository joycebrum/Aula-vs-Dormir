using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextControler : MonoBehaviour {
    public Text[] texto;

	// Use this for initialization
	void Start ()
    {
        texto[0].text = ControleRendimento.ganho.ToString();
        texto[1].text = ControleRendimento.duracao_bebendo.ToString();

	}
	
	// Update is called once per frame
	void Update ()
    {
        texto[0].text = "Valor do Toque: " + ControleRendimento.ganho.ToString();
        texto[1].text = "Duracao da água: " + ControleRendimento.duracao_bebendo.ToString();
    }
}
