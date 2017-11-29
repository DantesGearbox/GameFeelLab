using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour {

	private Rigidbody2D rigidbody2d;
	private PlayerInput playerInput;
	public enum BUTTONS {Cross, Circle, Triangle, Square, DPadUp, DPadDown, DPadLeft, DPadRight, RightShoulder, LeftShoulder, Start, Select, LeftStick, RightStick };
	public enum AXIS { StickLeftX, StickLeftY, StickRightX, StickRightY, TriggerLeft, TriggerRight };

	private float accelerationTime = 0.2f;
	private float deccelerationTime = 0.1f;
	private float movespeed = 2.0f;
	private float xSpeed = 0.0f;

	private float horizontalStickDeadzone = 0.1f;

	private Vector2 jumpVector;
	private float minJumpVelocity;
	private float maxJumpHeight = 4.0f;
	private float minJumpHeight = 1.0f;
	private float timeToJumpApex = 0.35f;
	private bool jumpIsPressed = false;

	public BUTTONS jumpButton = BUTTONS.Cross;
	public AXIS xMovementAxis = AXIS.StickLeftX;

	public bool one = false;
	public bool two = false;
	public bool three = false;

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

	void HorizontalMovement(){
		//We control all forces in x position, so we can control it more explicitly, by having an xSpeed
		one = false;
		two = false;
		three = false;

		bool switchedDirections = false;
		float xInput = playerInput.getAxis (xMovementAxis);

		//Check for deadzone to not move around on random input
		if((xInput > horizontalStickDeadzone || xInput < -horizontalStickDeadzone)){
			switchedDirections = Mathf.Sign (xInput) != Mathf.Sign (xSpeed);

			//Pressing a direction, acceleration
			if (!switchedDirections) {
				xSpeed = Mathf.Clamp (xSpeed + (accelerationTime * xInput * Time.deltaTime), -movespeed, movespeed);
				one = true;
			}
		}

		//Doesn't use input, doesn't need to wait for proper input
		//Pressing a direction opposite to movement, decceleration, but goes past zero
		if (switchedDirections) {
			xSpeed = Mathf.Clamp (xSpeed + (-deccelerationTime * Mathf.Sign(xSpeed) * Time.deltaTime), -movespeed, movespeed);
			two = true;
			//Not pressing anything, decceleration, only goes towards zero
		} else {
			xSpeed = Mathf.Lerp (xSpeed, 0, deccelerationTime * Time.deltaTime);
			three = true;
		}

		rigidbody2d.velocity = new Vector2 (xSpeed, rigidbody2d.velocity.y);
	}

	void Jumping(){
		//We want the gravity from the RB, therefore we add directly to RB instead of having a ySpeed
		if (playerInput.WasButtonPressed (jumpButton)) {
			jumpIsPressed = true;
			rigidbody2d.velocity = new Vector2 (rigidbody2d.velocity.x, 0);
			rigidbody2d.velocity += jumpVector;
		}
		if (playerInput.WasButtonReleased (jumpButton)) {
			jumpIsPressed = false;
		}
		if(!jumpIsPressed) {
			if(rigidbody2d.velocity.y > minJumpVelocity){
				rigidbody2d.velocity = new Vector2(0, minJumpVelocity);	
			}
		}
	}

	void SetupMoveAndJumpSpeed(){
		//Max jump height is now in character-heights
		maxJumpHeight *= 15;
		//Min jump height is now in character-heights
		minJumpHeight *= 15;
		//Time to jump apex is now in ~seconds
		timeToJumpApex *= 10;
		//Movespeed is now in ~characters/seconds
		movespeed *= 10;

		//Scale accelerations to movespeed
		accelerationTime = movespeed / accelerationTime;
		deccelerationTime = movespeed / deccelerationTime;

		//Scale gravity and jump velocity to jumpHeight and timeToJumpApex
		rigidbody2d.gravityScale = (2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		jumpVector = new Vector2(0, rigidbody2d.gravityScale * timeToJumpApex);
		minJumpVelocity = Mathf.Sqrt (2 * rigidbody2d.gravityScale * minJumpHeight);
	}
}
