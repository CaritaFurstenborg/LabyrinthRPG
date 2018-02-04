using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class PlayerCreation : MonoBehaviour {
    
    [SerializeField]
    private InputField charName;        // Input for creating a new character

    void Awake()
    {
    }
	// Use this for initialization
	void Start () {
        charName.characterLimit = 10;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SelectMage()
    {
        PlayerInfo.playerInfo.MyPlayerClass = "mage";
        PlayerInfo.playerInfo.MyPlayerLevel = 1;
        PlayerInfo.playerInfo.MyStamina = 15;
        PlayerInfo.playerInfo.MyIntelligence = 14;
        PlayerInfo.playerInfo.MyStrength = 9;
    }

    public void SelectWarrior()
    {
        PlayerInfo.playerInfo.MyPlayerClass = "warrior";
        PlayerInfo.playerInfo.MyPlayerLevel = 1;
        PlayerInfo.playerInfo.MyStamina = 15;
        PlayerInfo.playerInfo.MyIntelligence = 9;
        PlayerInfo.playerInfo.MyStrength = 14;
    }

    public void CreateNewCharacter()
    {
        PlayerInfo.playerInfo.MyPlayerName = charName.text.ToString();
        if(charName.text.ToString().Equals(""))
        {
            Debug.Log("Can not create a player without a name");
            return;
        }
        else if(AccountInfo.accountInfo.playerCharList.Count > 0 && AccountInfo.accountInfo.playerCharList.Contains(charName.text))
        {
            Debug.Log("Character name already exists");
            return;
        }
        else if(AccountInfo.accountInfo.playerCharList.Count == 6)
        {
            Debug.Log("Already at maximum characters");
            return;
        }
        else if(PlayerInfo.playerInfo.MyPlayerName.Equals(""))      //should prevent entry without a character
        {
            Debug.Log("Must create a character to enter the game");
            return;
        }
        else
        {
            AccountInfo.accountInfo.playerCharList.Add(charName.text);
            AccountInfo.accountInfo.Save();
            PlayerInfo.playerInfo.Save();
            LevelManagerScript.levelManager.LoadLevel(PlayerInfo.playerInfo.MyCurrentZone);
            LevelManagerScript.levelManager.UnloadLevel("StartScreen");
            AccountInfo.accountInfo.InstantiatePlayer();
        }        
    }
}
