using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for any characters to use
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour {

    [SerializeField]
    private float speed;

    protected Animator animator;

    protected Vector2 direction; // inheriting objects can access

    private Rigidbody2D myRigidBody;

    protected bool isAttacking = false;

    public bool IsMele { get; set; }

    protected Coroutine attackRoutine;

    [SerializeField]
    protected Transform hitBox;

    public bool isMoving
    {
        get
        {
            return (direction.x != 0 || direction.y != 0);            
        }
    } // Property bool for checking if character is moving

    // Use this for initialization
    protected virtual void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
    // protected virtual allows to override the function in inheriting classes
	protected virtual void Update () {
        HandleLayers();       
	}

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        myRigidBody.velocity = direction.normalized * speed;             
    } 

    public void HandleLayers()
    {
        if (isMoving)
        {
            ActivateLayer("WalkLayer");

            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);
            animator.SetFloat("LastMoveX", direction.x);
            animator.SetFloat("LastMoveY", direction.y);
            animator.SetBool("isMoving", true);
        }
        else if(isAttacking && IsMele)
        {
            ActivateLayer("AttackLayer");
        }
        else if(isAttacking && !IsMele)
        {
            ActivateLayer("RAttackLayer");
        }
        else
        {
            ActivateLayer("IdleLayer");
            animator.SetBool("isMoving", false);
        }
    }

    public void ActivateLayer(string layerName)
    {
        for(int i = 0; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 0);
        }

        animator.SetLayerWeight(animator.GetLayerIndex(layerName), 1);
    }

    public virtual void StopAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", isAttacking);

        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);            
        }        
    }
}
