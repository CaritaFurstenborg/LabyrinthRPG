using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour {

    //public static PlayerSelection ps;

    private CharacterButtonScript cbS;

    private AccountInfo accountInfo;

    private PlayerInfo playerInfo;

    [SerializeField]
    private GameObject toonButton;

    [SerializeField]
    private Transform toonButtonSpacer;

    [SerializeField]
    private List<GameObject> AllButtons;

    public string selectedPlayerName;      //button press selects 


    void Awake()
    {
        playerInfo = FindObjectOfType<PlayerInfo>();
        accountInfo = FindObjectOfType<AccountInfo>();

    }
    // Use this for initialization
    void Start () {
        SetSelectablePlayers();
	}
	
	// Update is called once per frame
	void Update () {
		
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
        LevelManagerScript.levelManager.LoadLevel(PlayerInfo.playerInfo.MyCurrentZone);
        LevelManagerScript.levelManager.UnloadLevel("StartScreen");
    }


}
