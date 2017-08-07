using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlatformSpawner : MonoBehaviour {

    private int n1;
    private int n2;

    private Transform[] board;
    public GameObject piattaforma;
    private GameObject lift;
    private Rigidbody2D rb;

    public bool MOVING;
    public float speed;
    public float tempo;
    public Text conto;
    public AudioClip cd_joint;
    public AudioClip roar;
    
    public AudioSource speaker1;
    public AudioSource speaker2;


    public Vector3 shaker;

    // Use this for initialization
    void Start () {

        board = transform.GetComponentsInChildren<Transform>();
        lift = transform.parent.gameObject;

        rb = lift.GetComponent<Rigidbody2D>();


        StartCoroutine(CountDown());


    }

    // Update is called once per frame
    void Update () {

        if (MOVING)
        {
            InvokeRepeating("spawn", 0, tempo/speed);

            rb.velocity = new Vector3(0, speed, 0);
            MOVING = false;
        }

        
	}

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1);
        conto.enabled = true;
        conto.text = "3";
        speaker1.clip = cd_joint;
        speaker1.volume = 0.5f;
        speaker1.Play();
        
        yield return new WaitForSeconds(1);
        conto.text = "2";
        yield return new WaitForSeconds(1);
        conto.text = "1";
        lift.transform.DOShakePosition(3.0f, shaker, 50);
        speaker2.clip = roar;
        speaker2.Play();

        yield return new WaitForSeconds(1);
        conto.text = "SURVIVE!";
        conto.color = Color.red;
        yield return new WaitForSeconds(2);
        conto.enabled = false;
        MOVING = true;
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
