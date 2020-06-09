using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad {

    //public static List<GameData> savedGames = new List<GameData>();
    public static GameData savedGame = new GameData();

    //it's static so we can call it from anywhere
    public static void Save() {
      GameData.current.DictToList();
        //Debug.Log(GameData.current.previouslyCraftedId.Count);
        if (GameData.current.previouslyCraftedId.Count != 0)
        {
            SaveLoad.savedGame = GameData.current;
            //SaveLoad.savedGames.Add(GameData.current);
            BinaryFormatter bf = new BinaryFormatter();
            //Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
            FileStream file = File.Create(Application.persistentDataPath + "/savedGame.gd"); //you can call it anything you want
            bf.Serialize(file, SaveLoad.savedGame);
            file.Close();
            Debug.Log("Saved");
            //Debug.Log(GameData.current.previouslyCrafted.Count.ToString());
        }
        else { Debug.Log("Not Saved"); }
    }

    public static void Load() {
        if(File.Exists(Application.persistentDataPath + "/savedGame.gd")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGame.gd", FileMode.Open);
            Debug.Log(Application.persistentDataPath);
            SaveLoad.savedGame = (GameData)bf.Deserialize(file);
            file.Close();
            GameData.current = SaveLoad.savedGame;
            GameData.current.ListToDict();
            Debug.Log("Loaded");
        }
    }
}
