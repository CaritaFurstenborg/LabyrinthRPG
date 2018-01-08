using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour {

    public int questNumber;

    public QuestManager theQm;

    public string startText;
    public string endText;

    public bool isItemQuest;
    public string targetItem;

    public bool isEnemyQ;
    public string targetEnemy;
    public int enemiesToKill;

    private int enemyKillCount;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(isItemQuest)
        {
            if(theQm.itemCollected == targetItem)
            {
                theQm.itemCollected = null;
                EndQuest();
            }
        }

        if(isEnemyQ)
        {
            if(theQm.enemyKilled == targetEnemy)
            {
                theQm.enemyKilled = null;
                enemyKillCount++;
            }

            if(enemyKillCount >= enemiesToKill)
            {
                EndQuest();
            }
        }
	}

    public void StartQuest()
    {
        theQm.ShowQuestText(startText);
    }

    public void EndQuest()
    {
        theQm.ShowQuestText(endText);
        theQm.questCompleate[questNumber] = true;
        gameObject.SetActive(false);
    }
}
