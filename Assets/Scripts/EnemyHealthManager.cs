using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour {

    public string enemyNameSet;
    public int enemyMaxHealth;
    public int enemyCurrentHealth;
    public int expToGive;

    public string questEnemyName;
    public Slider hpBar;
    public Text enemyName;
    
    //public Vector3 enemyPos;
    //public Quaternion enemyRot;

    private PlayerStats playerStats;
    private MonsterSpawner monSpaw;

    // Use this for initialization
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        monSpaw = FindObjectOfType<MonsterSpawner>();
        
        enemyCurrentHealth = enemyMaxHealth;
        enemyName.text = enemyNameSet;      
    }

    // Update is called once per frame
    void Update()
    {        
        hpBar.maxValue = enemyMaxHealth;
        hpBar.value = enemyCurrentHealth;

        if (enemyCurrentHealth <= 0)
        {
            //enemyPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            //enemyRot = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
            QuestManager.questManager.AddQuestObjective(questEnemyName, 1);
            Destroy(gameObject);        
            playerStats.AddExp(expToGive);
            monSpaw.exists = false;
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
