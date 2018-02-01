using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class PlayerCreation : MonoBehaviour {

    private AccountInfo accountInfo;

    private PlayerInfo playerInfo;
    [SerializeField]
    private InputField charName;        // Input for creating a new character

    private string playerName;          // existing character for loading

    private string selectedPlayer;      // currently selected saved playercharacter

    //private List<string> playerCharacters;

    void Awake()
    {
        playerInfo = FindObjectOfType<PlayerInfo>();
        accountInfo = FindObjectOfType<AccountInfo>();
        
    }
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SelectMage()
    {
        playerInfo.MyPlayerClass = "mage";
        playerInfo.MyPlayerLevel = 1;
        playerInfo.MyStamina = 15;
        playerInfo.MyIntelligence = 14;
        playerInfo.MyStrength = 9;
    }

    public void SelectWarrior()
    {
        playerInfo.MyPlayerClass = "warrior";
        playerInfo.MyPlayerLevel = 1;
        playerInfo.MyStamina = 15;
        playerInfo.MyIntelligence = 9;
        playerInfo.MyStrength = 14;
    }

    public void EnterNewGame()
    {
        playerInfo.MyPlayerName = charName.text.ToString();
        if(charName.text == "")
        {
            Debug.Log("Can not create a player without a name");
            return;
        }
        else if(accountInfo.playerCharList.Count > 0 && accountInfo.playerCharList.Contains(charName.text))
        {
            Debug.Log("Character name already exists");
            return;
        }
        else if(accountInfo.playerCharList.Count == 6)
        {
            Debug.Log("Already at maximum characters");
            return;
        }
        accountInfo.playerCharList.Add(charName.text);
        //LevelManagerScript.levelManager.LoadLevel("Level1");
        //LevelManagerScript.levelManager.UnloadLevel("StartScreen");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    

    public void SelectCharacter()
    {

    }
}
