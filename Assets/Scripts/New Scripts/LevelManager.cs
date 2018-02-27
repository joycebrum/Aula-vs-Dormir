using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	[SerializeField] private List<ObjectCreator> criadoresObj;

	[Header("Configurações do Level")]
	[SerializeField] private LevelData levelData;

	[Header("Elementos da UI")]
	[SerializeField] private Text timeText;
	[SerializeField] private Text coffeeText;
	[SerializeField] private Image beginText;
	[SerializeField] private Image endText;
	[SerializeField] private Image transition;

	public float tempoRestante;
	private Coroutine levelLoop;
	private Coroutine timer;

	[SerializeField] private GameObject explosion;

	private void Start(){
		tempoRestante = levelData.duracao;
		levelLoop = StartCoroutine(LevelLoop());
	}
	private IEnumerator LevelLoop(){
		transition.GetComponent<Animator>().Play("transition_off");
		yield return new WaitForSeconds(0.5f);
		beginText.gameObject.SetActive(true);
		timer = StartCoroutine(Timer());
		yield return new WaitForSeconds(0.5f);

		while(tempoRestante > 0){
			float time = Random.Range(levelData.intervaloTempo[0],levelData.intervaloTempo[1]);
			yield return new WaitForSeconds(time);
			int i = Random.Range(0,criadoresObj.Count);
			criadoresObj[i].CriaObjeto();
		}
		FinishObjects();
		endText.gameObject.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		transition.GetComponent<Animator>().Play("transition_on");
        SceneManager.LoadScene("UpdateScene");
	}
	private IEnumerator Timer(){
		while(tempoRestante>0){
			tempoRestante -= Time.deltaTime;
			timeText.text = ((int)tempoRestante).ToString();
			yield return new WaitForEndOfFrame();
		}
	}
	public IEnumerator StopCreatorForSeconds(float seconds){
		StopCoroutine(levelLoop);
		yield return new WaitForSeconds(seconds);
		levelLoop = StartCoroutine(LevelLoop());
	}
	private void FinishObjects(){
		var list = FindObjectsOfType<FlyingObjects>();
		Destroy(GameObject.Find("TouchAreas"));
		foreach(FlyingObjects fo in list){
			Destroy(fo.gameObject,0.1f);
			Instantiate(explosion,fo.transform.position,Quaternion.identity);
		}
	}
}
