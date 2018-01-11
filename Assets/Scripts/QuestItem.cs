using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour {

    public int questNumber;

    public string itemName;

    /*private QuestManager qm;

	// Use this for initialization
	void Start () {
        qm = FindObjectOfType<QuestManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            if(!qm.questCompleate[questNumber] && qm.quests[questNumber].gameObject.activeSelf)
            {
                qm.itemCollected = itemName;                
                gameObject.SetActive(false);
                qm.questCompleate[questNumber] = true;
            }
        }
    }*/
}
