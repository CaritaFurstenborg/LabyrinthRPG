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

    private Vector2 direction; // inheriting objects can access

    private Rigidbody2D myRigidBody;

    protected bool isAttacking = false;

    public bool IsMele { get; set; }

    protected Coroutine attackRoutine;

    [SerializeField]
    protected Transform hitBox;

    [SerializeField]
    protected Stats health;

    public Stats MyHealth
    {
        get { return health; }
    }

    [SerializeField]
    private float initialHealth;

    public bool isMoving
    {
        get
        {
            return (MyDirection.x != 0 || MyDirection.y != 0);            
        }
    } // Property bool for checking if character is moving

    public Vector2 MyDirection
    {
        get
        {
            return direction;
        }

        set
        {
            direction = value;
        }
    }

    public float MySpeed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    // Use this for initialization
    protected virtual void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        health.Initialize(initialHealth, initialHealth);
        //resource.Initialize(initialResource, maxResource);     //   FIX THIS LATER!!!
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
        myRigidBody.velocity = MyDirection.normalized * MySpeed;             
    } 

    public void HandleLayers()
    {
        if (isMoving)
        {
            ActivateLayer("WalkLayer");

            animator.SetFloat("MoveX", MyDirection.x);
            animator.SetFloat("MoveY", MyDirection.y);
            animator.SetFloat("LastMoveX", MyDirection.x);
            animator.SetFloat("LastMoveY", MyDirection.y);
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
    
    public virtual void TakeDamage(float dama)
    {
        health.MyCurrentValue -= dama;

        if(health.MyCurrentValue <= 0)
        {
            animator.SetTrigger("Die");
        }
    } 
}
