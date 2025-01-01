using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    // private static SaveManager _instance;
    //
    // public static SaveManager Instance
    // {
    //     get
    //     {
    //         return _instance;
    //     }
    // }
    //
    // private void Awake()
    // {
    //     _instance = this;
    // }
    private string gamePath;

    private void Start()
    {
        gamePath = Application.persistentDataPath + "/savedata.save";
        LoadData();
    }

    private void LoadData()
    {
        if (!File.Exists(gamePath))
        {
            return;
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fileStream = File.Open(gamePath, FileMode.Open);
        GameData save = (GameData)bf.Deserialize(fileStream);
        //关闭文件流
        fileStream.Close();
        GameManager.Instance.Init(save);
    }

    private void SaveData()
    {
        var data = GameManager.Instance.GetGameData();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fileStream = File.Create(gamePath);
        bf.Serialize(fileStream, data);
        fileStream.Close();
    }

    private void OnDestroy()
    {
        SaveData();
    }
}

[Serializable]
public class GameData
{
    public int coins;
    public List<BirdData> birds = new List<BirdData>();
}

[Serializable]
public class BirdData
{
    public BirdType type;
    public float eatFoodCount = 0;
}
