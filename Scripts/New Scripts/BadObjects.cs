﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadObjects : MonoBehaviour {
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject badClick;
    [SerializeField] private GameObject wrong;
    public int damage;

    private void Start(){
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
    }
    private void OnMouseDown(){
        var g = Instantiate(wrong,transform.position,Quaternion.Euler(0,0,45));
		Destroy(g,1f);

        playerController.PerderRendimento();
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        var go = Instantiate(badClick,pos,Quaternion.identity);
		Destroy(go,1f);
    }
}
