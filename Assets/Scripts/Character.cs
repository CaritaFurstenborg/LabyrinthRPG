using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for any characters to use
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour {

    [SerializeField]
    private float speed;

    public Animator MyAnimator { get; set; }

    private Vector2 direction; // inheriting objects can access

    private Rigidbody2D myRigidBody;

    public bool IsMele { get; set; }

    protected Coroutine attackRoutine;

    [SerializeField]
    protected Transform hitBox;

    public Transform MyTarget { get; set; }

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

    public bool IsAttacking { get; set; }

    public bool IsAlive
    {
        get
        {
            return MyHealth.MyCurrentValue > 0;
        }
    }

    public float InitialHealth
    {
        get
        {
            return initialHealth;
        }

        set
        {
            initialHealth = value;
        }
    }

    // Use this for initialization
    protected virtual void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        MyAnimator = GetComponent<Animator>();

        health.Initialize(InitialHealth, InitialHealth);
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
        if(IsAlive)
        {
            myRigidBody.velocity = MyDirection.normalized * MySpeed;
        }                   
    } 

    public void HandleLayers()
    {
        if(IsAlive)
        {
            if (isMoving)
            {
                ActivateLayer("WalkLayer");

                MyAnimator.SetFloat("MoveX", MyDirection.x);
                MyAnimator.SetFloat("MoveY", MyDirection.y);
                MyAnimator.SetFloat("LastMoveX", MyDirection.x);
                MyAnimator.SetFloat("LastMoveY", MyDirection.y);
                MyAnimator.SetBool("isMoving", true);
            }
            else if (IsAttacking && IsMele)
            {
                ActivateLayer("AttackLayer");
            }
            else if (IsAttacking && !IsMele)
            {
                ActivateLayer("RAttackLayer");
            }
            else
            {
                ActivateLayer("IdleLayer");
                MyAnimator.SetBool("isMoving", false);
            }
        }
        else
        {
            ActivateLayer("DeathLayer");
        }
    }

    public void ActivateLayer(string layerName)
    {
        for(int i = 0; i < MyAnimator.layerCount; i++)
        {
            MyAnimator.SetLayerWeight(i, 0);
        }

        MyAnimator.SetLayerWeight(MyAnimator.GetLayerIndex(layerName), 1);
    }
    
    public virtual void TakeDamage(float dama, Transform source)
    {
        health.MyCurrentValue -= dama;

        if(health.MyCurrentValue <= 0)
        {
            MyDirection = Vector2.zero;
            myRigidBody.velocity = MyDirection;
            MyAnimator.SetTrigger("Die");
        }
    } 
}
