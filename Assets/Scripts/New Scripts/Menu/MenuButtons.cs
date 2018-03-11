using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {
	[SerializeField] private GameObject transition;

	private void Start(){
        HistoryScript.position = 0;
		transition.GetComponent<Animator>().Play("transition_off");
	}
	public void OpenNewScene(string name){
		StartCoroutine(OpenNewSceneWithTransition(name));
	}
	private IEnumerator OpenNewSceneWithTransition(string name){
		transition.GetComponent<Animator>().Play("transition_on");
		yield return new WaitForSeconds(0.4f);
		SceneManager.LoadSceneAsync(name);
	}
	public void ExitGame(){
		Application.Quit();
	}
}
