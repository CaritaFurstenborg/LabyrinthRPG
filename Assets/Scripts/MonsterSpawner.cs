using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

    public GameObject monster;
    public int spawnTimer;
    public bool exists;

    private EnemyHealthManager ehM;
    private kksController kksC;

	// Use this for initialization
	void Start () {
        ehM = FindObjectOfType<EnemyHealthManager>();
        kksC = FindObjectOfType<kksController>();
        monster = Instantiate(monster, transform.position, transform.rotation);
        monster.transform.parent = gameObject.transform;
        exists = true;
	}
	
	// Update is called once per frame
	void Update () {
        //gameObject.transform.position = ehM.enemyPos;
        //gameObject.transform.rotation = ehM.enemyRot;
        //StartCoroutine(MonsterRespawner());
	}

    IEnumerator MonsterRespawner()
    {        
        if (!exists)
        {
            yield return new WaitForSeconds(spawnTimer);
            monster = Instantiate(monster, ehM.enemyPos, ehM.enemyRot);
            exists = true;
        }  
    }
}
