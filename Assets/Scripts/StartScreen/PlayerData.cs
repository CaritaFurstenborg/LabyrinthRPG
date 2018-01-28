using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class PlayerData {

    public int[] stats;
    public string[] defs;

    public PlayerData(PlayerInfo player)
    {
        stats = new int[4];
        stats[0] = player.MyPlayerLevel;
        stats[1] = player.MyStamina;
        stats[2] = player.MyStrength;
        stats[3] = player.MyIntelligence;

        defs = new string[2];
        defs[0] = player.MyPlayerName;
        defs[1] = player.MyPlayerClass;
    }
}
