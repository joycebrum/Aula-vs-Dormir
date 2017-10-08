using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetosPegaveis : MonoBehaviour {
    public float perda;
    private Vector2 direction;
    private Rigidbody2D rigidbody;
    private int countvoltas;
    public int voltas;
    public float velocity;
    private float time;
    public GameObject Player;
    public bool naopegavel;

	// Use this for initialization
	void Start ()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        countvoltas = 0;
        time = 0;

	}
    public void setDirection(Vector2 directions)
    {
        direction = directions;
    }
	
	// Update is called once per frame
	void Update ()
    {
        rigidbody.MovePosition(rigidbody.position + direction * Time.deltaTime * velocity);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Border")
        {
            countvoltas++;
            direction = -1 * direction;
            if (countvoltas>= voltas)
            {
                if (!naopegavel)
                {
                    ControleRendimento controle = Player.GetComponent<ControleRendimento>();
                    controle.Perda(perda);
                }
                Destroy(gameObject);            
            }
        }
        if (collision.gameObject.tag == "Upp")
        {
            direction.y = -1 * direction.y;
        }

    }
    private void OnMouseDown()
    {
        if(naopegavel)
        {
            ControleRendimento controle = Player.GetComponent<ControleRendimento>();
            controle.Perda(perda);
        }
        Destroy(gameObject);
    }
}
