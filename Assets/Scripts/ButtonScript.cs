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
        if (Player_Controler.quantidade_de_agua > 0)
        {
            ControleRendimento.bebendo = true;
        }
        Player_Controler.quantidade_de_agua--;
    }

    public void LoadNextLevel(string fase)
    {
        Debug.Log(fase);
        SceneManager.LoadScene(fase);
    }

    public void BackToSelection()
    {
        SceneManager.LoadScene("MenuDeFases");
    }


    public void UpdateAgua()
    {
        if (UpdateSceaneScript.updates > 0)
        {
            ControleRendimento.duracao_bebendo += 0.5f;
            UpdateSceaneScript.updates--;
            UpdateSceaneScript.numUpdates.text = "Updates possiveis = " + UpdateSceaneScript.updates.ToString();
        }
    }
    public void UpdateGanho()
    {
        if (UpdateSceaneScript.updates > 0)
        {
            ControleRendimento.ganho++;
            UpdateSceaneScript.updates--;
            UpdateSceaneScript.numUpdates.text = "Updates possiveis = " + UpdateSceaneScript.updates.ToString();
        }
    }
}
