using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC {

    [SerializeField]
    private CanvasGroup healthgroup;

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
}
