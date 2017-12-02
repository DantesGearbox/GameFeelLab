using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEmitter : MonoBehaviour {

	public GameObject enemy;

	private float timer = 0.0f;
	private float time = 2.0f;

	public LevelUpBar levelBar;

	// Use this for initialization
	void Start () {
		levelBar = FindObjectOfType<LevelUpBar> ();
	}
	
	// Update is called once per frame
	void Update () {

		if(levelBar.level >= 2){
			time = 1.5f;
		}

		if(levelBar.level >= 3){
			time = 1.0f;
		}

		if(levelBar.level >= 4){
			time = 0.5f;
		}

		if(levelBar.level >= 5){
			time = 0.1f;
		}

		timer += Time.deltaTime;
		if(timer > time){
			GameObject enemyHandler = Instantiate(enemy, transform.position, transform.rotation) as GameObject;
			timer = 0.0f;
		}
	}
}
