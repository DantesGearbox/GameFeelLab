using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvergingObject : MonoBehaviour {

	public Transform convergence;
	private float timer = 0.0f;
	private float convergenceTime;
	private float minConvergenceTime = 0.9f;
	private float maxConvergenceTime = 1.0f;

	private Vector3 convergencePoint = new Vector3 (0, 18, 0);

	float xSpeed = 0.0f;
	float ySpeed = 0.0f;

	private float minXSpeed = -10.0f;
	private float maxXSpeed = 10.0f;
	private float minYSpeed = -10.0f;
	private float maxYSpeed = 10.0f;
	public AudioSource audioclip;

	// Use this for initialization
	void Start () {

		//float delay = Random.Range (2.6f, 3.4f);
		//float pitch = Random.Range (0.8f, 1.2f);

		//audioclip.pitch = pitch;
		//audioclip.PlayDelayed (delay);
		xSpeed = Random.Range (minXSpeed, maxXSpeed);
		ySpeed = Random.Range (minYSpeed, maxYSpeed);

		convergenceTime = Random.Range (minConvergenceTime, maxConvergenceTime);
	}
	
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;
		if(timer > convergenceTime){
			xSpeed = convergencePoint.x - transform.position.x;
			ySpeed = convergencePoint.y - transform.position.y;
		}


		transform.Translate (new Vector3(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, 0.0f));
	}
}
