using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed; // set player move speed

    public float attackTime;

    private Animator anim; //player animation
    private Rigidbody2D mRigB; //player rigidbody component
    private bool isMoving; // check for player moving
    private bool isAttacking;
    private float attackTimeCounter;
    private Vector2 lastMove; // player last move direction

    private static bool playerExists; // check for all instances of players existing

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        mRigB = GetComponent<Rigidbody2D>();

        if(!playerExists) // if no player exist
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject); // don't destroy player
        } else // if player already exists in scene
        {
            Destroy(gameObject); // destroy the duplicated player
        }
	}
	
	// Update is called once per frame
	void Update () {

        isMoving = false; // player default is not moving

        if(!isAttacking)
        { 

		    if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f) {
                //transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
                mRigB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, mRigB.velocity.y);
                isMoving = true; 
                lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            }

            if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
            {
                //transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
                mRigB.velocity = new Vector2(mRigB.velocity.x, Input.GetAxisRaw("Vertical") * moveSpeed);
                isMoving = true;
                lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
            }

            if(Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
            {
                mRigB.velocity = new Vector2(0f, mRigB.velocity.y);
            }

            if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
            {
                mRigB.velocity = new Vector2(mRigB.velocity.x, 0f);
            }

            if (Input.GetKeyDown(KeyCode.J))
            {
                attackTimeCounter = attackTime;
                isAttacking = true;
                mRigB.velocity = Vector2.zero;
                anim.SetBool("isAttacking", true);
            }
        }

        if(attackTimeCounter > 0)
        {
            attackTimeCounter -= Time.deltaTime;
        }

        if(attackTimeCounter <= 0)
        {
            isAttacking = false;
            anim.SetBool("isAttacking", false);
        }

        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("isMoving", isMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
    }
}
