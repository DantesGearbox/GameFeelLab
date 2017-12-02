using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    You could just check the distance to enemies and count them as hit when they are too close during a swing.
    You could place a trigger collider roughly where the sword is and activate it during the swing.
*/

/*
	Might need an input-queue to make this feel good, sometimes there are dropped inputs on repeated sword swings.
	The sword doesn't follow the player while moving.
	The sword is missplaced.
	We could use a better color.
*/

public class SwordSwinging : MonoBehaviour {

	public GameObject swordSwing;

	//private PlayerLocomotion playerLocomotion;
	private PlayerInput playerInput;
	public PlayerLocomotion.BUTTONS swordSwingButton = PlayerLocomotion.BUTTONS.Square;
	public PlayerLocomotion.AXIS xMovementAxis = PlayerLocomotion.AXIS.StickLeftX;
	public PlayerLocomotion.AXIS yMovementAxis = PlayerLocomotion.AXIS.StickLeftY;

	private Vector3 swordSwingOffset = new Vector3 (1.625f, 0.0f, -2.0f);
	private Vector3 rotationOffset = new Vector3 (0, 0, 0);

	private bool swingInProgress;
	private float swingProgressTimer = 0.0f;
	private bool swingOnCooldown;
	private float swingCooldownTimer = 0.0f;

	private float swingCooldownTime = 0.2f;
	private float swingProgressTime = 0.1f;

	private LevelUpBar levelBar;
	private bool disableSwordSwinging = false;

	private GameObject swordHandler;

	// Use this for initialization
	void Start (){
		//playerLocomotion = GetComponent<PlayerLocomotion> ();
		playerInput = GetComponent<PlayerInput> ();
		levelBar = FindObjectOfType<LevelUpBar> ();
	}
	
	// Update is called once per frame
	void Update () {

		//Level 1: normal
		//Level 2: disable wallstuff
		//Level 3: disable jumping
		//Level 4: disable moving
		//Level 5: disable swordswing

		if(levelBar.level >= 5){
			disableSwordSwinging = true;
		}

		float xInput = playerInput.getAxis (xMovementAxis);
		float yInput = playerInput.getAxis (yMovementAxis);

		if(swingInProgress){
			swordHandler.transform.position = transform.position + swordSwingOffset;
			swingProgressTimer += Time.deltaTime;
			if(swingProgressTimer > swingProgressTime){
				swingInProgress = false;
				swingProgressTimer = 0.0f;
				Destroy(swordHandler);
			}
		}

		if(swingOnCooldown){
			swingCooldownTimer += Time.deltaTime;
			if(swingCooldownTimer > swingCooldownTime){
				swingOnCooldown = false;
				swingCooldownTimer = 0.0f;
			}
		}

		if(playerInput.WasButtonPressed (swordSwingButton) && !swingInProgress && !swingOnCooldown && !disableSwordSwinging){
			swingInProgress = true;
			swingOnCooldown = true;

			//Debug.Log ("xInput: " + xInput + ", yInput: " + yInput + ", SignX: " + Mathf.Sign (xInput) + ", SignY: " + Mathf.Sign (yInput));

			if(Mathf.Abs (xInput) + 0.15f > Mathf.Abs (yInput) && Mathf.Sign (xInput) == -1.0f){
				//Player input is left.
				rotationOffset = new Vector3 (0, 180, 0);
				swordSwingOffset = new Vector3 (-1.625f, 0.0f, -2.0f);
			}
			if(Mathf.Abs (xInput) + 0.15f > Mathf.Abs (yInput) && Mathf.Sign (xInput) == 1.0f){
				//Player input is right
				rotationOffset = new Vector3 (0, 0, 0);
				swordSwingOffset = new Vector3 (1.625f, 0.0f, -2.0f);
			}
			if(Mathf.Abs (xInput) + 0.15f < Mathf.Abs (yInput) && Mathf.Sign (yInput) == -1.0f){
				//Player input is down
				rotationOffset = new Vector3 (0, 0, 265);
				swordSwingOffset = new Vector3 (-0.04f, -1.44f, -2.0f);
			}
			if(Mathf.Abs (xInput) + 0.15f < Mathf.Abs (yInput) && Mathf.Sign (yInput) == 1.0f){
				//Player input is up
				rotationOffset = new Vector3 (0, 0, 85);
				swordSwingOffset = new Vector3 (0.04f, 1.44f, -2.0f);
			}

			//The sword instantiation happens here depends on variables set above
			swordHandler = Instantiate(swordSwing, transform.position, transform.rotation) as GameObject;
			swordHandler.transform.position += swordSwingOffset;
			swordHandler.transform.localRotation = Quaternion.Euler (rotationOffset);
		}
	}
}
