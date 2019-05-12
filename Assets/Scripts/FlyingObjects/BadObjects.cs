using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadObjects : MonoBehaviour {
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject badClick;
    [SerializeField] private GameObject wrong;
    private AudioSource wrongAudio;
    public int damage;

    private void Start(){
        wrongAudio = GetComponent<AudioSource>();
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
    }
    private void Update(){
        if (Input.touchCount >= 1)
        {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            if (this.gameObject.GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
            {
                wrongAudio.Play();
                var g = Instantiate(wrong, transform.position, Quaternion.Euler(0, 0, 45));
                Destroy(g, 1f);

                playerController.PerderRendimento(GetComponent<FlyingObjects>().damage);
                var pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                pos.z = 0;
                var go = Instantiate(badClick, pos, Quaternion.identity);
                Destroy(go, 1f);
            }
        }
            
    }
}
