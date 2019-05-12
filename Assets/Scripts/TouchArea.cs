using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchArea : MonoBehaviour {
	[SerializeField] private PlayerController playerController;
	[SerializeField] private GameObject goodClick;
    private int ganhoPorToque;
    LevelManager lManager;

    private void Start(){
        lManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        ganhoPorToque = PlayerController.playerAttributes.ganhoPorToque;
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
    }
	private void Update(){
        if(this.lManager.canActivateTouchArea())
        {
            foreach(Touch touch in Input.touches)
            {
                if(touch.phase == TouchPhase.Ended)
                {
                    if(lManager.level.levelType == LevelType.HISTORY || lManager.level.levelType == LevelType.FINAL)
                    {
                        return;
                    }
		            playerController.GanharRendimento(ganhoPorToque);
		            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    pos.z = 0;
                    var go = Instantiate(goodClick,pos,Quaternion.identity);
		            Destroy(go,1f);
                }
            }
        }
	}

}
