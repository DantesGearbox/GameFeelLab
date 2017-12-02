using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	Rigidbody2D rb;
	float timer = 0.0f;
	float directionSwitchTime = 1.0f;
	float direction = 1.0f;
	float ySpeed = 5.0f;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;
		if(timer > directionSwitchTime){
			direction = direction * -1.0f;
			timer = 0.0f;
		}

		rb.velocity = new Vector2 (0.0f, ySpeed * direction);
	}
}
