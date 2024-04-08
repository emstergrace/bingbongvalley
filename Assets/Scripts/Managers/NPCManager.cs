using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager Inst { get; private set; }

    [SerializeField]private Transform NPCContainer = null;
    [Header("")]
    [SerializeField]private List<Activity> genericActivities = new List<Activity>(); public List<Activity> GenericActivity { get { return genericActivities; } }
    [Header("")]
    [SerializeField] private List<NPCStruct> npcsList = new List<NPCStruct>(); public List<NPCStruct> NPCsList { get { return npcsList; } }

	private void Awake() {
        Inst = this;
	}

    public void SpawnNPCsInScene() {
        foreach(NPCStruct npc in npcsList) {
            if (npc.NPCData.CanSpawnInScene()) {
                SpawnNPC(npc);
			}
		}
    } // End of SpawnNPCsInScene().

    public void SpawnNPC(NPCStruct npc) {

        GameObject npcPrefab = Instantiate(npc.behaviour.NPCPrefab, NPCContainer);
        Activity currentActivity = npc.NPCData.currentActivity;
        npcPrefab.transform.position = npc.NPCData.currentActivity.ReturnAvailablePosition().location;

        NPCAI AI = npcPrefab.GetComponent<NPCAI>();
        //AI.DoAnimation();

	} // End of SpawnNPC().

    public void AssignAllNPCActivities() {
        foreach(Activity activity in genericActivities) {
            activity.ClearPositions();
		}

        foreach(NPCStruct npc in npcsList) {
            AssignRandomActivity(npc);
		}
	} // End of AssignAllNPCActivities().

    public void AssignRandomActivity(NPCStruct npc) {
        Activity activity = PickRandomActivity(npc);
        AssignActivity(npc.NPCData, activity);
	} // End of AssignRandomActivity().

    public Activity PickRandomActivity(NPCStruct npc) {
        WeightedList<Activity> activityList = new();

        foreach(ActivityContainer activity in npc.behaviour.SpecificActivities) {
            activityList.Add(activity.SpecificActivity, activity.SpecificActivity.weightedChance);
		}
        foreach(Activity activity in genericActivities) {
            activityList.Add(activity, activity.weightedChance);
		}

        Activity chosenActivity = activityList.Next();
        while (!chosenActivity.CanDoActivity()) {
            chosenActivity = activityList.Next();
		}
        return chosenActivity;
	} // End of PickRandomActivity().

    public void AssignActivity(NPCData NPC, Activity activity) {
        NPC.DoActivity(activity);
	} // End of AssignActivity().

} // End of NPCManager.

[System.Serializable]
public struct NPCStruct
{
    public NPCData NPCData;
    public NPCBehaviour behaviour;
}