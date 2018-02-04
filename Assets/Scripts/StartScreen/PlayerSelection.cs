using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour {
    
    [SerializeField]
    private GameObject toonButton;

    [SerializeField]
    private Transform toonButtonSpacer;

    [SerializeField]
    private List<GameObject> AllButtons;

    [SerializeField]
    private Text selectionName;
    [SerializeField]
    private Text selectionClass;
    [SerializeField]
    private Text selectionLevel;

    void Awake()
    {
        
    }
    // Use this for initialization
    void Start () {
        SetSelectablePlayers();
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayerInfo.playerInfo.MyPlayerName != null)
        {
            selectionName.text = PlayerInfo.playerInfo.MyPlayerName;
            selectionClass.text = PlayerInfo.playerInfo.MyPlayerClass;
            if(PlayerInfo.playerInfo.MyPlayerLevel > 0)
            {
                selectionLevel.text = "Level: " + PlayerInfo.playerInfo.MyPlayerLevel.ToString();
            }            
        }
	}

    public void SetSelectablePlayers()
    {
        foreach(string playerChar in AccountInfo.accountInfo.playerCharList)
        {
            GameObject charButton = Instantiate(toonButton);
            CharacterButtonScript cBScript = charButton.GetComponent<CharacterButtonScript>();

            cBScript.toonName = playerChar;
            cBScript.nameText.text = playerChar;

            charButton.transform.SetParent(toonButtonSpacer, false);
            AllButtons.Add(charButton);
        }
    }

    public void EnterGame()
    {   
        if(!PlayerInfo.playerInfo.MyPlayerName.Equals(""))
        {
            LevelManagerScript.levelManager.LoadLevel(PlayerInfo.playerInfo.MyCurrentZone);
            LevelManagerScript.levelManager.UnloadLevel("StartScreen");
            AccountInfo.accountInfo.InstantiatePlayer();
        }   
        else
        {
            Debug.Log("No Player selected");
        }
    }
}
