﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour {

    public int questID;
    public Text questTitle;

    //buttons
    private GameObject acceptQuestB;
    private GameObject abandonQuestB;
    private GameObject completeQuestB;

    //private QuestButton acceptButtonScript;
    //private QuestButton abandonButtonScript;
    //private QuestButton completeButtonScript;

    void Update()
    {
        if(acceptQuestB == null)
        {
            acceptQuestB = GameObject.Find("CanvasUiManager").transform.Find("QuestPanel").transform.Find("QuestDescription").transform.Find("ButtonsSpacer").transform.Find("QPAcceptButton").gameObject;
            //acceptButtonScript = acceptQuestB.GetComponent<QuestButton>();
            acceptQuestB.SetActive(false);
        }
        if(abandonQuestB == null)
        {
            abandonQuestB = GameObject.Find("CanvasUiManager").transform.Find("QuestLogPanel").transform.Find("QuestDescription").transform.Find("ButtonsSpacer").transform.Find("AbandonButton").gameObject;
            //abandonButtonScript = abandonQuestB.GetComponent<QuestButton>();
            abandonQuestB.SetActive(false);
        }

        if(completeQuestB == null)
        {
            completeQuestB = GameObject.Find("CanvasUiManager").transform.Find("QuestPanel").transform.Find("QuestDescription").transform.Find("ButtonsSpacer").transform.Find("QPCompleteButton").gameObject;
            //completeButtonScript = completeQuestB.GetComponent<QuestButton>();
            completeQuestB.SetActive(false);
        }

        if(QuestManager.questManager == null)
        {
            Debug.Log("QuestUiManager missing!");
        }
        
    }

    //show info by selecting a quest from the available quests
    public void ShowAllInfo()
    {
        QuestUIManager.uiManagerQ.ShowSelectedQuest(questID);
        //accept button
        if(QuestManager.questManager.RequestAvailableQuest(questID))
        {
            QuestUIManager.uiManagerQ.acceptButton.SetActive(true);
            QuestUIManager.uiManagerQ.acceptButtonScript.questID = questID;
        }
        else
        {
            QuestUIManager.uiManagerQ.acceptButton.SetActive(false);
        }
        //complete button
        if (QuestManager.questManager.RequestCompletedQuest(questID))
        {
            QuestUIManager.uiManagerQ.completeButton.SetActive(true);
            QuestUIManager.uiManagerQ.completeButtonScript.questID = questID;
        }
        else
        {
            QuestUIManager.uiManagerQ.completeButton.SetActive(false);
        }
    }   
    
    public void AcceptQuest()
    {
        QuestManager.questManager.AcceptQuest(questID);        
        QuestUIManager.uiManagerQ.HideQuestPanel(); //shut questpanel on accepting

        Debug.Log("Quest Accepted");

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
        QuestUIManager.uiManagerQ.acceptButton.SetActive(false);
        QuestUIManager.uiManagerQ.abandonButton.SetActive(false);
        QuestUIManager.uiManagerQ.completeButton.SetActive(false);
    }
}
