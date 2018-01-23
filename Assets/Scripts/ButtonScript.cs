using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {
    public int oi;
    //public string fase;
    // Use this for initialization

    /*void OnMouseDown()
    {
         Debug.Log("Clicked");
         SceneManager.LoadScene(fase);
    }*/

    

    public void Bebeu()
    {
        if (Player_Controler.quantidade_de_agua > 0) //so pode beber agua se nao beber cafe
        {
            ControleRendimento.bebendo = true;
            Player_Controler.quantidade_de_agua--;
        }
        
    }

    public void Lavou()
    {
        if (Player_Controler.LavadasPossiveis > 0)
        {
            ControleRendimento.lavando = true;
            Player_Controler.LavadasPossiveis--;
        }
        
    }

    public void LoadNextLevel(int fase)
    {
        HistoryScript.hfase = fase;
        string bas = "" + fase;
        Debug.Log(bas);
        SceneManager.LoadScene("Historia");
        Debug.Log("carregou");
    }

    public void BackToSelection()
    {
        SceneManager.LoadScene("MenuDeFases");
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void UpdateAgua()
    {
        if (UpdateSceaneScript.updates > 0)
        {
            TextControler.mudou = true;
            ControleRendimento.duracao_bebendo += 0.5f;
            UpdateSceaneScript.updates--;
            UpdateSceaneScript.numUpdates.text = "Updates possiveis = " + UpdateSceaneScript.updates.ToString();
        }
    }
    public void UpdateLavar()
    {
        if (UpdateSceaneScript.updates > 0)
        {
            TextControler.mudou = true;
            ControleRendimento.duracao_lavando += 0.5f;
            UpdateSceaneScript.updates--;
            UpdateSceaneScript.numUpdates.text = "Updates possiveis = " + UpdateSceaneScript.updates.ToString();
        }
    }
    public void UpdateGanho()
    {
        TextControler.mudou = true;
        if (UpdateSceaneScript.updates > 0)
        {
            ControleRendimento.ganho++;
            UpdateSceaneScript.updates--;
            UpdateSceaneScript.numUpdates.text = "Updates possiveis = " + UpdateSceaneScript.updates.ToString();
        }
    }
    public void ComprarAgua()
    {
        TextControler.mudou = true;
        if (UpdateSceaneScript.updates > 0)
        {
            Player_Controler.quantidade_de_agua++;
            UpdateSceaneScript.updates--;
            UpdateSceaneScript.numUpdates.text = "Updates possiveis = " + UpdateSceaneScript.updates.ToString();
        }
    }
    public void Passesprobanheiro()
    {
        TextControler.mudou = true;
        if (UpdateSceaneScript.updates > 0)
        {
            Player_Controler.LavadasPossiveis++;
            UpdateSceaneScript.updates--;
            UpdateSceaneScript.numUpdates.text = "Updates possiveis = " + UpdateSceaneScript.updates.ToString();
        }
    }
    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
