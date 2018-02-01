using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager {

	public static void SavePlayer(PlayerInfo player)
    {
        string playerName = player.MyPlayerName;

        BinaryFormatter bf = new BinaryFormatter();

        FileStream stream = new FileStream(Application.persistentDataPath + "/" + playerName + ".sav", FileMode.Create);

        PlayerData data = new PlayerData(player);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static int[] LoadPlayerStats(string playerName)
    {
        if(File.Exists(Application.persistentDataPath + "/" + playerName + ".sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream stream = new FileStream(Application.persistentDataPath + "/" + playerName + ".sav", FileMode.Open);

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

    public static string[] LoadPlayerDefs(string playerName)
    {
        if (File.Exists(Application.persistentDataPath + "/" + playerName + ".sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream stream = new FileStream(Application.persistentDataPath + "/" + playerName + ".sav", FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;     // as instead of typecast

            stream.Close();
            return data.defs;
        }
        else
        {
            Debug.LogError("File does not exist.");
            return new string[3];
        }
    }

    public static bool PlayerExists(string playerName)
    {
        if (File.Exists(Application.persistentDataPath + "/" + playerName + ".sav"))
        {
            return true;
        }
        return false;
    }

    public static void SaveAccountInfo(AccountInfo ai)
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream stream = new FileStream(Application.persistentDataPath + "/PlayerAccount.sav", FileMode.Create);

        AccountData data = new AccountData(ai);

        bf.Serialize(stream, data);
        stream.Close();
    }
    
    public static string[] LoadAccountInfo()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerAccount.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream stream = new FileStream(Application.persistentDataPath + "/PlayerAccount.sav", FileMode.Open);

            AccountData data = bf.Deserialize(stream) as AccountData;     // as instead of typecast

            stream.Close();
            return data.playerChars;
        }
        else
        {
            Debug.LogError("File does not exist.");
            return new string[6];
        }
    }    
}
