using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Seletor_de_fases : MonoBehaviour {
    public string fase;
   // Use this for initialization

   void OnMouseDown()
   {
        Debug.Log("Clicked");
        SceneManager.LoadScene(fase);
   }
}
