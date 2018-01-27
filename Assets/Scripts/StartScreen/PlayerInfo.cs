using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInfo : MonoBehaviour {

    private static PlayerInfo playerInfo;

    public static PlayerInfo MyPlayerInfo
    {
        get
        {
            return playerInfo;
        }

        set
        {
            playerInfo = value;
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

    private string playerClass;

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
        }
	}

    public void SelectMage()
    {
        MyPlayerClass = "mage";
    }

    public  void SelectWarrior()
    {
        MyPlayerClass = "warrior";
    }

    public void EnterGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
