using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObjects : MonoBehaviour {
	public int damage = 40;
	[SerializeField] private MovementType movementType;
	[SerializeField] private int direction;
    public float velocity;

	const float waveHeight = 7;
	const float waveDuration = 10;

	private Rigidbody2D rb;
	public void SetVelocity(float v){
		velocity = v;
	}
	public void SetDirection(int d){
		direction = d;
	}
	public void SetMovementType(int m){
		switch(m){
			case 0:
				movementType = MovementType.LINE;
				break;
			case 1:
				movementType = MovementType.WAVE;
				break;
		}
	}
	private void Start(){
		rb = GetComponent<Rigidbody2D>();
	}
	private void Update(){
		switch(movementType){
			case MovementType.LINE:
				LineMovement();
				break;
			case MovementType.WAVE:
				WaveMovement();
				break;
		}
	}
	private void LineMovement(){
		rb.velocity = Vector2.right*direction*velocity;
	}
	private void WaveMovement(){
		rb.velocity = new Vector3(direction*velocity, Mathf.Cos(Time.time*waveDuration)*waveHeight, 0);
	}
}
public enum MovementType{
	LINE,WAVE
}
