using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchArea : MonoBehaviour {
	[SerializeField] private PlayerController playerController;
	[SerializeField] private GameObject goodClick;

    private void Start(){
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
    }
	private void OnMouseDown(){
		playerController.GanharRendimento(playerController.playerAttributes.ganhoPorToque);
		var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        var go = Instantiate(goodClick,pos,Quaternion.identity);
		Destroy(go,1f);
	}
}
