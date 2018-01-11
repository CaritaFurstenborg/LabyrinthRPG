using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {

    public int enemyMaxHealth;
    public int enemyCurrentHealth;
    public int expToGive;

    public string questEnemyName;

    private PlayerStats playerStats;
    //private QuestManager qm;      Old qm

    // Use this for initialization
    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;

        playerStats = FindObjectOfType<PlayerStats>();
        //qm = FindObjectOfType<QuestManager>();    Old qm
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCurrentHealth <= 0)
        {
            //qm.enemyKilled = questEnemyName;   Old qm
            Destroy(gameObject);
            playerStats.AddExp(expToGive);
        }
    }

    public void HurtEnemy(int damageToTake)
    {
        enemyCurrentHealth -= damageToTake;
    }

    public void HealEnemy(int damageToHeal)
    {
        enemyCurrentHealth += damageToHeal;
    }

    public void SetMaxHealth()
    {
        enemyCurrentHealth = enemyMaxHealth;
    }
}
