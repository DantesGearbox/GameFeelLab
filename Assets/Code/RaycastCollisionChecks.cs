using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCollisionChecks : MonoBehaviour {

	private struct RaycastOrigins
	{
		public Vector2 botLeft, botRight, topLeft, topRight;
	}

	public struct Collisions
	{
		public bool left, right, top, bot, colliderLeft, colliderRight, colliderTop, colliderBot;
	}

	public LayerMask collisionMask;
	private BoxCollider2D bc;
	private RaycastOrigins raycastOrigins;
	public Collisions collisions;

	private float rayLengthVertical = 0.1f;
	private float rayLengthHorizontal = 0.1f;
	private const float dstBetweenRays = .15f;

	private int horizontalRayCount;
	private int verticalRayCount;
	private float horizontalRaySpacing;
	private float verticalRaySpacing;

	public bool bot, top, left, right = false;

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

		UpdateRaycastOrigins();
		CalculateRaySpacing ();
		UpdateCollisions();
	}

	void OnCollisionEnter2D(Collision2D col){
		//Remember to check for layer mask if it is ground
		collisions.colliderBot = collisions.bot;
		collisions.colliderTop = collisions.top;
		collisions.colliderLeft = collisions.left;
		collisions.colliderRight = collisions.right;
	}

	void OnCollisionExit2D(){
		//Remember to check for layer mask if it is ground
		collisions.colliderBot = false;
		collisions.colliderTop = false;
		collisions.colliderLeft = false;
		collisions.colliderRight = false;
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
		collisions.bot = false;
		collisions.top = false;

		for (int i = 0; i < verticalRayCount; i ++) {

			Vector2 rayOrigin = (directionY == -1.0f) ? raycastOrigins.botLeft : raycastOrigins.topLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLengthVertical, collisionMask);

			Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLengthVertical,Color.red);

			if (hit) {
//				if (hit.distance == 0.0f) {
//					continue;
//				}

				collisions.bot = directionY == -1.0f;
				collisions.top = directionY == 1.0f;
			}
		}
	}

	void HorizontalCollisions(float direction) {
		float directionX = Mathf.Sign (direction);
		collisions.left = false;
		collisions.right = false;

		for (int i = 0; i < horizontalRayCount; i ++) {
			Vector2 rayOrigin = (directionX == -1.0f) ? raycastOrigins.botLeft : raycastOrigins.botRight;
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLengthHorizontal, collisionMask);

			Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLengthHorizontal, Color.red);

			if (hit) {
//				if (hit.distance == 0.0f) {
//					continue;
//				}

				collisions.left = directionX == -1.0f;
				collisions.right = directionX == 1.0f;
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
}
