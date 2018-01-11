using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Quest {

	public enum questProgress { NOT_AVAILABLE, AVAILABLE, ACCEPTED, COMPLETED, TURNED_IN }     //State of quest progress

    public string title;    //title of quest
    public int id;          // quest id
    public questProgress progress;  // the state of the current quest (enum), connection var
    public string questDescription;     // gotten from questgiver
    public string hint;                 // gotten from quest giver
    public string turnInDescription;    // gotten from quest reciver
    public string summary;              // quest log?
    public int nextQuest;               // if quest line is chain

    public string questObjective;       //name of the quest objective
    public int questObjectiveCount;     // counter for mobs killed/items gathered
    public int questObjectiveRequirement; // how many to kill/gather of quest objective

    public int expReward;               
    public int moneyReward;
    public string itemReward;
}
