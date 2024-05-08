using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{

    public int questID = -1;
    
    public void ActivateQuest() {
        if (questID == -1 || !QuestSOContainer.Inst.QuestDictionary.ContainsKey(questID)) {
            Debug.LogError("Quest ID not available! Ensure Scriptable Objects/Singletons/Quest SO Container has quest");
		}
        else {
            QuestManager.Inst.ActivateQuest(questID);
        }
	}
}
