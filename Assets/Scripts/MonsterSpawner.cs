using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject monsterPrefab;
    [SerializeField]
    private int spawnTimer;

    [SerializeField]
    private bool exists;

    public bool MyExists
    {
        get
        {
            return exists;
        }

        set
        {
            exists = value;
        }
    }



    // Use this for initialization
    void Start () {
        GameObject monster = Instantiate(monsterPrefab, transform);
        monster.transform.parent = this.transform;
        MyExists = true;
    }
	
	// Update is called once per frame
	void Update () {
        if(!MyExists)
        {
            StartCoroutine(WaitSpawn());              
        }
	}

    IEnumerator WaitSpawn()
    {
        yield return new WaitForSeconds(spawnTimer);

        while(!MyExists)
        {
            GameObject monster = Instantiate(monsterPrefab, transform);
            monster.transform.parent = this.transform;
            MyExists = true;
        }        
    }
}
