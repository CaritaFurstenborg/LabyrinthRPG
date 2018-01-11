using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour {

    public int questID;
    public Text questTitle;

    //buttons
    private GameObject acceptQuestB;
    //private GameObject dumpQuestB;
    private GameObject completeQuestB;

    private QuestButton acceptButtonScript;
    //private QuestButton dumpButtonScript;
    private QuestButton completeButtonScript;

    void Start()
    {
        acceptQuestB = GameObject.Find("QuestPanel").transform.Find("QuestDescription").transform.Find("ButtonsSpacer").transform.Find("QPAcceptButton").gameObject;
        acceptButtonScript = acceptQuestB.GetComponent<QuestButton>();
        completeQuestB = GameObject.Find("QuestPanel").transform.Find("QuestDescription").transform.Find("ButtonsSpacer").transform.Find("QPCompleteButton").gameObject;
        completeButtonScript = completeQuestB.GetComponent<QuestButton>();       
        
        acceptQuestB.SetActive(false);
        completeQuestB.SetActive(false);
    }

    //show info by selecting a quest from the available quests
    public void ShowAllInfo()
    {
        QuestUIManager.uiManagerQ.ShowSelectedQuest(questID);
        //accept button
        if(QuestManager.questManager.RequestAvailableQuest(questID))
        {
            acceptQuestB.SetActive(true);
            acceptButtonScript.questID = questID;
        }
        else
        {
            acceptQuestB.SetActive(false);
        }
        //complete button
        if (QuestManager.questManager.RequestCompletedQuest(questID))
        {
            completeQuestB.SetActive(true);
            completeButtonScript.questID = questID;
        }
        else
        {
            completeQuestB.SetActive(false);
        }
    }   
    
    public void AcceptQuest()
    {
        QuestManager.questManager.AcceptQuest(questID);        
        QuestUIManager.uiManagerQ.HideQuestPanel(); //shut questpanel on accepting

        //update all other npcs
        QuestObject[] currentQuestNPCs = FindObjectsOfType(typeof(QuestObject)) as QuestObject[];
        foreach(QuestObject npc in currentQuestNPCs)
        {
            npc.SetQuestMarker();
        }
    }

    public void AbandonQuest()
    {
        QuestManager.questManager.AbandonQuest(questID);
        QuestUIManager.uiManagerQ.HideQuestPanel(); //shut questpanel on accepting

        //update all other npcs
        QuestObject[] currentQuestNPCs = FindObjectsOfType(typeof(QuestObject)) as QuestObject[];
        foreach (QuestObject npc in currentQuestNPCs)
        {
            npc.SetQuestMarker();
        }
    }

    public void CompleteQuest()
    {
        QuestManager.questManager.CompleteQuest(questID);
        QuestUIManager.uiManagerQ.HideQuestPanel(); //shut questpanel on accepting

        //update all other npcs
        QuestObject[] currentQuestNPCs = FindObjectsOfType(typeof(QuestObject)) as QuestObject[];
        foreach (QuestObject npc in currentQuestNPCs)
        {
            npc.SetQuestMarker();
        }
    }

    public void ClosePanel()
    {
        QuestUIManager.uiManagerQ.HideQuestPanel();
    }
}
