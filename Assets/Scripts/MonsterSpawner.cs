using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

    public GameObject monster;
    public int spawnTimer;
    public bool exists;
    
    

	// Use this for initialization
	void Start () {
        Instantiate(monster, transform.position, transform.rotation);
        exists = true;
    }
	
	// Update is called once per frame
	void Update () {
        if(!exists)
        {
            StartCoroutine(WaitSpawn());              
        }
	}

    IEnumerator WaitSpawn()
    {
        yield return new WaitForSeconds(spawnTimer);

        while(!exists)
        {
            Instantiate(monster, transform.position, transform.rotation);
            exists = true;
        }        
    }
}
