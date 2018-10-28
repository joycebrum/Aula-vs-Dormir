using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchArea : MonoBehaviour {
	[SerializeField] private PlayerController playerController;
	[SerializeField] private GameObject goodClick;
    LevelManager lManager;

    private void Start(){
        lManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
    }
	private void OnMouseDown(){
        // acho que isso pode estar causando interferencia ao tentar clickar em um item
        if(lManager.level.levelType == LevelType.HISTORY)
        {
            return;
        }
		playerController.GanharRendimento(playerController.playerAttributes.ganhoPorToque);
		var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        var go = Instantiate(goodClick,pos,Quaternion.identity);
		Destroy(go,1f);
	}
}
