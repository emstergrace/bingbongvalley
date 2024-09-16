using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestManager : MonoBehaviour
{
    //Trigger example:
    // EventManager.TriggerEvent(Objective.StringNotifier, new Dictionary<string, object> { { "picked up " + itemName, 1 } });
    public static QuestManager Inst { get; private set; }
    
    public static QuestSOContainer QuestListContainer { get; private set; }

    public List<Quest> Quests { get; private set; } = new List<Quest>();
    private Dictionary<int, Quest> QuestDictionary = new Dictionary<int, Quest>();
    private int numActiveQuests = 0;

    public static Action<Quest> QuestCompletedEvent;
    public static Action<Quest> QuestAcceptedEvent;

	private void Awake() {
        Inst = this;
	} // End of Awake().

	// Start is called before the first frame update
	void Start()
    {
        QuestListContainer = QuestSOContainer.Inst;

        //QuestListContainer.LoadQuestProgress();// Use this line to load quest progress

        EventManager.StartListening(Objective.StringNotifier, OnObjectiveTriggered);
    } // End of Start().

	public void SaveAllQuests() {
        foreach(Quest q in Quests) {
            q.SaveQuest();
		}
	} // End of SaveQuests().

    public void RemoveAllQuests() {
        for (int i = 0; i < QuestSOContainer.QuestList.Count; i++) {
            /*
            if (SaveGame.Exists("quest_" + QuestSOContainer.QuestList[i].ID)) {
                SaveGame.Delete("quest_" + QuestSOContainer.QuestList[i].ID);
            }
            */
        }
        Quests.Clear();
        QuestDictionary.Clear();
	} // End of RemoveAllQuests().

	public void LoadQuest(int questID) {
        ActivateQuest(questID);
        QuestDictionary[questID].LoadQuest();
	} // End of LoadQuest().

    public void LoadAllQuests() {
        for (int i = 0; i < QuestSOContainer.QuestList.Count; i++) {
            /*
            if (SaveGame.Exists("quest_" + QuestSOContainer.QuestList[i].ID)) {
                Inst.LoadQuest(QuestSOContainer.QuestList[i].ID);
            }
            */
        }
    }

    public void ResetQuest(int questID) {
        QuestDictionary[questID].ResetQuest();
	} // End of ResetQuest().

    private void OnObjectiveTriggered(Dictionary<string, object> trigger) {

        foreach (KeyValuePair<string, object> kvp in trigger) {
            Debug.Log("Triggered " + kvp.Key + " - " + (int)kvp.Value);
            TriggerObjectives(kvp.Key, (int)kvp.Value);
        }
	} // End of ObjectiveTriggered().

    public void ActivateQuest(int ID) {
        AddQuest(QuestListContainer.CreateQuestFromData(ID));
	} // End of ActivateQuest().

    public void ActivateQuest(string name) {
        AddQuest(QuestListContainer.CreateQuestFromData(name));
    } // End of ActivateQuest().

    public void ActivateQuest(QuestData qsd) {
         AddQuest(QuestListContainer.CreateQuestFromData(qsd));
	} // End of ActivateQuest().

    private void AddQuest(Quest quest) {
        if (!QuestDictionary.ContainsKey(quest.ID)) {
            quest.Activate();
            Quests.Add(quest);
            QuestDictionary.Add(quest.ID, quest);
            QuestAcceptedEvent?.Invoke(quest);
        }
    } // End of AddQuest().

    public Quest RetrieveQuest(int ID) {
        if (QuestDictionary.ContainsKey(ID))
            return QuestDictionary[ID];
        else
            return null;
	} // End of RetrieveQuest().

    public bool SetQuestStatus(int ID, QuestStatus status) {
        if (RetrieveQuest(ID) == null) return false;

        RetrieveQuest(ID).SetQuestStatus(status);
        return true;
	} // End of SetQuestStatus().

    public QuestStatus GetQuestStatus(int ID) {
        if (RetrieveQuest(ID) == null) {
            return QuestStatus.Inactive;
		}
        else {
            return RetrieveQuest(ID).status;
		}
	}

    private void TriggerObjectives(string trigger, int amt) {
        foreach(Quest quest in Quests) {
            if (quest.status == QuestStatus.Active)
                quest.TriggerPossibleObjective(trigger, amt);
		}
	} // End of TriggerObjectives().

	private void OnApplicationQuit() {
        SaveAllQuests();
	}

} // End of QuestManager.
