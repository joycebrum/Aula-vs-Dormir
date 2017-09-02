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
    public GameObject Player;
    private ControleRendimento controlerend;
	// Use this for initialization
	void Start ()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        countvoltas = 0;
        controlerend = Player.GetComponent<ControleRendimento>();

	}
    public void setDirection(Vector2 directions)
    {
        direction = directions;
        Debug.Log(direction);
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
            print(countvoltas + voltas);
            direction = -1 * direction;
            if (countvoltas>= voltas)
            {
                controlerend.Perda(perda);
                Destroy(gameObject);
            }
        }
        
    }
    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
