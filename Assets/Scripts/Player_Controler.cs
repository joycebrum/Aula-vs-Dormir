using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controler : MonoBehaviour {
    private float time;
    public float leveltimeMax;
    private float leveltime;
    public bool gameover;

	// Use this for initialization
	void Start () {
        time = 0;
        leveltime = 0;

	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        /*leveltime += Time.deltaTime;
        if(leveltime>=leveltimeMax)
        {
            gameover = true;
        }*/
	}
}
