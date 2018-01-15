using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour {

    public string questItemName;
    

	// Use this for initialization
	void Start () {
        questItemName = gameObject.name;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            QuestManager.questManager.AddQuestObjective(questItemName, 1);
            gameObject.SetActive(false);

        }               
    }
}
