using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour {
    //Static = only one instance
    private static PlayerInfo playerInfo;
    // Player definitions
    
    private string playerName;
    [SerializeField]
    public string MyPlayerName { get; set; }
    
    private string playerClass;
    [SerializeField]
    public string MyPlayerClass { get; set; }
    
    private int playerLevel;
    [SerializeField]
    public int MyPlayerLevel { get; set; }

    //player stats
    
    private int stamina;
    [SerializeField]
    public int MyStamina { get; set; }
    
    private int intelligence;
    [SerializeField]
    public int MyIntelligence { get; set; }
    
    private int strength;
    [SerializeField]
    public int MyStrength { get; set; }

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

        SceneManager.LoadScene("Level1");
    }
    
}
