using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButtonScript : MonoBehaviour {

    public string toonName;
    public Text nameText;
    
    public void Select()
    {
        SelectCharacter(this.toonName);
    }

    public void SelectCharacter(string playerName)
    {
        int[] loadedStats = SaveLoadManager.LoadPlayerStats(playerName);      // Set to desired player to load

        PlayerInfo.MyInstance.MyPlayerLevel = loadedStats[0];
        PlayerInfo.MyInstance.MyStamina = loadedStats[1];
        PlayerInfo.MyInstance.MyStrength = loadedStats[2];
        PlayerInfo.MyInstance.MyIntelligence = loadedStats[3];

        string[] loadedDefs = SaveLoadManager.LoadPlayerDefs(playerName);     // Set to desired player to load

        PlayerInfo.MyInstance.MyPlayerName = loadedDefs[0];
        PlayerInfo.MyInstance.MyPlayerClass = loadedDefs[1];
        PlayerInfo.MyInstance.MyCurrentZone = loadedDefs[2];

        Debug.Log(playerName);
    }
}
