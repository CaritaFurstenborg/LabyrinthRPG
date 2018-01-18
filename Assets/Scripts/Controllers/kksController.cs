using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class kksController : Character {
    // Monster walk zone bounds
    public Collider2D moveZone; // IS NOT WORKING YET! (movement controllers has nothing)

    private Vector2 minMovePoint;
    private Vector2 maxMovePoint;

    public Transform target;

    public float moveSpeed; // Enemy movement speed

    public float playerRespawnTimer; // Respawn timer for player (when dead)
    //Timers
    public float timeBetweenMove; // Enemy randomized movement wait timer
    public float timeToMove; // Enemy randomized movement time timer
    //Timer counters
    private float timeBetweenMoveCount; // counter for enemy movemnet wait timer
    private float timeToMoveCount; // counter for enemy movement time 

    private Rigidbody2D mRigBody; // Enemy rigid body component
    private Vector3 moveDirection; // enemy move direction (randomized)

    //Player related vars
    private bool pRespawning; // check if player is respawning after defeat
    private GameObject player; // player gameobject



	// Use this for initialization
	protected override void Start () {
        mRigBody = GetComponent<Rigidbody2D>();
        moveZone = GameObject.Find("MoveZones").transform.Find("kksZone").gameObject.GetComponent<Collider2D>();        

        timeBetweenMoveCount = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
        timeToMoveCount = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f);

        minMovePoint = moveZone.bounds.min;
        maxMovePoint = moveZone.bounds.max;
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {

        FollowTarget();        

        if (isMoving)
        {
            timeToMoveCount -= Time.deltaTime; // countdown for movement
            mRigBody.velocity = moveDirection; // set movement

            if(timeToMoveCount < 0f) // movement timer expired
            {
                timeBetweenMoveCount = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f); // set a random wait time for next movement
            }

        } else
        {
            mRigBody.velocity = Vector2.zero; // stop all movement

            timeBetweenMoveCount -= Time.deltaTime;

            if(timeBetweenMoveCount < 0) // wait time expired
            {
                timeToMoveCount = Random.Range(timeToMove * 0.75f, timeToMove * 1.25f); //set random movement time

                moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed, 0f); //set random movement direction
            }
        }        

        if(pRespawning)
        {
            playerRespawnTimer -= Time.deltaTime;
            if(playerRespawnTimer < 0f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //load currently active scene
                player.SetActive(true);
            }
        }
	}

    public void FollowTarget()
    {
        if(target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }

   
}
