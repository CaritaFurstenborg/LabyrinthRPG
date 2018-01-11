using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QLogButton : MonoBehaviour {

    public int questID;
    public Text questTitle;

	public void ShowAllInfo()
    {
        QuestManager.questManager.ShowQuestLog(questID);
    }

    public void ClosePanel()
    {
        QuestUIManager.uiManagerQ.HideQuestLogPanel();
    }
}
