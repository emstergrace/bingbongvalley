using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using BayatGames.SaveGameFree;

// Carbon copy of QuestData, but specifically for player usage.
[Serializable]
public class Quest
{
    public int ID = -1;

    private string _name; public string Name { get { return _name; } }
    private string description; public string Description { get { return description; } }
    public QuestStatus status = QuestStatus.Inactive;
    public bool isRepeatable { get; private set; } = false;
    public int maxRepeatAmount { get; private set; } = -1;
    public int currentRepeatAmount { get; private set; } = 0;

    public List<Objective> objectives = new List<Objective>();

    public delegate void StatusChanged(QuestStatus after, Quest quest);
    public event StatusChanged OnStatusChanged;

    public delegate void ObjProgressChanged(float after, Objective task, Quest quest);
    public event ObjProgressChanged OnObjProgressChanged;

    public Quest(int identifier, string n, string d, bool repeatable, int maxRepeats, List<Objective> objs) {
        ID = identifier;
        _name = n;
        description = d;
        isRepeatable = repeatable;
        maxRepeatAmount = maxRepeats;
        foreach(Objective o in objs) {
            objectives.Add(new Objective(o));
		}
	} // End of Quest() constructor.

    public static Quest CreateFromData(QuestData data) {
        if (data == null) return null;
        return new Quest(data.ID, data.Name, data.Description, data.isRepeatable, data.maxRepeatAmount, data.objectives);
	} // End of CreateFromData().

    public void SetQuestStatus(QuestStatus newStatus) {
        status = newStatus;
        OnStatusChanged?.Invoke(newStatus, this);

        if (status == QuestStatus.Completed) {
            CompleteQuest();
        }
    } // End of SetQuestStatus().

    public void LoadQuest() {

        // Load quest from save
        if (!SaveGame.Exists("quest_" + ID)) return;
        QuestSaveData qsd = SaveGame.Load<QuestSaveData>("quest_" + ID);
        status = (QuestStatus)qsd.questStatus;
        for (int i = 0; i < objectives.Count; i++) {
            objectives[i].SetProgress(qsd.objectiveProgress[i]);
            OnObjProgressChanged?.Invoke(qsd.objectiveProgress[i], objectives[i], this);
        }
        if (status == QuestStatus.Completed || status == QuestStatus.Failed) {
            UnregisterEventsOnObjective();
		}
    } // End of LoadQuest().

    public void SaveQuest() {
        QuestSaveData qsd = new QuestSaveData();
        qsd.questID = ID;
        qsd.questStatus = (int)status;
        Debug.Log("Saving quest " + ID);
        Dictionary<int, int> objectiveValues = new Dictionary<int, int>();
        for (int i = 0; i < objectives.Count; i++) {
            objectiveValues.Add(i, objectives[i].CurrentAmount);
        }

        qsd.objectiveProgress = new Dictionary<int, int>(objectiveValues);

        SaveGame.Save<QuestSaveData>("quest_" + ID, qsd);
    } // End of SaveQuest().

    public void Activate() {
        status = QuestStatus.Active;
        RegisterEventsOnObjective();
        Debug.Log("Activated quest " + Name);
    } // End of RegisterQuest().

    public void CompleteQuest() {
        UnregisterEventsOnObjective();
        Debug.Log("Completed quest " + Name);
        // Save it somewhere else to a finished quest list? idk
    } // End of CompleteQuest().

    public void NotifyTaskProgressChanged(int newVal, Objective obj) {
        OnObjProgressChanged?.Invoke(newVal, obj, this);

    } // End of NotifyTaskProgrsesChanged().

    public void NotifyTaskStatusChanged(Objective obj) {
        bool allCompleted = true;
        foreach (Objective o in objectives) {
            if (!o.Completed && o.IsRequired) {
                allCompleted = false;
            }
        }
        if (allCompleted) {
            SetQuestStatus(QuestStatus.Completed);
        }
    } // End of NotifyTaskStatusChanged().

    private void UnregisterEventsOnObjective() {
        foreach (Objective obj in objectives) {
            obj.OnProgressChangedCallback -= NotifyTaskProgressChanged;
            obj.OnCompletedCallback -= NotifyTaskStatusChanged;
        }
    } // End of UnregisterEventsOnObjective().

    private void RegisterEventsOnObjective() {
        foreach (Objective obj in objectives) {
            obj.OnProgressChangedCallback += NotifyTaskProgressChanged;
            obj.OnCompletedCallback += NotifyTaskStatusChanged;
        }
    } // End of RegisterEventsOnObjective().

    public void ResetQuest() {
        foreach (Objective obj in objectives) {
            obj.Restart();
        }

    } // End of ResetQuest().

    public void TriggerPossibleObjective(string trigger, int amt) {
        foreach (Objective obj in objectives) {
            obj.TryProgressing(trigger, amt);
        }
    } // End of TriggerPossibleObjective().

} // End of Quest class.
