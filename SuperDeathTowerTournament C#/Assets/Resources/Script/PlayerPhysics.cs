using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour {

	public LayerMask collisionMask;

	private BoxCollider collider;
	private Vector3 s;
	private Vector3 c;

	public bool Playercol;
	public GameObject Enemy;



	private float skin = .005f;
	[HideInInspector]
	public bool grounded;
	[HideInInspector]
	public bool MovementStopped;
	Ray ray;
	Ray Raggio;
	RaycastHit Hit;


	void Start(){
		collider = GetComponent<BoxCollider> ();
		s = collider.size;
		c = collider.center;
		Playercol = false;

	}

	//collision check down and up

	public void Move(Vector2 moveAmount){
		float deltaY = moveAmount.y;
		float deltaX = moveAmount.x;
		Vector2 p = transform.position;

		grounded = false;
		for (int i = 0; i < 3; i++) {
			float dir = Mathf.Sign (deltaY);
			float x = (p.x + c.x - s.x / 2) + s.x / 2 * i; //left centre and then rightmost point of collider;
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

			//CHECK COLLISIONE CON ALTRI GIOCATORI PER SMASH()
			Raggio = new Ray (new Vector2 (x, y), new Vector2 (0, dir));
			Debug.DrawRay (ray.origin, ray.direction, Color.red);
			if (Physics.Raycast (Raggio, out Hit, Mathf.Abs (deltaY) + skin) && Hit.transform.gameObject.tag == "Player") {
				Debug.Log ("passato per HIT");
				Playercol = true;
				Enemy = Hit.transform.gameObject;
				//SETTA LA COLLISIONE A TRUE
			}
				
			//CONTROLLA LA DISTANZA TRA I DUE GIOCATORI E SE E' POSSIBILE LO SMASH
			//float dst_enemy = transform.position.x - Hit.transform.position.x;
			//Debug.Log (dst_enemy);

			if (Playercol == true /*&& dst_enemy <= 1*/ && Input.GetMouseButtonDown (0)) {
					//StartCoroutine (Smash());
				Playercol = false;
				Enemy.GetComponent<Rigidbody> ().AddForce (new Vector3 (0, -10, 0));
				Debug.Log ("SMASHED!");
				Enemy = null;
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
		

	private IEnumerator Smash (){
		
		Debug.Log ("Passato per Ienumerator");
		Enemy.GetComponent<Rigidbody> ().AddForce (new Vector3 (0, -10, 0));

		yield return new WaitForSeconds (1);

	}

}
