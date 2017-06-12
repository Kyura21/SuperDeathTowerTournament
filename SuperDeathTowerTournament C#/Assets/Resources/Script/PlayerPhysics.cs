using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour {

	public LayerMask collisionMask;

	private BoxCollider collider;
	private Vector3 s;
	private Vector3 c;

	private float skin = .005f;
	[HideInInspector]
	public bool grounded;
	[HideInInspector]
	public bool MovementStopped;
	Ray ray;
	RaycastHit Hit;


	void Start(){
		collider = GetComponent<BoxCollider> ();
		s = collider.size;
		c = collider.center;

	}

	//collision check down and up

	public void Move(Vector2 moveAmount){
		float deltaY = moveAmount.y;
		float deltaX = moveAmount.x;
		Vector2 p = transform.position;

		grounded = false;
		for (int i = 0; i < 3; i++) {
			float dir = Mathf.Sign (deltaY);
			float x = (p.x + c.x - s.x / 2) + s.x / 2 * i; //left centre anda then rightmost point of collider;
			float y = p.y + c.y + s.y / 2 * dir; //bottom of collider;

			ray = new Ray (new Vector2 (x, y), new Vector2 (0, dir));
			Debug.DrawRay (ray.origin, ray.direction);
			if (Physics.Raycast (ray, out Hit, Mathf.Abs (deltaY) + skin, collisionMask)) {
				// Get distance between player and ground
				float dst = Vector3.Distance (ray.origin, Hit.point);

				//stop player's downwards movement after coming within skin width of a collider

				if (dst > skin) {
					deltaY = -dst - skin * dir;// dst * dir + skin;
				} else {
					deltaY = 0;
				}
				grounded = true;
				break;
			}
		}
			//Collision check left & right
			MovementStopped = false;
			for (int i = 0; i < 3; i++) {
			float dir = Mathf.Sign (deltaX);
			float x = p.x + c.x + s.x/2 * dir; //left centre and then rightmost point of collider;
			float y = p.y + c.y - s.y/2 + s.y/2 * i;

				ray = new Ray (new Vector2 (x, y), new Vector2(dir,0));
				Debug.DrawRay (ray.origin, ray.direction);
				if (Physics.Raycast (ray, out Hit, Mathf.Abs (deltaX) + skin, collisionMask)) {
					// Get distance between player and ground
					float dst = Vector3.Distance(ray.origin, Hit.point);

					//stop player's downwards movement after coming within skin width of a collider

					if (dst > skin) {
						deltaX = dst* dir - skin * dir;// dst * dir + skin;
					} else {
						deltaX = 0;
					}
					MovementStopped = true;
					break;
				}

		}
		Vector3 PlayerDir = new Vector3 (deltaX, deltaY);
		Vector3 o = new Vector3 (p.x + c.x + s.x / 2 * Mathf.Sign (deltaX), p.y + c.y + s.y / 2 * Mathf.Sign (deltaY));
		Debug.DrawRay (o, PlayerDir.normalized);
		Vector2 finaltransform = new Vector2 (deltaX, deltaY);
		transform.Translate (finaltransform);
	}


}
