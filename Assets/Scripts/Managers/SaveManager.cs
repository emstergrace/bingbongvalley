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


	public static void Save()
    {

    }

    public static void Load()
    {

    }
}
