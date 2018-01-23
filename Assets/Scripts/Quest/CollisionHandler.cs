using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    public string questItemName;
    public int questItemId;


    // Use this for initialization
    void Start()
    {
        questItemName = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.name == "Player")
        {
            if(Input.GetKeyDown(KeyCode.I) && QuestManager.questManager.RequestAcceptedQuest(questItemId))
            {
                QuestManager.questManager.AddQuestObjective(questItemName, 1);
                gameObject.SetActive(false);
            }            
        }
    }
}
