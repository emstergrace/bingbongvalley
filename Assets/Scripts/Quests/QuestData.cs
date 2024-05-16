using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using BayatGames.SaveGameFree;
using System.Runtime.Serialization;

// Just the container
[CreateAssetMenu(fileName = "New Quest", menuName = "Game Data/Quest")]
public class QuestData : ScriptableObject
{
    [ShowOnly] public int ID = -1;

    private static HashSet<int> UsedIDs = new HashSet<int>();

    [SerializeField]private string _name; public string Name { get { return _name; } }
    [SerializeField] [Multiline] private string description; public string Description { get { return description; } }
    public bool isRepeatable = false;
    [ConditionalHide("isRepeatable")]public int maxRepeatAmount = 0;

    public List<Objective> objectives = new List<Objective>();

	private void Awake() {
        AssignID();
	} // End of Awake().

	private void OnValidate() {
        if (ID == -1 || UsedIDs.Contains(ID)) {
            int newID = GetHashCode();
            ID = newID;
        }
    } // End of OnValidate().

    void AssignID() {
        if (ID == -1 || UsedIDs.Contains(ID)) {
            int newID = GetHashCode();
            ID = newID;
            UsedIDs.Add(newID);
        }
    } // End of AssignID().

} // End of QuestData.

[System.Serializable]
public class QuestSaveData{
    public int questID;
    public int questStatus;
    public Dictionary<int, int> objectiveProgress = new Dictionary<int, int>();
}

[DataContract]
public enum QuestStatus
{
    [EnumMember]
    Inactive = 0,
    [EnumMember]
    Active = 1,
    [EnumMember]
    FinishedUncompleted = 2,
    [EnumMember]
    Completed = 3,
    [EnumMember]
    Failed = 4
} // End of QuestStatus enum.