using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCollisionChecks : MonoBehaviour {

	private struct RaycastOrigins
	{
		public Vector2 botLeft, botRight, topLeft, topRight;
	}

	private struct Collisions
	{
		public bool left, right, top, bot, immediateBot;
	}

	public LayerMask collisionMask;
	private BoxCollider2D bc;
	private RaycastOrigins raycastOrigins;
	private Collisions collisions;

	private float rayLengthVertical = 0.1f;
	private float rayLengthHorizontal = 0.1f;
	private const float dstBetweenRays = .15f;

	private int horizontalRayCount;
	private int verticalRayCount;
	private float horizontalRaySpacing;
	private float verticalRaySpacing;

	public bool bot, top, left, right, immediateBot = false;

	// Use this for initialization
	void Start () {
		bc = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update (){

		bot = collisions.bot;
		top = collisions.top;
		left = collisions.left;
		right = collisions.right;
		immediateBot = collisions.immediateBot;

		UpdateRaycastOrigins();
		CalculateRaySpacing ();
		UpdateCollisions();
	}

	private void UpdateRaycastOrigins(){
		Bounds bounds = bc.bounds;

		raycastOrigins.botLeft = new Vector2(bounds.min.x, bounds.min.y);
		raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
		raycastOrigins.botRight = new Vector2(bounds.max.x, bounds.min.y);
		raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
	}

	public void CalculateRaySpacing() {
		Bounds bounds = bc.bounds;

		float boundsWidth = bounds.size.x;
		float boundsHeight = bounds.size.y;

		horizontalRayCount = Mathf.RoundToInt (boundsHeight / dstBetweenRays);
		verticalRayCount = Mathf.RoundToInt (boundsWidth / dstBetweenRays);

		horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
		verticalRaySpacing = bounds.size.x  / (verticalRayCount - 1);
	}

	private void UpdateCollisions(){

		VerticalCollisions (1.0f);
		VerticalCollisions (-1.0f);
		HorizontalCollisions (1.0f);
		HorizontalCollisions (-1.0f);
	}

	void VerticalCollisions(float direction) {
		float directionY = Mathf.Sign (direction);
		collisions.immediateBot = false;

		for (int i = 0; i < verticalRayCount; i ++) {

			Vector2 rayOrigin = (directionY == -1.0f) ? raycastOrigins.botLeft : raycastOrigins.topLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLengthVertical, collisionMask);

			Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLengthVertical,Color.red);

			if (hit) {

				if(!collisions.immediateBot && !collisions.bot && directionY == -1.0f){
					collisions.immediateBot = true;
				}

				collisions.bot = directionY == -1.0f;
				collisions.top = directionY == 1.0f;

			} else {
				if(direction == -1.0f){ collisions.bot = false; }
				if(direction == 1.0f){ collisions.top = false; }
			}
		}
	}

	void HorizontalCollisions(float direction) {
		float directionX = Mathf.Sign (direction);

		for (int i = 0; i < horizontalRayCount; i ++) {
			Vector2 rayOrigin = (directionX == -1.0f) ? raycastOrigins.botLeft : raycastOrigins.botRight;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLengthHorizontal, collisionMask);

			Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLengthHorizontal, Color.red);

			if (hit) {
				collisions.left = directionX == -1.0f;
				collisions.right = directionX == 1.0f;
			} else {
				if(direction == -1.0f){ collisions.left = false; }
				if(direction == 1.0f){ collisions.right = false; }
			}
		}
	}

	public bool Colliding(){
		return collisions.left || collisions.right || collisions.top || collisions.bot;
	}

	public bool CollidingLeftOrRight(){
		return collisions.left || collisions.right;
	}

	public bool OnGround(){
		return collisions.bot;
	}

	public bool GetImmediateBot(){
		return collisions.immediateBot;
	}
}
