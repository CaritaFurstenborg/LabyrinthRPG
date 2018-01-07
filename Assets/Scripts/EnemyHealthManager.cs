using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour {

    public int enemyMaxHealth;
    public int enemyCurrentHealth;
    public int expToGive;

    private PlayerStats playerStats;

    // Use this for initialization
    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;

        playerStats = FindObjectOfType<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCurrentHealth <= 0)
        {
            gameObject.SetActive(false);
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
