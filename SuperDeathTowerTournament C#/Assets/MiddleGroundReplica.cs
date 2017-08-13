using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleGroundReplica : MonoBehaviour {
    public GameObject middleGround;
    public Transform spawnPoint;
    private GameObject botta;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MiddleGround")
        {
            botta = collision.gameObject;
            spawnPoint = botta.transform.Find("Spawner");
            GameObject.Instantiate(middleGround, spawnPoint);
            spawnPoint.DetachChildren();
        }
    }

}
