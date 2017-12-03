using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUpBar : MonoBehaviour {

	RectTransform levelUpBar;

	public float xPosition;
	public float xWidth;
	public float maxWidth = 1256.4f;
	public float initialPosition = -21.3f;
	public float totalIncrease = 23.7f;

	private float timer = 0.0f;
	private float resetTime = 7.0f;

	public int level = 1;
	private AudioSource audioclip;
	public AudioClip levelSound;
	public AudioClip percentSound;

	// Use this for initialization
	void Start () {
		levelUpBar = GetComponent<RectTransform> ();
		audioclip = GetComponent<AudioSource> ();
		xPosition = levelUpBar.position.x;
		xWidth = levelUpBar.sizeDelta.x;
	
		//IncreaseBarByPercent (5.0f);
	}

	// Update is called once per frame
	void Update () {
		if(level >= 5){
			timer += Time.deltaTime;
			if(timer > resetTime){
				SceneManager.LoadScene (0);
			}	
		}
	}

	public void IncreaseBarByPercent(float percent){
		//When position increases by 100, width should increase by 50

		Invoke ("stuff", 4.0f);
	}

	private void stuff(){
		float widthIncrease = (maxWidth/100.0f) * 10.2f;
		float positionIncrease = (totalIncrease/100.0f) * 10.2f;

		//Debug.Log ("Width Increase: " + widthIncrease + ", Position Increase: " + positionIncrease);

		levelUpBar.sizeDelta = new Vector2 (levelUpBar.sizeDelta.x + widthIncrease, levelUpBar.sizeDelta.y);
		levelUpBar.position =  new Vector3 (levelUpBar.position.x + positionIncrease, levelUpBar.position.y, levelUpBar.position.z);
		xPosition = levelUpBar.position.x;
		xWidth = levelUpBar.sizeDelta.x;

		if(xWidth > maxWidth){
			levelUpBar.sizeDelta = new Vector2 (0.0f, levelUpBar.sizeDelta.y);
			levelUpBar.position =  new Vector3 (initialPosition, levelUpBar.position.y, levelUpBar.position.z);
			xPosition = initialPosition;
			xWidth = levelUpBar.sizeDelta.x;
			level++;
			audioclip.clip = levelSound;
			audioclip.Play ();
		} else {
			audioclip.clip = percentSound;
			audioclip.Play ();
		}
	}

	public void ResetBar(){

	}
}