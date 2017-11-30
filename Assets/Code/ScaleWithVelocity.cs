using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithVelocity : MonoBehaviour {

	Rigidbody2D rb;
	PlayerLocomotion pl;
	BoxCollider2D bc;

	private float maxXSpeed;
	private float maxYSpeed = 0.0f;
	private float minScaleX = 1.0f;
	private float maxScaleX = 1.5f;
	private float minScaleY = 1.0f;
	private float maxScaleY = 2.0f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		pl = GetComponent<PlayerLocomotion> ();
		bc = GetComponent<BoxCollider2D> ();

		maxXSpeed = pl.GetMaxXSpeed ();
	}
	
	// Update is called once per frame
	void Update () {

		updateMaxYSpeed ();

		//How lerped will the scale be
		float xScale = Mathf.Abs (rb.velocity.x) / maxXSpeed;
		float yScale = Mathf.Abs (rb.velocity.y) / maxYSpeed;
		float xLerp = Mathf.Lerp (minScaleX, maxScaleX, xScale);
		float yLerp = Mathf.Lerp (minScaleY, maxScaleY, yScale);

		bc.size = new Vector2 (1/xLerp, 1/yLerp);

		//Debug.Log ("xLerp: " + xLerp + ", yLerp: " + yLerp);
		transform.localScale = new Vector2 (xLerp, yLerp);
	}

	void updateMaxYSpeed(){
		//We don't know what the maxJumpVelocity will be so we just update it to be the highest we've seen
		if(Mathf.Abs(rb.velocity.y) > maxYSpeed){
			maxYSpeed = Mathf.Abs (rb.velocity.y);
		}
	}
}
