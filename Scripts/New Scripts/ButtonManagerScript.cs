﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagerScript : MonoBehaviour {
    [SerializeField] private GameObject transition;
    private AudioSource myAudio;
    // Use this for initialization
    void Start () {
        myAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OpenNewScene(string name)
    {
        StartCoroutine(OpenNewSceneWithTransition(name));
    }
    private IEnumerator OpenNewSceneWithTransition(string name)
    {
        transition.GetComponent<Animator>().Play("transition_on");
        yield return new WaitForSeconds(0.4f);
        myAudio.Stop();
        SceneManager.LoadSceneAsync(name);
    }
}
