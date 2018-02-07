using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelTrigger : MonoBehaviour {

    public static LoadLevelTrigger levelTrigger;

	[SerializeField]
    private string loadName;

    void Awake()
    {
        levelTrigger = this;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(SceneManager.GetActiveScene().name.Contains("Level"))
            {
                Player.MyInstance.MySpawnPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
            }
                        
            if(loadName != "")
            {
                if(loadName.Contains("Level"))
                {
                    Player.MyInstance.transform.position = Player.MyInstance.MySpawnPoint;
                }
                LevelManagerScript.levelManager.LoadLevel(loadName);
                PlayerInfo.MyInstance.MyCurrentZone = loadName;
            }
        }
    }
}
