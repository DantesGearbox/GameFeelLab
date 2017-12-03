using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEmitter : MonoBehaviour {

	public GameObject enemy;
	public ColorChangeOverTime colorChange;


	private float timer = 0.0f;
	private float time = 6.0f;

	public LevelUpBar levelBar;
	private AudioSource audioclip;

	// Use this for initialization
	void Start () {
		levelBar = FindObjectOfType<LevelUpBar> ();
		audioclip = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

		if(levelBar.level >= 2){
			time = 1.5f;
			colorChange.switchFrequency = time / 2.0f;
		}

		if(levelBar.level >= 3){
			time = 0.75f;
			colorChange.switchFrequency = time / 2.0f;
		}

		if(levelBar.level >= 4){
			time = 0.15f;
			colorChange.switchFrequency = time / 2.0f;
		}

		if(levelBar.level >= 5){
			time = 0.05f;
			colorChange.switchFrequency = time / 2.0f;
		}

		timer += Time.deltaTime;
		if(timer > time){
			GameObject enemyHandler = Instantiate(enemy, transform.position, transform.rotation) as GameObject;
			audioclip.Play ();
			timer = 0.0f;
		}
	}
}
