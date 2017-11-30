using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure; // Required in C#

public class PlayerInput : MonoBehaviour {
	private PlayerIndex playerIndex = PlayerIndex.One;
	private GamePadState state;
	private GamePadState prevState;
	[HideInInspector]
	public float horizontalStickDeadzone = 0.1f;

	// Update is called once per frame
	void Update () {
		prevState = state;
		state = GamePad.GetState(playerIndex);
	}

	public void SetVibration(float leftMotor, float rightMotor){
		GamePad.SetVibration (playerIndex, leftMotor, rightMotor);
	}

	public bool WasButtonPressed(PlayerLocomotion.BUTTONS button){

		switch(button){
		case PlayerLocomotion.BUTTONS.Cross:
			return prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed;

		case PlayerLocomotion.BUTTONS.Circle:
			return prevState.Buttons.B == ButtonState.Released && state.Buttons.B == ButtonState.Pressed;

		case PlayerLocomotion.BUTTONS.Triangle:
			return prevState.Buttons.Y == ButtonState.Released && state.Buttons.Y == ButtonState.Pressed;

		case PlayerLocomotion.BUTTONS.Square:
			return prevState.Buttons.X == ButtonState.Released && state.Buttons.X == ButtonState.Pressed;

		case PlayerLocomotion.BUTTONS.DPadUp:
			return prevState.DPad.Up == ButtonState.Released && state.DPad.Up == ButtonState.Pressed;

		case PlayerLocomotion.BUTTONS.DPadDown:
			return prevState.DPad.Down == ButtonState.Released && state.DPad.Down == ButtonState.Pressed;

		case PlayerLocomotion.BUTTONS.DPadLeft:
			return prevState.DPad.Left == ButtonState.Released && state.DPad.Left == ButtonState.Pressed;

		case PlayerLocomotion.BUTTONS.DPadRight:
			return prevState.DPad.Right == ButtonState.Released && state.DPad.Right == ButtonState.Pressed;

		case PlayerLocomotion.BUTTONS.RightShoulder:
			return prevState.Buttons.RightShoulder == ButtonState.Released && state.Buttons.RightShoulder == ButtonState.Pressed;

		case PlayerLocomotion.BUTTONS.LeftShoulder:
			return prevState.Buttons.LeftShoulder == ButtonState.Released && state.Buttons.LeftShoulder == ButtonState.Pressed;

		case PlayerLocomotion.BUTTONS.Start:
			return prevState.Buttons.Start == ButtonState.Released && state.Buttons.Start == ButtonState.Pressed;

		case PlayerLocomotion.BUTTONS.Select:
			return prevState.Buttons.Back == ButtonState.Released && state.Buttons.Back == ButtonState.Pressed;

		case PlayerLocomotion.BUTTONS.LeftStick:
			return prevState.Buttons.LeftStick == ButtonState.Released && state.Buttons.LeftStick == ButtonState.Pressed;

		case PlayerLocomotion.BUTTONS.RightStick:
			return prevState.Buttons.RightStick == ButtonState.Released && state.Buttons.RightStick == ButtonState.Pressed;

		default:
			Debug.Log ("Couldn't recognise player input: " + button + ".");
			return false;
		}
	}

	public bool WasButtonReleased(PlayerLocomotion.BUTTONS button){

		switch(button){
		case PlayerLocomotion.BUTTONS.Cross:
			return prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Released;

		case PlayerLocomotion.BUTTONS.Circle:
			return prevState.Buttons.B == ButtonState.Released && state.Buttons.B == ButtonState.Released;

		case PlayerLocomotion.BUTTONS.Triangle:
			return prevState.Buttons.Y == ButtonState.Released && state.Buttons.Y == ButtonState.Released;

		case PlayerLocomotion.BUTTONS.Square:
			return prevState.Buttons.X == ButtonState.Released && state.Buttons.X == ButtonState.Released;

		case PlayerLocomotion.BUTTONS.DPadUp:
			return prevState.DPad.Up == ButtonState.Released && state.DPad.Up == ButtonState.Released;

		case PlayerLocomotion.BUTTONS.DPadDown:
			return prevState.DPad.Down == ButtonState.Released && state.DPad.Down == ButtonState.Released;

		case PlayerLocomotion.BUTTONS.DPadLeft:
			return prevState.DPad.Left == ButtonState.Released && state.DPad.Left == ButtonState.Released;

		case PlayerLocomotion.BUTTONS.DPadRight:
			return prevState.DPad.Right == ButtonState.Released && state.DPad.Right == ButtonState.Released;

		case PlayerLocomotion.BUTTONS.RightShoulder:
			return prevState.Buttons.RightShoulder == ButtonState.Released && state.Buttons.RightShoulder == ButtonState.Released;

		case PlayerLocomotion.BUTTONS.LeftShoulder:
			return prevState.Buttons.LeftShoulder == ButtonState.Released && state.Buttons.LeftShoulder == ButtonState.Released;

		case PlayerLocomotion.BUTTONS.Start:
			return prevState.Buttons.Start == ButtonState.Released && state.Buttons.Start == ButtonState.Released;

		case PlayerLocomotion.BUTTONS.Select:
			return prevState.Buttons.Back == ButtonState.Released && state.Buttons.Back == ButtonState.Released;

		case PlayerLocomotion.BUTTONS.LeftStick:
			return prevState.Buttons.LeftStick == ButtonState.Released && state.Buttons.LeftStick == ButtonState.Released;

		case PlayerLocomotion.BUTTONS.RightStick:
			return prevState.Buttons.RightStick == ButtonState.Released && state.Buttons.RightStick == ButtonState.Released;

		default:
			Debug.Log ("Couldn't recognise player input: " + button + ".");
			return false;
		}
	}

	public float getAxis(PlayerLocomotion.AXIS axis){
		switch(axis){
		case PlayerLocomotion.AXIS.StickLeftX:
			return state.ThumbSticks.Left.X;

		case PlayerLocomotion.AXIS.StickLeftY:
			return state.ThumbSticks.Left.Y;

		case PlayerLocomotion.AXIS.StickRightX:
			return state.ThumbSticks.Right.X;

		case PlayerLocomotion.AXIS.StickRightY:
			return state.ThumbSticks.Right.Y;

		case PlayerLocomotion.AXIS.TriggerLeft:
			return state.Triggers.Left;

		case PlayerLocomotion.AXIS.TriggerRight:
			return state.Triggers.Right;

		default:
			Debug.Log ("Couldn't recognise player input: " + axis + ".");
			return 0.0f;
		}
	}
}
