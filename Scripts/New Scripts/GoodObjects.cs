using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodObjects : MonoBehaviour {

	private PlayerController playerController;
	[SerializeField] private GameObject explosion;
	[SerializeField] private GameObject plus;

	private void Start(){
		playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
	}
	private void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Border"){
			var go = Instantiate(explosion,transform.position,Quaternion.identity);
			Destroy(go,0.35f);
			playerController.PerderRendimento(GetComponent<FlyingObjects>().damage);
		}
	}
	private void OnMouseDown(){
		var go = Instantiate(plus,transform.position,Quaternion.identity);
		Destroy(go,1f);
		StartCoroutine( DecreaseScaleThenDie() );
	}
	private IEnumerator DecreaseScaleThenDie(){
		Destroy(GetComponent<FlyingObjects>());
		Destroy(GetComponent<Rigidbody2D>());
		while(transform.localScale.x > 0.01f){
			transform.localScale = new Vector3(Mathf.MoveTowards(transform.localScale.x, 0, 0.2f),
											   Mathf.MoveTowards(transform.localScale.y, 0, 0.2f),
											   1);
			yield return new WaitForEndOfFrame();
		}
		Destroy(this.gameObject);
	}
}
