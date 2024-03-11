using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestManager : MonoBehaviour
{

    public static QuestManager Inst { get; private set; }
    
    public static QuestSOContainer QuestListContainer { get; private set; }

    public List<Quest> Quests = new List<Quest>();
    private Dictionary<int, Quest> QuestDictionary = new Dictionary<int, Quest>();

    public Action<Quest> QuestCompletedEvent;
    public Action<Quest> QuestAcceptedEvent;

	private void Awake() {
        Inst = this;
	} // End of Awake().

	// Start is called before the first frame update
	void Start()
    {
        QuestListContainer = QuestSOContainer.Inst;
        QuestListContainer.LoadQuestProgress();
        EventManager.StartListening(Objective.StringNotifier, OnObjectiveTriggered);
    } // End of Start().

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
        quest.Activate();
        Quests.Add(quest);
        QuestDictionary.Add(quest.ID, quest);
        QuestAcceptedEvent?.Invoke(quest);
    } // End of AddQuest().

    public Quest RetrieveQuest(int ID) {
        if (QuestDictionary.ContainsKey(ID))
            return QuestDictionary[ID];
        else
            return null;
	} // End of RetrieveQuest().

    private void TriggerObjectives(string trigger, int amt) {
        foreach(Quest quest in Quests) {
            if (quest.status == QuestStatus.Active)
                quest.TriggerPossibleObjective(trigger, amt);
		}
	} // End of TriggerObjectives().

} // End of QuestManager.
