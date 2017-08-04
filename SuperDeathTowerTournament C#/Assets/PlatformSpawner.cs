using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {

    private int n1;
    private int n2;

    private Transform[] board;
    public GameObject piattaforma;
    private GameObject lift;

    public bool MOVING;
    public float speed;
    public float tempo;

    // Use this for initialization
    void Start () {

        board = transform.GetComponentsInChildren<Transform>();
        lift = transform.parent.gameObject;

        InvokeRepeating("spawn", tempo, tempo);
    }
	
	// Update is called once per frame
	void Update () {

        if (MOVING)
        {
            lift.transform.position += new Vector3(0, speed, 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            
        }
	}

    void spawn()
    {
        n1 = Random.Range(1, 5);
        n2 = Random.Range(1, 5);

        while (n1 == n2)
        {
            n2 = Random.Range(1, 5);
        }


        if (Random.value > 0.5f)
        {
            for (int i = 1; i < 6; i++)
            {
                if (i != n1 && i != n2)
                {
                    GameObject.Instantiate(piattaforma, board[i].position, Quaternion.identity);
                }
            }

        }
        else
        {
            GameObject.Instantiate(piattaforma, board[n1].position, Quaternion.identity);
            GameObject.Instantiate(piattaforma, board[n2].position, Quaternion.identity);
        }
        print(n1 + " " + n2);
    }
    
}
