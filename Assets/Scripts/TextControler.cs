using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextControler : MonoBehaviour {
    public Text texto;

	// Use this for initialization
	void Start ()
    {
        texto.text = ControleRendimento.ganho.ToString();

	}
	
	// Update is called once per frame
	void Update ()
    {
        texto.text = ControleRendimento.ganho.ToString();
    }
}
