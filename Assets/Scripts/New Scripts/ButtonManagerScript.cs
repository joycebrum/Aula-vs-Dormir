using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagerScript : MonoBehaviour {
    [SerializeField] private GameObject transition;    	
	// Update is called once per frame
	void Update () {
		
	}
    public void OpenNewSceneCiencias(string name)
    {
        HistoryScript.position = 1;
        StartCoroutine(OpenNewSceneWithTransition(name));
    }
    private IEnumerator OpenNewSceneWithTransition(string name)
    {
        transition.GetComponent<Animator>().Play("transition_on");
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadSceneAsync(name);
    }
}
