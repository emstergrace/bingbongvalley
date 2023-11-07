using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class SaveLoad : MonoBehaviour {

    public static List<GameManager> savedGames = new List<GameManager>();

    public static void Save()
    {
        savedGames.Add(GameManager.Inst);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Path.Combine(Application.persistentDataPath, "/savedGames.dvx"));
        bf.Serialize(file, SaveLoad.savedGames);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.dvx"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Path.Combine(Application.persistentDataPath, "/savedGames.dvx"), FileMode.Open);
            SaveLoad.savedGames = (List<GameManager>)bf.Deserialize(file);
            file.Close();
        }
    }
}
