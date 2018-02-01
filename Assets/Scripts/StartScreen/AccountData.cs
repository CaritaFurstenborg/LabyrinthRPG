using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AccountData {    

    public string[] playerChars;

    public AccountData(AccountInfo ai)
    {
        playerChars = new string[6];

        for(int i = 0; i < ai.playerCharList.Count; i++)
        {
            playerChars[i] = ai.playerCharList[i];
        }
    }
}
