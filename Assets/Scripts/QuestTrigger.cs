using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour {

    public int questNumber;

    public bool startQuest;
    public bool endQuest;

    private QuestManager qm;

    // Use this for initialization
    void Start () {
        qm = FindObjectOfType<QuestManager>();
	}
	
	// Update is called once per frame
	void Update () {        
      
		/*if(Input.GetMouseButtonDown(1) && !qm.questHandedIn[questNumber])
        {
            if(startQuest && !qm.quests[questNumber].gameObject.activeSelf)
            {
                qm.quests[questNumber].gameObject.SetActive(true);
                qm.quests[questNumber].StartQuest();
            }

            if(qm.questCompleate[questNumber] && qm.quests[questNumber].gameObject.activeSelf)
            {
                qm.quests[questNumber].EndQuest();
            }
        }*/
	}

    /*void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            if(!qm.questHandedIn[questNumber])
            {
                if (startQuest && !qm.quests[questNumber].gameObject.activeSelf)
                {
                    qm.quests[questNumber].gameObject.SetActive(true);
                    qm.quests[questNumber].StartQuest();
                }

                if (qm.questCompleate[questNumber] && qm.quests[questNumber].gameObject.activeSelf)
                {
                    qm.quests[questNumber].EndQuest();
                }
            }            
        }
    }*/
}
