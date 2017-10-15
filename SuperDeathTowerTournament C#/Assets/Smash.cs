using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smash : MonoBehaviour {



	void OnCollisionStay2D(Collision2D coll){
		if (coll.gameObject.tag == "Player" && Input.GetMouseButtonDown (0) ){
			coll.rigidbody.AddForce (new Vector2 (0, -10), ForceMode2D.Impulse);
			Debug.Log ("NemicoColpito");
		}
	}
}
