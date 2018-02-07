using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {

    private static PlayerInfo instance;      // Sets UiManager to singleton

    public static PlayerInfo MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerInfo>();
            }

            return instance;
        }
        set
        {
            instance = value;
        }
    }

    // Player definitions
    [SerializeField]
    private string playerName;

    [SerializeField]
    private string playerClass;

    private string currentZone;

    [SerializeField]
    private int playerLevel;

    private int exp;

    //player stats
    [SerializeField]
    private int stamina;

    [SerializeField]
    private int intelligence;

    [SerializeField]
    private int strength;

    // Player Position
    [SerializeField]
    private float x;
    [SerializeField]
    private float y;
    [SerializeField]
    private float z;

    // Properties

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

    public int MyExp
    {
        get
        {
            return exp;
        }

        set
        {
            exp = value;
        }
    }

    public float MyX
    {
        get
        {
            return x;
        }

        set
        {
            x = value;
        }
    }

    public float MyY
    {
        get
        {
            return y;
        }

        set
        {
            y = value;
        }
    }

    public float MyZ
    {
        get
        {
            return z;
        }

        set
        {
            z = value;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
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

    public void ClearPlayer()
    {

    }
}
