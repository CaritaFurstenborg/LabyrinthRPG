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

        PlayerInfo.playerInfo.MyPlayerLevel = loadedStats[0];
        PlayerInfo.playerInfo.MyStamina = loadedStats[1];
        PlayerInfo.playerInfo.MyStrength = loadedStats[2];
        PlayerInfo.playerInfo.MyIntelligence = loadedStats[3];

        string[] loadedDefs = SaveLoadManager.LoadPlayerDefs(playerName);     // Set to desired player to load

        PlayerInfo.playerInfo.MyPlayerName = loadedDefs[0];
        PlayerInfo.playerInfo.MyPlayerClass = loadedDefs[1];
        PlayerInfo.playerInfo.MyCurrentZone = loadedDefs[2];

        Debug.Log(playerName);
    }
}
