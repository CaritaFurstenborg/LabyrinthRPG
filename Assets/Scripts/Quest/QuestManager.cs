using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {

    public static QuestManager questManager;    // static to make sure we have only 1 instance

    public List<Quest> questList = new List<Quest>();   // Master quest list
    public List<Quest> currentQuests = new List<Quest>();   // Current quests list accepted and completed

    // Private variables for our QuestObjects

    void Awake()
    {
        // Removes multiple copies if exits
        if(questManager == null)
        {
            questManager = this;
        }
        else if(questManager != this)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
    }

    public void RequestQuest(QuestObject questNPC)
    {
        //available quests
        if(questNPC.availableQuestID.Count > 0)
        {
            for(int i= 0; i < questList.Count; i++)
            {
                for(int j= 0; j < questNPC.availableQuestID.Count; j++)
                {
                    if(questList[i].id == questNPC.availableQuestID[j] && questList[i].progress == Quest.questProgress.AVAILABLE)
                    {
                        Debug.Log("QuestID: " + questNPC.availableQuestID[j] + " is " + questList[i].progress);

                        //AcceptQuest(questNPC.availableQuestID[j]);
                        // quest ui manager
                        QuestUIManager.uiManagerQ.questAvailable = true;
                        QuestUIManager.uiManagerQ.availableQuests.Add(questList[i]);
                    }
                }
            }
        }
        //active quests
        for(int i = 0; i < currentQuests.Count; i++)
        {
            for(int j = 0; j < questNPC.receivableQuestID.Count; j++)
            {
                if(currentQuests[i].id == questNPC.receivableQuestID[j] && currentQuests[i].progress == Quest.questProgress.ACCEPTED || currentQuests[i].progress == Quest.questProgress.COMPLETED)
                {
                    Debug.Log("QuestID: " + questNPC.receivableQuestID[j] + " is " + currentQuests[i].progress);
                    //CompleteQuest(questNPC.receivableQuestID[j]);
                    // quest UI manager
                    QuestUIManager.uiManagerQ.questRunning = true;
                    QuestUIManager.uiManagerQ.activeQuests.Add(questList[i]);
                }
            }
        }
    }



    // Accept quest
    public void AcceptQuest(int questID)
    {
        for(int i= 0; i < questList.Count; i++)
        {
            if(questList[i].id == questID && questList[i].progress == Quest.questProgress.AVAILABLE)
            {
                currentQuests.Add(questList[i]);
                questList[i].progress = Quest.questProgress.ACCEPTED;
            }
        }
    }

    // Abandon quest
    public void AbandonQuest(int questID)
    {
        for (int i = 0; i < currentQuests.Count; i++)
        {
            if (currentQuests[i].id == questID && currentQuests[i].progress == Quest.questProgress.ACCEPTED)
            {
                currentQuests[i].progress = Quest.questProgress.AVAILABLE;
                currentQuests[i].questObjectiveCount = 0;
                currentQuests.Remove(currentQuests[i]);
            }
        }
    }

    //complete quest
    public void CompleteQuest(int questID)
    {
        for(int i = 0; i < currentQuests.Count; i++)
        {
            if(currentQuests[i].id == questID && currentQuests[i].progress == Quest.questProgress.COMPLETED)
            {
                currentQuests[i].progress = Quest.questProgress.TURNED_IN;
                currentQuests.Remove(currentQuests[i]);

                // REWARD
            }
        }
        // check for chain quest
        CheckChainQuest(questID);
    }

    // check chain quest
    void CheckChainQuest(int questID)
    {
        int tempID = 0;
        for(int i = 0; i < questList.Count; i++)
        {
            if(questList[i].id == questID && questList[i].nextQuest > 0)
            {
                tempID = questList[i].nextQuest;
            }
        }

        if(tempID > 0)
        {
            for(int i = 0; i < questList.Count; i++)
            {
                if (questList[i].id == tempID && questList[i].progress == Quest.questProgress.NOT_AVAILABLE)
                {
                    questList[i].progress = Quest.questProgress.AVAILABLE;
                }
            }
        }
    }

    // Adding items to the list
    public void AddQuestObjective(string questObject, int itemAmount)
    {
        for(int i = 0; i < currentQuests.Count; i++)
        {
            if(currentQuests[i].questObjective == questObject && currentQuests[i].progress == Quest.questProgress.ACCEPTED)
            {
                currentQuests[i].questObjectiveCount += itemAmount;
            }
            if(currentQuests[i].questObjectiveCount >= currentQuests[i].questObjectiveRequirement && currentQuests[i].progress == Quest.questProgress.ACCEPTED)
            {
                currentQuests[i].progress = Quest.questProgress.COMPLETED;
            }
        }
    }

    // Removing items (for inventory system)

    // BOOLS for checking quest states
	public bool RequestAvailableQuest(int questID)
    {
        for(int i = 0; i < questList.Count; i++)
        {
            if(questList[i].id == questID && questList[i].progress == Quest.questProgress.AVAILABLE)
            {
                return true;
            } 
        }
        return false;
    }

    public bool RequestAcceptedQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.questProgress.ACCEPTED)
            {
                return true;
            }
        }
        return false;
    }

    public bool RequestCompletedQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == Quest.questProgress.COMPLETED)
            {
                return true;
            }
        }
        return false;
    }

    // BOOLS for checking questgiver/receiver available quests

    public bool CheckAvailableQuests(QuestObject questNPC)
    {
        for(int i = 0; i < questList.Count; i++)
        {
            for(int j = 0; j < questNPC.availableQuestID.Count; j++)
            {
                if(questList[i].id == questNPC.availableQuestID[j] && questList[i].progress == Quest.questProgress.AVAILABLE)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool CheckAcceptedQuests(QuestObject questNPC)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            for (int j = 0; j < questNPC.receivableQuestID.Count; j++)
            {
                if (questList[i].id == questNPC.receivableQuestID[j] && questList[i].progress == Quest.questProgress.ACCEPTED)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool CheckCompletedQuests(QuestObject questNPC)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            for (int j = 0; j < questNPC.receivableQuestID.Count; j++)
            {
                if (questList[i].id == questNPC.receivableQuestID[j] && questList[i].progress == Quest.questProgress.COMPLETED)
                {
                    return true;
                }
            }
        }
        return false;
    }

    //show quest log
    public void ShowQuestLog(int questID)
    {
        for (int i = 0; i < currentQuests.Count; i++)
        {
            if(currentQuests[i].id == questID)
            {
                QuestUIManager.uiManagerQ.ShowQuestLog(currentQuests[i]);
            }
        }
    }
}
