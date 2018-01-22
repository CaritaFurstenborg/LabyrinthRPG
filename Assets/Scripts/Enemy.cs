using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC {

    [SerializeField]
    private CanvasGroup healthgroup;

    private IState currentState;

    private Transform target;

    public Transform Target
    {
        get
        {
            return target;
        }

        set
        {
            target = value;
        }
    }

    protected void Awake()
    {
        ChangeState(new IdleState());
    }

    protected override void Update()
    {
        currentState.Update();

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

    public override void TakeDamage(float dama)
    {
        base.TakeDamage(dama);

        OnHealthChanged(health.MyCurrentValue);
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
}
