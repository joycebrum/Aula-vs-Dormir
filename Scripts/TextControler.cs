using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextControler : MonoBehaviour {
    public Text[] texto;
    public static bool mudou = false;

	// Use this for initialization
	void Start ()
    {
        texto[0].text = "Valor do Toque: " + ControleRendimento.ganho.ToString();
        texto[1].text = "Duracao da água: " + ControleRendimento.duracao_bebendo.ToString() + " segundos";
        texto[2].text = "Duracao lavar rosto: " + ControleRendimento.duracao_lavando.ToString() + " segundos";
        texto[3].text = "Aguas: " + Player_Controler.quantidade_de_agua.ToString();
        texto[4].text = "Passes: " + Player_Controler.LavadasPossiveis.ToString();

    }
	
	// Update is called once per frame
	public static void Mudou ()
    {
            
    }
}
