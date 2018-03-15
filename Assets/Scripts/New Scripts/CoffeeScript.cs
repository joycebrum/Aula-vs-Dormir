using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeScript : MonoBehaviour {

    private PlayerController playerController;
    private LevelManager levelManager;

    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject plus;
    [SerializeField] private int duracao;
    private AudioSource goodAudio;

    private void Start()
    {
        goodAudio = GetComponent<AudioSource>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Border")
        {
            var go = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(go, 0.35f);
        }
    }
    private void OnMouseDown()
    {
        if (!playerController.coffeeEffect)
        {
            print("TomouCafe");
            goodAudio.Play();
            var go = Instantiate(plus, transform.position, Quaternion.identity);
            playerController.UsarCafe(GetComponent<FlyingObjects>().damage, duracao);
            Destroy(go, 1f);
            StartCoroutine(DecreaseScaleThenDie());
        }
        else
        {
            print("Cafe ja usado");
        }
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
