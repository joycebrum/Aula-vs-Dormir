using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpdateSceaneScript : MonoBehaviour {

    public PlayerAttributes playerAttributes;
    public Text[] texto;
    public static int updates=0;
    public Text numUpdates;
    public Button[] botao;
    private int updatesantigo;
	// Use this for initialization
	void Start ()
    {
        texto[0].text = "Valor do Toque: " + playerAttributes.ganhoPorToque.ToString();
        texto[1].text = "Rendimento por água: " + playerAttributes.rendimentoPorAgua.ToString();
        texto[2].text = "Duracao banheiro: " + playerAttributes.duracaoBanheiro.ToString() + " segundos";
        texto[3].text = "Aguas: " + playerAttributes.passesAgua.ToString();
        texto[4].text = "Passes: " + playerAttributes.passesBanheiro.ToString();
        numUpdates.text = "Updates possiveis = " + updates.ToString();
        updatesantigo = updates;
	}

    public void UpdateAgua()
    {
        if (updates > 0)
        {
            TextControler.mudou = true;
            playerAttributes.rendimentoPorAgua += 1;
            updates--;
            numUpdates.text = "Updates possiveis = " + UpdateSceaneScript.updates.ToString();
            texto[1].text = "Rendimento por água: " + playerAttributes.rendimentoPorAgua.ToString();
            if (updates == 0)
            {
                for (int i = 0; i < botao.Length; i++)
                {
                    botao[i].interactable = false;
                }
            }
        }
    }
    public void UpdateLavar()
    {
        if (updates > 0)
        {
            TextControler.mudou = true;
            playerAttributes.duracaoBanheiro += 0.5f;
            updates--;
            numUpdates.text = "Updates possiveis = " + UpdateSceaneScript.updates.ToString();
            texto[2].text = "Duracao banheiro: " + playerAttributes.duracaoBanheiro.ToString() + " segundos";
            if (updates == 0)
            {
                for (int i = 0; i < botao.Length; i++)
                {
                    botao[i].interactable = false;
                }
            }
        }

    }
    public void UpdateGanho()
    {
        if (updates > 0)
        {
            playerAttributes.ganhoPorToque++;
            updates--;
            numUpdates.text = "Updates possiveis = " + UpdateSceaneScript.updates.ToString();
            texto[0].text = "Valor do Toque: " + playerAttributes.ganhoPorToque.ToString();
            if (updates == 0)
            {
                for (int i = 0; i < botao.Length; i++)
                {
                    botao[i].interactable = false;
                }
            }
        }
    }
    public void ComprarAgua()
    {
        TextControler.mudou = true;
        if (updates > 0)
        {
            playerAttributes.passesAgua++;
            updates--;
            numUpdates.text = "Updates possiveis = " + UpdateSceaneScript.updates.ToString();
            texto[3].text = "Aguas: " + playerAttributes.passesAgua.ToString();
            if (updates == 0)
            {
                for (int i = 0; i < botao.Length; i++)
                {
                    botao[i].interactable = false;
                }
            }
        }
    }
    public void Passesprobanheiro()
    {
        TextControler.mudou = true;
        if (updates > 0)
        {
            playerAttributes.passesBanheiro++;
            updates--;
            numUpdates.text = "Updates possiveis = " + UpdateSceaneScript.updates.ToString();
            texto[4].text = "Passes: " + playerAttributes.passesBanheiro.ToString();
            if (updates == 0)
            {
                for (int i = 0; i < botao.Length; i++)
                {
                    botao[i].interactable = false;
                }
            }
        }
    }
    public void Sair(string name)
    {
        SceneManager.LoadScene(name);
    }

}
