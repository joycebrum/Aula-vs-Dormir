using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Seletor_de_fases : MonoBehaviour {
    public int oi;
    //public string fase;
    // Use this for initialization

    /*void OnMouseDown()
    {
         Debug.Log("Clicked");
         SceneManager.LoadScene(fase);
    }*/

    public void LoadNextLevel(string fase)
    {
        Debug.Log(fase);
        SceneManager.LoadScene(fase);
    }

    public void BackToSelection()
    {
        SceneManager.LoadScene("MenuDeFases");
    }

    public void UpdateGanho()
    {
        print(ControleRendimento.ganho);
        ControleRendimento.ganho++;
        print(ControleRendimento.ganho);
    }
}
