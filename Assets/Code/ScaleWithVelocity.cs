using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithVelocity : MonoBehaviour {

	Rigidbody2D rb;
	PlayerLocomotion pl;

	private float maxXSpeed;
	private float maxYSpeed = 0.0f;
	private float minScaleX = 1.0f;
	private float maxScaleX = 1.2f;
	private float minScaleY = 1.0f;
	private float maxScaleY = 2.0f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		pl = GetComponent<PlayerLocomotion> ();
		maxXSpeed = pl.GetMaxXSpeed ();
	}
	
	// Update is called once per frame
	void Update () {

		//We don't know what the maxJumpVelocity will be so we just update it to be the highest we've seen
		if(Mathf.Abs(rb.velocity.y) > maxYSpeed){
			maxYSpeed = Mathf.Abs (rb.velocity.y);
		}

		//How lerped will the scale be
		float xScale = Mathf.Abs (rb.velocity.x) / maxXSpeed;
		float yScale = Mathf.Abs (rb.velocity.y) / maxYSpeed;
		//Debug.Log ("xScale: " + xScale + ", yScale: " + yScale);

		transform.localScale = new Vector2 (Mathf.Lerp (minScaleX, maxScaleX, xScale), Mathf.Lerp (minScaleY, maxScaleY, yScale));
	}
}
