using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Controler : MonoBehaviour {
    public float leveltimeMax;
    private float leveltime;
    public string UpdateSceane;

	// Use this for initialization
	void Start () {
        leveltime = 0;

	}
	
	// Update is called once per frame
	void Update () {
        leveltime += Time.deltaTime;
        if(leveltime>=leveltimeMax)
        {
            SceneManager.LoadScene(UpdateSceane);
        }
	}

}
