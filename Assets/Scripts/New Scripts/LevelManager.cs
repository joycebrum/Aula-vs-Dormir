using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	[SerializeField] private List<ObjectCreator> criadoresObj;

	[Header("Configurações do Level")]
	public LevelData levelData;

	[Header("Elementos da UI")]
	[SerializeField] private Text timeText;
	[SerializeField] private Text coffeeText;
	[SerializeField] private Image beginText;
	[SerializeField] private Image endText;
	[SerializeField] private Image transition;

	public float tempoRestante;
	private Coroutine levelLoop;
	private Coroutine timer;

	private int target = 100;

	[SerializeField] private GameObject explosion;
	[SerializeField] private GameObject touchArea;
	private PlayerController playerController;

	private void Start(){
		playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
		tempoRestante = levelData.duracao;
		InitializeLoop();
	}
	private Coroutine InitializeLoop(){
		Coroutine l;
		switch(levelData.levelType){
			case LevelType.HISTORY:
				l = StartCoroutine(HistoryLoop());
				break;
			case LevelType.SCIENCE:
				l = StartCoroutine(ScienceLoop());
				break;
			case LevelType.MATHMATIC:
				l = StartCoroutine(MathLoop());
				break;
			default:
				l = StartCoroutine(MathLoop());
				break;
		}
		return l;
	}
	private IEnumerator ScienceLoop(){
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
	private IEnumerator MathLoop(){
		transition.GetComponent<Animator>().Play("transition_off");
		yield return new WaitForSeconds(0.5f);
		beginText.gameObject.SetActive(true);

		tempoRestante = 60;
		timer = StartCoroutine( Timer() );

		playerController.rendimento = 0;

		while(tempoRestante > 0 && playerController.rendimento < target){
			print(playerController.rendimento);
			float time = Random.Range(levelData.intervaloTempo[0],levelData.intervaloTempo[1]);
			yield return new WaitForSeconds(time);
			int i = Random.Range(0,criadoresObj.Count);
			criadoresObj[i].CriaObjeto();
		}
		if(playerController.rendimento >= 0 && tempoRestante > 0){
			print("ganhou");
		}else if(tempoRestante > 0){
			print("perdeu");
		}
		FinishObjects();
		endText.gameObject.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		transition.GetComponent<Animator>().Play("transition_on");
        SceneManager.LoadScene("UpdateScene");
	}
	private IEnumerator HistoryLoop(){
		transition.GetComponent<Animator>().Play("transition_off");
		yield return new WaitForSeconds(0.5f);
		beginText.gameObject.SetActive(true);

		playerController.rendimento = 50;
		tempoRestante = 60;
		timer = StartCoroutine( Timer() );

		touchArea.SetActive(false);

		while(tempoRestante > 0 && playerController.rendimento > 0){
			print(playerController.rendimento);
			float time = Random.Range(levelData.intervaloTempo[0],levelData.intervaloTempo[1]);
			yield return new WaitForSeconds(time);
			int i = Random.Range(0,criadoresObj.Count);
			criadoresObj[i].CriaObjeto();
		}
		if(playerController.rendimento > 0){
			print("ganhou");
		}else if(tempoRestante <= 0){
			print("perdeu");
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
		levelLoop = InitializeLoop();
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
