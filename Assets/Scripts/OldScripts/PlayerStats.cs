using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public int currentLevel;

    public int currentExp;

    public int[] toLevelUp;
    public int[] hpLevels;
    public int[] attackLevels;
    public int[] defenseLevels;

    public int currentHp;
    public int currentAttackLevel;
    public int currentDefLevel;

    private PlayerHealthManager pHealth;

	// Use this for initialization
	void Start () {
        currentHp = hpLevels[1];
        currentAttackLevel = attackLevels[1];
        currentDefLevel = defenseLevels[1];

        pHealth = FindObjectOfType<PlayerHealthManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if(currentExp >= toLevelUp[currentLevel])
        {
            //currentLevel++;
            LevelUp();
        }
	}

    public void AddExp(int exp)
    {
        currentExp += exp;
    }

    public void LevelUp()
    {
        currentLevel++;
        currentHp = hpLevels[currentLevel];

        pHealth.playerMaxHealth = currentHp;
        pHealth.playerCurrentHealth += currentHp - hpLevels[currentLevel - 1];

        currentAttackLevel = attackLevels[currentLevel];
        currentDefLevel = defenseLevels[currentLevel];
    }
}
