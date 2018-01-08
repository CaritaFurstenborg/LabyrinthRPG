using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {

    public QuestObject[] quests;
    public bool[] questCompleate;

    public DialogueManager dm;

    public string itemCollected;
    public string enemyKilled;
    

	// Use this for initialization
	void Start () {
        questCompleate = new bool[quests.Length];
        dm = FindObjectOfType<DialogueManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowQuestText(string qText)
    {
        dm.dialogueLines = new string[1];
        dm.dialogueLines[0] = qText; 

        dm.currentLine = 0;
        dm.ShowDialogue();
    }
}
