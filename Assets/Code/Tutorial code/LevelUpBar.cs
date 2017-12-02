using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpBar : MonoBehaviour {

	RectTransform levelUpBar;

	public float xPosition;
	public float xWidth;
	public float maxWidth = 1256.4f;
	public float initialPosition = -21.3f;
	public float totalIncrease = 23.7f;

	public int level = 1;

	// Use this for initialization
	void Start () {
		levelUpBar = GetComponent<RectTransform> ();
		xPosition = levelUpBar.position.x;
		xWidth = levelUpBar.sizeDelta.x;

		//IncreaseBarByPercent (50.0f);
	}
	
	// Update is called once per frame
	void Update () {
		//IncreaseBarByPercent (10.0f * Time.deltaTime);
	}

	public void IncreaseBarByPercent(float percent){
		//When position increases by 100, width should increase by 50
		float widthIncrease = (maxWidth/100.0f) * percent;
		float positionIncrease = (totalIncrease/100.0f) * (percent);

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
		}
	}

	public void ResetBar(){
		
	}
}
