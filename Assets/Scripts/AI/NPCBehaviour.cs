using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC Behaviour", menuName = "Game Data/NPC Behaviour")]
public class NPCBehaviour : ScriptableObject
{
	public GameObject NPCPrefab = null;
	public List<GameObject> genericDialoguePrefabs = new List<GameObject>();
	public List<ActivityContainer> SpecificActivities = new List<ActivityContainer>();

	public GameObject PickRandomDialogue() {
		return genericDialoguePrefabs[Random.Range(0, genericDialoguePrefabs.Count)];
	} // End of PickRandomDialogue().

}

public struct ActivityContainer
{
	public List<GameObject> ActivityDialoguePrefabs; 
	public Activity SpecificActivity;
}