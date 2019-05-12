using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpdateSceaneScript : MonoBehaviour {

    [Header("UI elements")]
    public Text[] texto;
    public Text numUpdates;
    public Button[] botao;

	// Use this for initialization
	void Start ()
    {
        startUI();
	}
    private void startUI()
    {
        texto[0].text = "Valor do Toque: " + PlayerController.GetPlayerAttributes().ganhoPorToque.ToString();
        texto[1].text = "Rendimento por água: " + PlayerController.GetPlayerAttributes().rendimentoPorAgua.ToString();
        texto[2].text = "Duracao banheiro: " + PlayerController.GetPlayerAttributes().duracaoBanheiro.ToString() + " segundos";
        texto[3].text = "Aguas: " + PlayerController.GetPlayerAttributes().passesAgua.ToString();
        texto[4].text = "Passes: " + PlayerController.GetPlayerAttributes().passesBanheiro.ToString();
        numUpdates.text = "Upgrades possíveis = " + PlayerController.GetPlayerAttributes().updates.ToString();
    }
    public void UpdateAgua()
    {
        if (PlayerController.GetPlayerAttributes().updates > 0)
        {
            PlayerController.playerAttributes.rendimentoPorAgua += 1;
            PlayerController.GetPlayerAttributes().updates--;
            numUpdates.text = "Updates possiveis = " + PlayerController.GetPlayerAttributes().updates.ToString();
            texto[1].text = "Rendimento por água: " + PlayerController.playerAttributes.rendimentoPorAgua.ToString();
            if (PlayerController.GetPlayerAttributes().updates == 0)
            {
                for (int i = 0; i < botao.Length; i++)
                {
                    botao[i].interactable = false;
                }
            }
            PlayerController.SavePlayerAttributes();
        }
    }
    public void UpdateLavar()
    {
        if (PlayerController.GetPlayerAttributes().updates > 0)
        {
            PlayerController.playerAttributes.duracaoBanheiro += 0.5f;
            PlayerController.GetPlayerAttributes().updates--;
            numUpdates.text = "Updates possiveis = " + PlayerController.GetPlayerAttributes().updates.ToString();
            texto[2].text = "Duracao banheiro: " + PlayerController.playerAttributes.duracaoBanheiro.ToString() + " segundos";
            if (PlayerController.GetPlayerAttributes().updates == 0)
            {
                for (int i = 0; i < botao.Length; i++)
                {
                    botao[i].interactable = false;
                }
            }
            PlayerController.SavePlayerAttributes();
        }

    }
    public void UpdateGanho()
    {
        if (PlayerController.GetPlayerAttributes().updates > 0)
        {
            PlayerController.playerAttributes.ganhoPorToque++;
            PlayerController.GetPlayerAttributes().updates--;
            numUpdates.text = "Updates possiveis = " + PlayerController.GetPlayerAttributes().updates.ToString();
            texto[0].text = "Valor do Toque: " + PlayerController.playerAttributes.ganhoPorToque.ToString();
            if (PlayerController.GetPlayerAttributes().updates == 0)
            {
                for (int i = 0; i < botao.Length; i++)
                {
                    botao[i].interactable = false;
                }
            }
            PlayerController.SavePlayerAttributes();
        }
    }
    public void ComprarAgua()
    {
        if (PlayerController.GetPlayerAttributes().updates > 0)
        {
            PlayerController.playerAttributes.passesAgua++;
            PlayerController.GetPlayerAttributes().updates--;
            numUpdates.text = "Updates possiveis = " + PlayerController.GetPlayerAttributes().updates.ToString();
            texto[3].text = "Aguas: " + PlayerController.playerAttributes.passesAgua.ToString();
            if (PlayerController.GetPlayerAttributes().updates == 0)
            {
                for (int i = 0; i < botao.Length; i++)
                {
                    botao[i].interactable = false;
                }
            }
            PlayerController.SavePlayerAttributes();
        }
    }
    public void Passesprobanheiro()
    {
        if (PlayerController.GetPlayerAttributes().updates > 0)
        {
            PlayerController.playerAttributes.passesBanheiro++;
            PlayerController.GetPlayerAttributes().updates--;
            numUpdates.text = "Updates possiveis = " + PlayerController.GetPlayerAttributes().updates.ToString();
            texto[4].text = "Passes: " + PlayerController.playerAttributes.passesBanheiro.ToString();
            if (PlayerController.GetPlayerAttributes().updates == 0)
            {
                for (int i = 0; i < botao.Length; i++)
                {
                    botao[i].interactable = false;
                }
            }
            PlayerController.SavePlayerAttributes();
        }
    }
    public void Sair(string name)
    {
        SceneManager.LoadScene(name);
    }

}
