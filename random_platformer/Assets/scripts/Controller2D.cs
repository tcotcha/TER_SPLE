using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class Controller2D : MonoBehaviour {

	const float skinWidth = 0.015f;
	public int horizontalRayCount = 4;
	public int verticalRayCount = 4;
	float horizontalRaySpacing;
	float verticalRaySpacing;
	BoxCollider2D collider;
	RaycastOrigins raycastOrigins;
	public CollisionInfo collisions;

	public LayerMask collisionMask;

	void Start () {
		collider = GetComponent<BoxCollider2D>();
		CalculateRaySpacing ();

	}

	void Update () {
		
	}

	public void Move(Vector3 velocity){
		UpdateRaycastOrigins ();
		collisions.reset ();
		if (velocity.x != 0) {
			HorizontalCollision (ref velocity);
		}
		if (velocity.y != 0) {
			VerticalCollision (ref velocity);
		}
		transform.Translate (velocity);
	}

	public void VerticalCollision(ref Vector3 velocity){
		float directionY = Mathf.Sign (velocity.y);
		float rayLenght = Mathf.Abs (velocity.y) + skinWidth;
		for (int i = 0; i < verticalRayCount; i++) {
			Vector2 rayOrigin;
			if(directionY == -1){
				rayOrigin = raycastOrigins.bottomLeft;
			}else{
				rayOrigin = raycastOrigins.topLeft;
			}
			rayOrigin += Vector2.right * (verticalRaySpacing * i +velocity.x);
			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.up * directionY, rayLenght, collisionMask);

			Debug.DrawRay (rayOrigin, Vector2.up * directionY * rayLenght, Color.red);

			if (hit) {
				velocity.y = (hit.distance - skinWidth) * directionY;
				rayLenght = hit.distance;

				collisions.above = directionY == 1;
				collisions.below = directionY == -1;
			}
		}
	}

	public void HorizontalCollision(ref Vector3 velocity){
		float directionX = Mathf.Sign (velocity.x);
		float rayLenght = Mathf.Abs (velocity.x) + skinWidth;
		for (int i = 0; i < horizontalRayCount; i++) {
			Vector2 rayOrigin;
			if(directionX == -1){
				rayOrigin = raycastOrigins.bottomLeft;
			}else{
				rayOrigin = raycastOrigins.bottomRight;
			}
			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.right * directionX, rayLenght, collisionMask);

			Debug.DrawRay (rayOrigin, Vector2.right * directionX * rayLenght, Color.red);

			if (hit) {
				velocity.x = (hit.distance - skinWidth) * directionX;
				rayLenght = hit.distance;

				collisions.left = directionX == -1;
				collisions.right = directionX == 1;
			}
		}
	}

	void UpdateRaycastOrigins(){
		Bounds bound = collider.bounds;
		bound.Expand (skinWidth * -2);
		raycastOrigins.bottomLeft = new Vector2(bound.min.x,bound.min.y);
		raycastOrigins.bottomRight = new Vector2(bound.max.x, bound.min.y);
		raycastOrigins.topLeft = new Vector2(bound.min.x,bound.max.y);
		raycastOrigins.topRight = new Vector2(bound.max.x,bound.max.y);
	}

	void CalculateRaySpacing(){
		Bounds bound = collider.bounds;
		bound.Expand (skinWidth * -2);

		horizontalRayCount = Mathf.Clamp (horizontalRayCount, 2, int.MaxValue);
		verticalRayCount = Mathf.Clamp (verticalRayCount, 2, int.MaxValue);

		horizontalRaySpacing = bound.size.y / (horizontalRayCount - 1);
		verticalRaySpacing = bound.size.x / (verticalRayCount - 1);
	}

	struct RaycastOrigins{
		public Vector2 topLeft, topRight;
		public Vector2 bottomLeft, bottomRight;
	}

	public struct CollisionInfo{
		public bool above, below;
		public bool left, right;

		public void reset() {
			above = below = left = right = false;
		}
	}
}
