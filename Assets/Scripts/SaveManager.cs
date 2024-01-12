using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveManager
{
    public static PlayerData playerData;

    public static string GetFilePath()
    {
        return Path.Combine(Application.persistentDataPath,"/playerData.json");
    }

    public static void SaveData(PlayerData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(GetFilePath(), json);
    }

    public static PlayerData LoadData()
    {
        if (File.Exists(GetFilePath()))
        {
            string json = File.ReadAllText(GetFilePath());
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            return data;
        }
        return new PlayerData();
    }
}

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int level;
    public int experience;
}
