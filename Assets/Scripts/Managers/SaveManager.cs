using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;
using BayatGames.SaveGameFree.Serializers;

public class SaveManager : MonoBehaviour {

    public static SaveManager Inst { get; private set; } = null;

	private void Awake() {
        Inst = this;
        SaveGame.Serializer = new SaveGameBinarySerializer();
        SaveGame.Encode = true;
        SaveGame.EncodePassword = "AYEpSGhjzD3ItoLHh5nlqKTMgNW1aV3a";


    } // End of Awake().

    public static bool SaveExists() {
        return SaveGame.Exists("existing save");
	}

    /* List of things to save/load
     * Player position
     * Quests
     * Inventory
     * Boons
     * Garden
     * Potions
     * Day number
     * */

    public static void ResetSaves() {
        SaveGame.Clear();
	} // End of ResetSaves().

	public static void Save()
    {
        SaveQuests();
        SaveInventory();
        SaveBoons();
    }

    public static void Load()
    {

        // technically load here whever they're supposed to load
        // Then after the scene is loaded everything should instantiate itself
    }

    // Quests
    public static void SaveQuests() {
        QuestManager.Inst.SaveAllQuests();
	} // End of SaveQuests().

    public static void LoadQuests() {
        QuestManager.Inst.LoadAllQuests();
	}

    // Inventory
    public static void SaveInventory() {

	}

    public static void LoadInventory() {

	}

    public static void SaveBoons() {

	}

    public static void LoadBoons() {

	}
}
