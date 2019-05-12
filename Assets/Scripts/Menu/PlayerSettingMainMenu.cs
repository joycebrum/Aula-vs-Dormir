using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettingMainMenu : MonoBehaviour {
    public Text playerNameText;
    public InputField input;
    public GameObject panelChangeName;
    // Use this for initialization 
    private void Awake()
    {
        PlayerController.LoadPlayerAttributes();
    }

    void Start () {
        verificaSeMostraPanel();
        playerNameText.text = PlayerController.playerAttributes.nome;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void verificaSeMostraPanel()
    {
        if(PlayerController.playerAttributes.nome.Length > 0)
        {
            this.panelChangeName.SetActive(false);
        }
    }
    public void AtualizaNomePlayer()
    {
        if(input.text.Length > 0)
        {
            PlayerController.playerAttributes.nome = input.text;
            this.playerNameText.text = PlayerController.playerAttributes.nome;
            PlayerController.SavePlayerAttributes();
            closePanel();
        }
    }
    public void CancelarAtualizacaoNomePlayer()
    {
        if(PlayerController.playerAttributes.nome.Length > 0)
        {
            closePanel();
        }
    }
    public void openPanel()
    {
        this.panelChangeName.SetActive(true);
    }
    public void closePanel()
    {
        this.panelChangeName.SetActive(false);
    }
}
