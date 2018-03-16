using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeScript : MonoBehaviour {

    private PlayerController playerController;
    private LevelManager levelManager;
    private ObjectCreator objcreat;

    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject plus;
    [SerializeField] private int duracao;
    private AudioSource goodAudio;

    private void Start()
    {
        goodAudio = GameObject.Find("Audio Source Correct").GetComponent<AudioSource>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        objcreat = GameObject.Find("CafeSpawn").GetComponent<ObjectCreator>();
        objcreat.Coffee = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Border")
        {
            objcreat.Coffee = false;
            var go = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(go, 0.35f);
        }
    }
    private void OnMouseDown()
    {
        print("TomouCafe");
        goodAudio.Play();
        var go = Instantiate(plus, transform.position, Quaternion.identity);
        playerController.UsarCafe(GetComponent<FlyingObjects>().damage, duracao);
        Destroy(go, 1f);
        StartCoroutine(DecreaseScaleThenDie());
    }
    private IEnumerator DecreaseScaleThenDie()
    {
        Destroy(GetComponent<FlyingObjects>());
        Destroy(GetComponent<Rigidbody2D>());
        while (transform.localScale.x > 0.01f)
        {
            transform.localScale = new Vector3(Mathf.MoveTowards(transform.localScale.x, 0, 0.2f),
                                               Mathf.MoveTowards(transform.localScale.y, 0, 0.2f),
                                               1);
            yield return new WaitForEndOfFrame();
        }
        Destroy(this.gameObject);
    }
}
