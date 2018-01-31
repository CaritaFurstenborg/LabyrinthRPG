using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour {
    //Static = only one instance
    public static PlayerInfo playerInfo;
    // Player definitions
    [SerializeField]
    private string playerName;

    [SerializeField]
    private string playerClass;

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

    void Awake()
    {
        if(playerInfo == null)
        {
            playerInfo = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log(MyPlayerClass);
            Debug.Log(MyPlayerLevel);
            Debug.Log(MyStamina);
            Debug.Log(MyStrength);
            Debug.Log(MyIntelligence);
        }
	}

    public void Save()
    {
        SaveLoadManager.SavePlayer(this);
    }

    public void Load()
    {
        int[] loadedStats = SaveLoadManager.LoadPlayerStats();

        MyPlayerLevel = loadedStats[0];
        MyStamina = loadedStats[1];
        MyStrength = loadedStats[2];
        MyIntelligence = loadedStats[3];

        string[] loadedDefs = SaveLoadManager.LoadPlayerDefs();

        MyPlayerName = loadedDefs[0];
        MyPlayerClass = loadedDefs[1];

        LevelManagerScript.levelManager.LoadLevel("Level1");
        LevelManagerScript.levelManager.UnloadLevel("StartScreen");
    }
    
}
