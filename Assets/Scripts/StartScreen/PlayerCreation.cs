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
        AccountInfo.accountInfo.MyPlayerScreen.SetActive(false);
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
        PlayerInfo.MyInstance.MyPlayerClass = "mage";
        PlayerInfo.MyInstance.MyPlayerLevel = 1;
        PlayerInfo.MyInstance.MyStamina = 15;
        PlayerInfo.MyInstance.MyIntelligence = 14;
        PlayerInfo.MyInstance.MyStrength = 9;
    }

    public void SelectWarrior()
    {
        PlayerInfo.MyInstance.MyPlayerClass = "warrior";
        PlayerInfo.MyInstance.MyPlayerLevel = 1;
        PlayerInfo.MyInstance.MyStamina = 15;
        PlayerInfo.MyInstance.MyIntelligence = 9;
        PlayerInfo.MyInstance.MyStrength = 14;
    }

    public void CreateNewCharacter()
    {
        PlayerInfo.MyInstance.MyPlayerName = charName.text.ToString();
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
        else if(PlayerInfo.MyInstance.MyPlayerName.Equals(""))      //should prevent entry without a character
        {
            Debug.Log("Must create a character to enter the game");
            return;
        }
        else
        {
            AccountInfo.accountInfo.playerCharList.Add(charName.text);
            AccountInfo.accountInfo.Save();
            Player.MyInstance.transform.position = new Vector3(0, 0, 0);
            PlayerInfo.MyInstance.Save();
            AccountInfo.accountInfo.MyPlayerScreen.SetActive(true);

            Player.MyInstance.SetClass();
            LevelManagerScript.levelManager.LoadLevel(PlayerInfo.MyInstance.MyCurrentZone);
            //LevelManagerScript.levelManager.UnloadLevel("StartScreen");
            
        }        
    }
}
