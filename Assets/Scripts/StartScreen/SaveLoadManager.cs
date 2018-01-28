using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager {

	public static void SavePlayer(PlayerInfo player)
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);

        PlayerData data = new PlayerData(player);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static int[] LoadPlayerStats()
    {
        if(File.Exists(Application.persistentDataPath + "/player.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;     // as instead of typecast

            stream.Close();
            return data.stats;
        }
        else
        {
            Debug.LogError("File does not exist.");
            return new int[4];
        }
    }

    public static string[] LoadPlayerDefs()
    {
        if (File.Exists(Application.persistentDataPath + "/player.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;     // as instead of typecast

            stream.Close();
            return data.defs;
        }
        else
        {
            Debug.LogError("File does not exist.");
            return new string[2];
        }
    }
}
