using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ItemData : ScriptableObject
{
	[ShowOnly] public int ID = -1;
	private static HashSet<int> UsedIDs = new HashSet<int>();

	[Header("Item")]
	public string Name = "New Item";
	public Sprite icon = null;
	public string description = "";

	[Header("Drop Attributes")]
	public bool stackable = false;
	[ConditionalHide("stackable")] public int stackLimit = 1;
	public bool dropUnique = false;
	public bool dropAlways = false;
	public bool dropEnabled = true;
	public bool key = false;

	public virtual void Use() { }

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

}

[Serializable]
public class WeightedItem
{
	public ItemData item;
	public float dropWeight;
	[HideInInspector] public float probabilityFrom;
	[HideInInspector] public float probabilityTo;
}
