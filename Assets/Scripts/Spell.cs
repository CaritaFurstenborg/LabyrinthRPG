using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Spell {

    [SerializeField]
    private string name;

    [SerializeField]
    private int damage;

    //[SerializeField]
    //private Sprite icon;

    //[SerializeField]
    //private float spellSpeed;

    [SerializeField]
    private float castTime;

    [SerializeField]
    private GameObject spellPrefab;

    [SerializeField]
    private Color barColor;

    [SerializeField]
    private float maxRange;

    [SerializeField]
    private float coolDown;

    public string MyName
    {
        get
        {
            return name;
        }
    }

    public int MyDamage
    {
        get
        {
            return damage;
        }
    }

    /*public Sprite MyIcon
    {
        get
        {
            return icon;
        }
    }

    public float MySpellSpeed
    {
        get
        {
            return spellSpeed;
        }
    }*/

    public float MyCastTime
    {
        get
        {
            return castTime;
        }
    }

    public GameObject MySpellPrefab
    {
        get
        {
            return spellPrefab;
        }
    }

    public Color MyBarColor
    {
        get
        {
            return barColor;
        }
    }

    public float MyMaxRange
    {
        get
        {
            return maxRange;
        }
    }

    public float MyCoolDown
    {
        get
        {
            return coolDown;
        }
    }
}
