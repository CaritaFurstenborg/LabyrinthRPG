using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCreation : MonoBehaviour {

    PlayerInfo playerInfo;

    void Awake()
    {
        playerInfo = FindObjectOfType<PlayerInfo>();
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
        LevelManagerScript.levelManager.LoadLevel("Level1");
        LevelManagerScript.levelManager.UnloadLevel("StartScreen");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
