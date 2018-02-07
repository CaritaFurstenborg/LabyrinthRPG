using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class PlayerData {

    public int[] stats;     //Players stats ie. int, str, etc
    public string[] defs;       //Player name, class, current zone etc.
    public float[] positions;

    public PlayerData(PlayerInfo player)
    {
        stats = new int[5];
        stats[0] = player.MyPlayerLevel;
        stats[1] = player.MyStamina;
        stats[2] = player.MyStrength;
        stats[3] = player.MyIntelligence;
        stats[4] = player.MyExp;

        defs = new string[3];
        defs[0] = player.MyPlayerName;
        defs[1] = player.MyPlayerClass;
        defs[2] = player.MyCurrentZone;

        positions = new float[3];
        positions[0] = player.MyX;
        positions[1] = player.MyY;
        positions[2] = player.MyZ;
    }
}
