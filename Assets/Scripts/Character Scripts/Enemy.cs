using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC {

    [SerializeField]
    private CanvasGroup healthgroup;        // For ui target frame
    // Enemy State swapping
    private IState currentState;

    [SerializeField]
    private float InitialAggroRange;            // The initial aggro range of enemy

    public float MyAggroRange { get; set; }     // Aggro range to be calculated and set

    public float MyAttackRange { get; set; }        // Range from whitch the enemy can start attacking

    public float MyAttackTime { get; set; }         // Attacktime is set from AttackState script

    public Vector3 MyStartPosition { get; set; }        // Start position that the enemy will return to if evades

    public bool InRange
    {
        get
        {
            return Vector2.Distance(transform.position, MyTarget.transform.position) < MyAggroRange;
        }
    }                           // Check for being in range of target

    private MonsterSpawner monsterSpawner;          // Respawning monster

    private bool goExists;                          // Check if the game object exists for despawn

    private bool notLooted;
    
    protected void Awake()
    {
        monsterSpawner = GetComponentInParent<MonsterSpawner>();        // Find parent object

        MyStartPosition = transform.position;       // Set start position (enemy will return if evades)

        MyAggroRange = InitialAggroRange;           // My aggro range will be calculated based on character attack distance

        MyAttackRange = 1;                          // The range from player that the enemy will start attacking

        ChangeState(new IdleState());               // On start sets to  idle

        goExists = true;                            // As long as the gameobject exists

        notLooted = true;                           // monster has not been looted

    }

    protected override void Update()
    {
        if(IsAlive)                                 
        {
            if (!IsAttacking)
            {
                MyAttackTime += Time.deltaTime;
            }
            
            currentState.Update();            
        }
        else
        {
            if(notLooted)
            {
                LootScript ls = GetComponentInParent<LootScript>();
                ls.CalculateLoot();
                notLooted = false;
            }            

            if(goExists)
            {
                StartCoroutine("DespawnCorpse");
            }            
        }

        base.Update();
    }

    public override Transform Select()
    {
        healthgroup.alpha = 1;

        return base.Select();
    }

    public override void DeSelect()
    {
        healthgroup.alpha = 0;

        base.DeSelect();
    }

    public override void TakeDamage(float dama, Transform source)
    {
        if(!(currentState is EvadeState))
        {
            SetTarget(source);

            base.TakeDamage(dama, source);

            OnHealthChanged(health.MyCurrentValue);
        }        
    }    

    public void ChangeState(IState newState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }

    public void SetTarget(Transform target)
    {
        if(MyTarget == null && !(currentState is EvadeState))
        {
            float distance = Vector2.Distance(transform.position, target.position);
            MyAggroRange = InitialAggroRange;
            MyAggroRange += distance;
            MyTarget = target;
        }
    }

    public void Reset()
    {
        MyTarget = null;
        this.MyAggroRange = InitialAggroRange;
        this.MyHealth.MyCurrentValue = this.InitialHealth;
        OnHealthChanged(health.MyCurrentValue);
    }

    public IEnumerator DespawnCorpse()          // Corpse despawn in 15 secs after death
    {
        yield return new WaitForSeconds(15);
        monsterSpawner.MyExists = false;
        Destroy(gameObject);
        goExists = false;
    }
}
