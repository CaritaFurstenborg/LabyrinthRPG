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
        //AccountInfo.accountInfo.MyPlayerScreen.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if(PlayerInfo.MyInstance.MyPlayerName != null)
        {
            selectionName.text = PlayerInfo.MyInstance.MyPlayerName;
            selectionClass.text = PlayerInfo.MyInstance.MyPlayerClass;
            if(PlayerInfo.MyInstance.MyPlayerLevel > 0)
            {
                selectionLevel.text = "Level: " + PlayerInfo.MyInstance.MyPlayerLevel.ToString();
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
        if(!PlayerInfo.MyInstance.MyPlayerName.Equals(""))
        {
            Debug.Log(AccountInfo.accountInfo.MyPlayerScreen);
            if (AccountInfo.accountInfo.MyPlayerScreen != null)
            {
                AccountInfo.accountInfo.MyPlayerScreen.SetActive(true);
                LevelManagerScript.levelManager.LoadLevel(PlayerInfo.MyInstance.MyCurrentZone);
                Player.MyInstance.transform.position = new Vector3(PlayerInfo.MyInstance.MyX, PlayerInfo.MyInstance.MyY, PlayerInfo.MyInstance.MyZ);
                Player.MyInstance.SetClass();
            }      

        }   
        else
        {
            Debug.Log("No Player selected");
        }
    }
}
