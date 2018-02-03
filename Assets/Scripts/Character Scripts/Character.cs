using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Any new character to be created will automatically have rigidBody2D and Animator
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public abstract class Character : MonoBehaviour {

    [SerializeField]
    private float speed;        // Character movespeed

    public Animator MyAnimator { get; set; }        //Character animator

    private Vector2 direction;              // Character direction

    private Rigidbody2D myRigidBody;        // Rigidbody for animator

    public bool IsMele { get; set; }        // Defines if attack shound be mele (needed for animator)

    protected Coroutine attackRoutine;      // 

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
    // Using fixed update because moving a rigid body
    private void FixedUpdate()
    {
        Move();
    }
    // handles character move
    public void Move()
    {
        if(IsAlive)
        {
            myRigidBody.velocity = MyDirection.normalized * MySpeed;
        }                   
    } 
    // Handles switching to correct animation layer based on bools
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
                if(MyAnimator.gameObject.tag == "Player")
                {
                    MyAnimator.SetBool("isMoving", true);
                }
                
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
                if (MyAnimator.gameObject.tag == "Player")
                {
                    MyAnimator.SetBool("isMoving", false);
                }
            }
        }
        else
        {
            ActivateLayer("DeathLayer");
        }
    }
    // Activates animation layer by name
    public void ActivateLayer(string layerName)     
    {
        for(int i = 0; i < MyAnimator.layerCount; i++)
        {
            if(MyAnimator == null)
            {
                Debug.Log("Animator not found");
            }
            MyAnimator.SetLayerWeight(i, 0);
        }

        MyAnimator.SetLayerWeight(MyAnimator.GetLayerIndex(layerName), 1);
    }
    // Take damage method for controlling health loss and death and setting animator trigger to die
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
