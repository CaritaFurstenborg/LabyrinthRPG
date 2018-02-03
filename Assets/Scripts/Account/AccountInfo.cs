using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AccountInfo : MonoBehaviour {

    public static AccountInfo accountInfo;

    [SerializeField]
    private GameObject playerScreenPrefab;

    public List<string> playerCharList;

	// Use this for initialization
	void Awake () {

        if (accountInfo == null)
        {
            accountInfo = this;
        }

        if (File.Exists(Application.persistentDataPath + "/PlayerAccount.sav"))
        {
            string[] playerChars = SaveLoadManager.LoadAccountInfo();

            for (int i = 0; i < 6; i++)
            {
                if (playerChars[i] != null)
                { 
                    playerCharList.Add(playerChars[i]);
                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Save()
    {
        SaveLoadManager.SaveAccountInfo(this);
    }

    public void InstantiatePlayer()
    {
        Instantiate(playerScreenPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
