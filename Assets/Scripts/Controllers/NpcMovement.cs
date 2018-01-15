using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovement : MonoBehaviour {

    public float moveSpeed; // How fast Npc is going to move
    public float walkTime; // How long npc is going to move
    public float waitTime; // how long wait time for npc to move again

    public Collider2D walkZone; // Npc Movement Field

    public bool canMove;

    private Rigidbody2D myRigBody;
    private bool isMoving;
    private float walkCounter; // how long for npc to move
    private float waitCounter; // how long for npc to wait untill moving again
    private int walkDirection; 

    private Vector2 minMovePoint; // min constraint for Npc Move Field
    private Vector2 maxMovePoint; // max constraint for Npc Move Field
    private bool hasMoveField;


	// Use this for initialization
	void Start () {
        myRigBody = GetComponent<Rigidbody2D>();

        waitCounter = waitTime;
        walkCounter = walkTime;

        ChooseDirection();

        if(walkZone != null) //check npc has walkzone
        {
            minMovePoint = walkZone.bounds.min;
            maxMovePoint = walkZone.bounds.max;
            hasMoveField = true;
        }

        canMove = true;        
	}
	
	// Update is called once per frame
	void Update () {

        if(!QuestUIManager.uiManagerQ.questPanelActive)
        {
            canMove = true;
        }

        if(!canMove)
        {
            myRigBody.velocity = Vector2.zero;
            return;
        }

		if(isMoving)
        {
            switch(walkDirection)
            {
                case 0:
                    myRigBody.velocity = new Vector2(0, moveSpeed);
                    if(hasMoveField && transform.position.y > maxMovePoint.y)
                    {
                        isMoving = false;
                        waitCounter = waitTime;
                    }
                    break;
                case 1:
                    myRigBody.velocity = new Vector2(moveSpeed, 0);
                    if (hasMoveField && transform.position.x > maxMovePoint.x)
                    {
                        isMoving = false;
                        waitCounter = waitTime;
                    }
                    break;
                case 2:
                    myRigBody.velocity = new Vector2(0, -moveSpeed);
                    if (hasMoveField && transform.position.y < minMovePoint.y)
                    {
                        isMoving = false;
                        waitCounter = waitTime;
                    }
                    break;
                case 3:
                    myRigBody.velocity = new Vector2(-moveSpeed, 0);
                    if (hasMoveField && transform.position.x < minMovePoint.x)
                    {
                        isMoving = false;
                        waitCounter = waitTime;
                    }
                    break;
            }

            walkCounter -= Time.deltaTime;

            if (walkCounter < 0)
            {
                isMoving = false;
                waitCounter = waitTime;
            }

        }
        else
        {
            waitCounter -= Time.deltaTime;

            myRigBody.velocity = Vector2.zero;

            if(waitCounter < 0)
            {
                ChooseDirection();
            }
        }
	}

    public void ChooseDirection()
    {
        walkDirection = Random.Range(0, 4);
        isMoving = true;
        walkCounter = walkTime;
    }
}
