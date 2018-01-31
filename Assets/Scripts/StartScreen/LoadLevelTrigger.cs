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
            }
            if(unloadName != "")
            {
                LevelManagerScript.levelManager.UnloadLevel(unloadName);
            }
        }
    }

    public void ExitToStartScreen()
    {
        LevelManagerScript.levelManager.LoadLevel(loadName);
        LevelManagerScript.levelManager.UnloadLevel(unloadName);
    }
}
