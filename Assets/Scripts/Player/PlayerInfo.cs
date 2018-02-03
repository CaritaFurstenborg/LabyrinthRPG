using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {
    
    public static PlayerInfo playerInfo;        //The one and only

    // Player definitions
    [SerializeField]
    private string playerName;

    [SerializeField]
    private string playerClass;

    private string currentZone;

    [SerializeField]
    private int playerLevel;

    //player stats
    [SerializeField]
    private int stamina;

    [SerializeField]
    private int intelligence;

    [SerializeField]
    private int strength;

    public string MyPlayerName
    {
        get
        {
            return playerName;
        }

        set
        {
            playerName = value;
        }
    }

    public string MyPlayerClass
    {
        get
        {
            return playerClass;
        }

        set
        {
            playerClass = value;
        }
    }

    public int MyPlayerLevel
    {
        get
        {
            return playerLevel;
        }

        set
        {
            playerLevel = value;
        }
    }

    public int MyStamina
    {
        get
        {
            return stamina;
        }

        set
        {
            stamina = value;
        }
    }

    public int MyIntelligence
    {
        get
        {
            return intelligence;
        }

        set
        {
            intelligence = value;
        }
    }

    public int MyStrength
    {
        get
        {
            return strength;
        }

        set
        {
            strength = value;
        }
    }

    public string MyCurrentZone
    {
        get
        {
            return currentZone;
        }

        set
        {
            currentZone = value;
        }
    }

    void Awake()
    {
        if (playerInfo == null)
        {
            playerInfo = this;
        }
    }

    // Use this for initialization
    void Start () {
		if(MyCurrentZone == null)
        {
            MyCurrentZone = "Level1";
        }
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void Save()
    {
        SaveLoadManager.SavePlayer(this);
    }
}
