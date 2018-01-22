using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC {

    [SerializeField]
    private CanvasGroup healthgroup;

    private IState currentState;

    [SerializeField]
    private float InitialAggroRange;

    public float MyAggroRange { get; set; }

    public float MyAttackRange { get; set; }

    public float MyAttackTime { get; set; }

    public Vector3 MyStartPosition { get; set; }

    public bool InRange
    {
        get
        {
            return Vector2.Distance(transform.position, MyTarget.transform.position) < MyAggroRange;
        }
    }
    
    protected void Awake()
    {
        MyStartPosition = transform.position;

        MyAggroRange = InitialAggroRange;

        MyAttackRange = 1;

        ChangeState(new IdleState());
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
}
