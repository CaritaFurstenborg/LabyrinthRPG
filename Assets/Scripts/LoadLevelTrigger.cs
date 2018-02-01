using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelTrigger : MonoBehaviour {

    public static LoadLevelTrigger levelTrigger;

	[SerializeField]
    private string loadName;

    [SerializeField]
    private string unloadName;

    private bool isLoaded = false;

    void Awake()
    {
        levelTrigger = this;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(loadName != "")
            {
                LevelManagerScript.levelManager.LoadLevel(loadName);
                PlayerInfo.playerInfo.MyCurrentZone = loadName;
            }
            if(unloadName != "")
            {
                LevelManagerScript.levelManager.UnloadLevel(unloadName);
            }
        }
    }

    public void ExitToStartScreen()
    {
        string currentLevel = SceneManager.GetActiveScene().name;
        Debug.Log(currentLevel);
        LevelManagerScript.levelManager.LoadLevel(loadName);
        isLoaded = true;
        if(isLoaded)
        {
            LevelManagerScript.levelManager.UnloadLevel(unloadName);
            isLoaded = false;
        }        
    }
}
