using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour {

	private Rigidbody2D rigidbody2d;
	private PlayerInput playerInput;

	//Controller buttons
	public enum BUTTONS {Cross, Circle, Triangle, Square, DPadUp, DPadDown, DPadLeft, DPadRight, RightShoulder, LeftShoulder, Start, Select, LeftStick, RightStick };
	public enum AXIS { StickLeftX, StickLeftY, StickRightX, StickRightY, TriggerLeft, TriggerRight };

	//Controls
	public BUTTONS jumpButton = BUTTONS.Cross;
	public AXIS xMovementAxis = AXIS.StickLeftX;
	public bool keyboardControls = false;
	public KeyCode jumpKey = KeyCode.Space;
	public KeyCode leftMovement = KeyCode.LeftArrow;
	public KeyCode rightMovement = KeyCode.RightArrow;

	//Movement variables
	private float accelerationTime = 0.1f;
	private float deccelerationTime = 0.05f;
	private float directionSwitchTime = 0.1f;
	private float movespeed = 20.0f;
	private float directionSwitchLowerSpeedLimit = 5.0f;

	//Jumping variables
	private float maxJumpHeight = 8.0f;
	private float minJumpHeight = 1.0f;
	private float timeToJumpApex = 0.42f;
	private float airAccelerationTime = 0.15f;
	private float airDeccelerationTime = 0.125f;
	private float airDirectionSwitchTime = 0.05f;

	/* SUPER MEAT BOY VARIABLES
	//Movement variables
	private float accelerationTime = 0.1f;
	private float deccelerationTime = 0.05f;
	private float directionSwitchTime = 0.1f;
	private float movespeed = 20.0f;
	private float directionSwitchLowerSpeedLimit = 5.0f;

	//Jumping variables
	private float maxJumpHeight = 8.0f;
	private float minJumpHeight = 1.0f;
	private float timeToJumpApex = 0.42f;
	private float airAccelerationTime = 0.15f;
	private float airDeccelerationTime = 0.125f;
	private float airDirectionSwitchTime = 0.05f;
	*/

	/* ORI AND THE BLIND FOREST VARIABLES
	//Movement variables
	private float accelerationTime = 0.2f;
	private float deccelerationTime = 0.2f;
	private float directionSwitchTime = 0.1f;
	private float movespeed = 15.0f;
	private float directionSwitchLowerSpeedLimit = 1.0f;

	//Jumping variables
	private float maxJumpHeight = 10.0f;
	private float minJumpHeight = 3.0f;
	private float timeToJumpApex = 1.2f;
	private float airAccelerationTime = 0.4f;
	private float airDeccelerationTime = 0.4f;
	private float airDirectionSwitchTime = 0.4f;
	*/

	/* HOLLOW KNIGHT VARIABLES
	//Movement variables
	private float accelerationTime = 0.1f;
	private float deccelerationTime = 0.1f;
	private float directionSwitchTime = 0.003f;
	private float movespeed = 15.0f;
	private float directionSwitchLowerSpeedLimit = 5.0f;

	//Jumping variables
	private float maxJumpHeight = 7.0f;
	private float minJumpHeight = 0.25f;
	private float timeToJumpApex = 0.42f;
	private float airAccelerationTime = 0.1f;
	private float airDeccelerationTime = 0.1f;
	private float airDirectionSwitchTime = 0.003f;
	*/

	//Not set by self
	private float gravity;
	private float maxJumpVelocity;
	private float minJumpVelocity;
	private float accelerationSmooth;
	private float deccelerationSmooth;
	private float directionSwitchSmooth;
	private float airAccelerationSmooth;
	private float airDeccelerationSmooth;
	private float airDirectionSwitchSmooth;

	//State variables
	private float xSpeed = 0.0f;
	private float ySpeed = 0.0f;
	private bool jumpIsPressed = false;
	private bool onGround = false;
	private float direction = 0.0f;

	// Use this for initialization
	void Start () {
		rigidbody2d = GetComponent<Rigidbody2D> ();
		playerInput = GetComponent<PlayerInput> ();

		SetupMoveAndJumpSpeed ();
	}

	// Update is called once per frame
	void Update (){
		Jumping ();
		HorizontalMovement ();
	}

	void OnCollisionEnter2D(Collision2D col){
		onGround = true;
		ySpeed = 0;
	}

	void OnCollisionExit2D(Collision2D col){
		onGround = false;
	}

	void HorizontalMovement(){

		float xInput = playerInput.getAxis (xMovementAxis);

		if(keyboardControls){
			if(Input.GetKey (rightMovement)){
				xInput = 1;
			} else if(Input.GetKey (leftMovement)){
				xInput = -1;
			} else {
				xInput = 0;
			}
		}

		//Check for deadzone to not move around on random input
		if(xInput > playerInput.horizontalStickDeadzone || xInput < -playerInput.horizontalStickDeadzone){

			// 1 is to the right, -1 is to the left
			direction = Mathf.Sign (xSpeed);

			//Did we switch directions?
			bool switchedDirections = Mathf.Sign (xInput) != Mathf.Sign (direction);	
			if(xSpeed < directionSwitchLowerSpeedLimit && xSpeed > -directionSwitchLowerSpeedLimit){
				switchedDirections = false;
			}

			//Set speed with acceleration
			float targetSpeed = movespeed * Mathf.Sign (xInput);
			if (!switchedDirections) {
				if(onGround){
					//Normal acceleration
					xSpeed = Mathf.SmoothDamp (xSpeed, targetSpeed, ref accelerationSmooth, accelerationTime);	
				} else {
					//Normal acceleration in air
					xSpeed = Mathf.SmoothDamp (xSpeed, targetSpeed, ref airAccelerationSmooth, airAccelerationTime);
				}
			} else {
				if(onGround){
					//Acceleration on direction switch
					xSpeed = Mathf.SmoothDamp (xSpeed, targetSpeed, ref directionSwitchSmooth, directionSwitchTime);
				} else {
					//Acceleration on direction switch in air
					xSpeed = Mathf.SmoothDamp (xSpeed, targetSpeed, ref airDirectionSwitchSmooth, airDirectionSwitchTime);
				}
			}
		} else {
			if(onGround){
				//Decceleration on no input
				xSpeed = Mathf.SmoothDamp (xSpeed, 0.0f, ref deccelerationSmooth, deccelerationTime);	
			} else {
				//Decceleration on no input
				xSpeed = Mathf.SmoothDamp (xSpeed, 0.0f, ref airDeccelerationSmooth, airDeccelerationTime);
			}
		}

		rigidbody2d.velocity = new Vector2 (xSpeed, rigidbody2d.velocity.y);
	}

	void Jumping(){
		if(keyboardControls){
			if (Input.GetKeyDown (jumpKey)) {
				jumpIsPressed = true;
				ySpeed = 0;
				ySpeed += maxJumpVelocity;
			}
			if (Input.GetKeyUp (jumpKey)) {
				jumpIsPressed = false;
			}
			if(!jumpIsPressed) {
				if(rigidbody2d.velocity.y > minJumpVelocity){
					ySpeed = minJumpVelocity;
				}
			}
		} else {
			if (playerInput.WasButtonPressed (jumpButton)) {
				jumpIsPressed = true;
				ySpeed = 0;
				ySpeed += maxJumpVelocity;
			}
			if (playerInput.WasButtonReleased (jumpButton)) {
				jumpIsPressed = false;
			}
			if(!jumpIsPressed) {
				if(rigidbody2d.velocity.y > minJumpVelocity){
					ySpeed = minJumpVelocity;
				}
			}
		}

		//We apply gravity ourselves, going past Unitys RB gravity
		if(!onGround) ySpeed -= gravity * Time.deltaTime;

		rigidbody2d.velocity = new Vector2 (rigidbody2d.velocity.x, ySpeed);
	}

	void SetupMoveAndJumpSpeed(){
		//Scale accelerations to movespeed
		//accelerationTime = movespeed / accelerationTime;
		//deccelerationTime = movespeed / deccelerationTime;
		//directionSwitchTime = movespeed / directionSwitchTime;

		//Scale gravity and jump velocity to jumpHeights and timeToJumpApex
		gravity = (2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = gravity * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * gravity * minJumpHeight);

		Debug.Log ("Acelleration time: " + accelerationTime + ", Decelleration time: " + deccelerationTime + ", Direction Switch time: " + directionSwitchTime + ", Gravity: " + gravity + ", Min jump velocity: " + minJumpVelocity + ", Max jump velocity: " + maxJumpVelocity);
	}

	public float GetMaxXSpeed(){
		return movespeed;
	}
}
