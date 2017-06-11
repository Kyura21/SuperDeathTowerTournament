using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerPhysics))]
public class Player_Father : MonoBehaviour {

	//player handling
	public float speed = 8;
	public float accelleration = 12;

	private float CurrentSpeed;
	private float TargetSpeed;
	private Vector2 amountToMove;

	private PlayerPhysics playerPhysics;

	// Use this for initialization
	void Start () {
		playerPhysics = GetComponent<PlayerPhysics> ();
	}
	
	// Update is called once per frame
	void Update () {
		TargetSpeed = Input.GetAxisRaw ("Horizontal") * speed;
		CurrentSpeed = IncrementTowards (CurrentSpeed, TargetSpeed, accelleration);

		amountToMove = new Vector2 (CurrentSpeed, 0);
		playerPhysics.Move (amountToMove * Time.deltaTime);
	}

	//increase n towards target by speed
	private float IncrementTowards(float n, float target, float a){
		if (n == target) {
			return n;
		} else {
			float dir = Mathf.Sign (target - n); //must n be increased or decreased to get closer to target
			n += a *Time.deltaTime * dir;
			return(dir == Mathf.Sign (target - n)) ? n : target; // if n has now passed target then return target, otherwise return n
		}


	}

}
