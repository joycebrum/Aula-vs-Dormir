using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeScript : MonoBehaviour {
    private PlayerController playerController;
    private ObjectCreator objcreat;

    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject plus;
    [SerializeField] private int duracao;
    [SerializeField] private int efeitoNegativo;
    [SerializeField] private float velocidadeBadEffect;
    private AudioSource goodAudio;

    private void Start()
    {
        goodAudio = GameObject.Find("Audio Source Correct").GetComponent<AudioSource>();
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
    private void Update()
    {
        if(!ObjectCreator.coffee)
        {
            StartCoroutine(DecreaseScaleThenDie());
        }
        else if (Input.touchCount >= 1)
        {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            if (this.gameObject.GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
            {
                goodAudio.Play();
                var goodSign = Instantiate(plus, transform.position, Quaternion.identity);
                playerController.UsarCafe(GetComponent<FlyingObjects>().damage, duracao, efeitoNegativo, velocidadeBadEffect);
                Destroy(goodSign, 1f);
                StartCoroutine(DecreaseScaleThenDie());
            }
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
