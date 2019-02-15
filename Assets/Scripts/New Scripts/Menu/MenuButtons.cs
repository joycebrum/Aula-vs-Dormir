using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {
	[SerializeField] private GameObject transition;

	private void Start(){
        HistoryScript.textValue = "Você não está indo muito bem nas matérias de Matemática, História e Ciências, " +
            "proponho que estude para se aprimorar. Se ao final das provas você não tiver alcançado nota de aprovação, " +
            "terá de fazer a prova final que definirá sua situação final. Boa sorte!";
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
