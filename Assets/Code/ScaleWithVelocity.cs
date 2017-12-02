using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithVelocity : MonoBehaviour {

	Rigidbody2D rb;
	PlayerLocomotion pl;
	BoxCollider2D bc;

	private float maxXSpeed = 0.0f;
	private float maxYSpeed = 0.0f;

//	private float minScaleX = 1.0f;
//	private float maxScaleX = 1.5f;
//	private float minScaleY = 1.0f;
//	private float maxScaleY = 2.0f;
//
	private float minScaleX = 0.4f;
	private float maxScaleX = 1.0f;
	private float minScaleY = 0.8f;
	private float maxScaleY = 1.0f;

	// Use this for initialization
	void Start () {
		//Debug.Log ("minScaleX: " + minScaleX + ", maxScaleX: " + maxScaleX + ", minScaleY: " + minScaleY + ", maxScaleY: " + maxScaleY);

		rb = GetComponent<Rigidbody2D> ();
		pl = GetComponent<PlayerLocomotion> ();
		//bc = GetComponent<BoxCollider2D> ();

		maxXSpeed = pl.GetMaxXSpeed ();
	}
	
	// Update is called once per frame
	void Update () {

		updateMaxYSpeed ();

		//How lerped will the scale be
		float xScale = Mathf.Abs (rb.velocity.x) / maxXSpeed;
		float yScale = Mathf.Abs (rb.velocity.y) / maxYSpeed;
		float xLerp = Mathf.Lerp (maxScaleX, minScaleX, yScale);
		float yLerp = Mathf.Lerp (maxScaleY, minScaleY, xScale);

		//if(!pl.onGround){
		//			
		//	yLerp = 1.0f;
		//}

		transform.localScale = new Vector2 (xLerp, yLerp);
	}

	void updateMaxYSpeed(){
		//We don't know what the maxJumpVelocity will be so we just update it to be the highest we've seen
		if(Mathf.Abs(rb.velocity.y) > maxYSpeed){
			maxYSpeed = Mathf.Abs (rb.velocity.y);
		}
	}
}
