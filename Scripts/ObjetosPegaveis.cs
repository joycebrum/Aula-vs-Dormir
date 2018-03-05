
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ObjetosPegaveis : MonoBehaviour {
    public float perda;
    private Vector2 direction;
    private Rigidbody2D rigidbodyob;
    private int countvoltas;
    public int voltas;
    public float velocity;
    public GameObject Player;
    public bool naopegavel;

    public bool cafe;
    public float duracao;

    public static bool fim;
	// Use this for initialization
	void Start ()
    {
        rigidbodyob = GetComponent<Rigidbody2D>();
        countvoltas = 0;
	}
    public void setDirection(Vector2 directions)
    {
        direction = directions;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(cafe)
        {
            if(ControleRendimento.cafeEfeito)
            {
                Destroy(gameObject);
            }
            if(ControleRendimento.bebendo)
            {
                Destroy(gameObject);
            }
        }
        rigidbodyob.MovePosition(rigidbodyob.position + direction * Time.deltaTime * velocity);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Border")
        {
            countvoltas++;
            direction = -1 * direction;
            if (countvoltas>= voltas)
            {
                if (!naopegavel && !cafe)
                {
                    ControleRendimento controle = Player.GetComponent<ControleRendimento>();
                    controle.Perda(perda);
                }
                Destroy(gameObject);            
            }
        }
       /* if (collision.gameObject.tag == "Upp")
        {
            direction.y = -1 * direction.y;
        }*/

    }
    private void OnMouseDown()
    {
        if (!fim)
        {
            ControleRendimento controle = Player.GetComponent<ControleRendimento>();
            if (naopegavel)
            {
                controle.Perda(perda);
            }
            if(cafe && !ControleRendimento.bebendo)
            {
                ControleRendimento.timeCafe = duracao;
                ControleRendimento.cafeEfeito = true;
                ControleRendimento.valorCafe = perda;
            }
            Destroy(gameObject);
        }
    }
}
