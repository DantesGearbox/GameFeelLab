using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyAI : MonoBehaviour {

	private Rigidbody2D rb;
	private PlayerLocomotion pl;
	public GameObject convergenceObject;

	private float minXSpeed = -20.0f;
	private float maxXSpeed = 20.0f;
	private float minYSpeed = -20.0f;
	private float maxYSpeed = 20.0f;

	private float xAcceleration = 5.0f;

	private float xSpeed;
	private float ySpeed;

	public LevelUpBar levelBar;
	public AudioSource audioclip;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		pl = FindObjectOfType<PlayerLocomotion> ();
		audioclip = GetComponent<AudioSource> ();
		levelBar = FindObjectOfType<LevelUpBar> ();
		rb.velocity = new Vector2 (Random.Range (minXSpeed, maxXSpeed), Random.Range (minYSpeed, maxYSpeed));
		xSpeed = rb.velocity.x;
		ySpeed = rb.velocity.y;
	}

	void OnCollisionEnter2D(Collision2D col){
		rb.velocity = new Vector2 (xSpeed, -ySpeed);
		xSpeed = rb.velocity.x;
		ySpeed = rb.velocity.y;
	}

	void Update(){

		//float diffToPlayer = pl.transform.position.x - transform.position.x;
		Vector2 vecToPlayer = pl.transform.position - transform.position;
		vecToPlayer = vecToPlayer.normalized * xAcceleration * Time.deltaTime;

		xSpeed += vecToPlayer.x;
		ySpeed += vecToPlayer.y;

		rb.velocity = new Vector2 (xSpeed, ySpeed);
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.tag == "Sword"){
			//Debug.Log ("Hit by sword!");

			levelBar.IncreaseBarByPercent (10.2f);

			//Debug.Log ("Played audio.");
			for(int i = 0 ; i < 20 ; i++){
				GameObject handler = Instantiate(convergenceObject, transform.position, transform.rotation) as GameObject;
				Destroy (handler, 4.0f);
			}

			Destroy (this.gameObject);
		}
	}
}
