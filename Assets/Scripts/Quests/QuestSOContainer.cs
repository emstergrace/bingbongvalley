using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest SO Container", menuName = "Singletons/Quest SO")]
public class QuestSOContainer : SingletonScriptableObject<QuestSOContainer>
{
	[SerializeField] private List<QuestData> questList = new List<QuestData>(); public static List<QuestData> QuestList { get { return Inst.questList; } }
	private Dictionary<int, QuestData> questDictionary = new Dictionary<int, QuestData>(); public Dictionary<int, QuestData> QuestDictionary { get { return questDictionary; } }


	public override void Init() {
		base.Init();
		InitDictionary();
	} // End of Init().

	public void ResetQuestProgress() {
		for (int i = 0; i < questList.Count; i++) {
			if (SaveGame.Exists("quest_" + questList[i].ID)) {
				SaveGame.Delete("quest_" + questList[i].ID);
			}
		}
	} // End of ResetQuestProgress().

	void InitDictionary() {
		for(int i = 0; i < questList.Count; i++) {
			questDictionary.Add(questList[i].ID, questList[i]);
		}
	} // End of InitDictionary().

	public QuestData RetrieveQuest(int id) {
		if (questDictionary.ContainsKey(id)) {
			return questDictionary[id];
		}
		else {
			return null;
		}
	} // End of RetrieveQuest().

	public QuestData RetrieveQuest(string name) {
		for (int i = 0; i < questList.Count; i++) {
			if (questList[i].name == name)
				return questList[i];
		}

		return null;
	} // End of RetrieveQuest().

	public QuestData RetrieveQuest(QuestData qsd) {
		return questList.Find(item => item.ID == qsd.ID);
	} // End of RetrieveQuest().

	public Quest CreateQuestFromData(int id) {
		QuestData qsd = RetrieveQuest(id);
		return Quest.CreateFromData(qsd);
	} // End of CreateQuestFromData().

	public Quest CreateQuestFromData(string name) {
		QuestData qsd = RetrieveQuest(name);
		return Quest.CreateFromData(qsd);

	} // End of CreateQuestFromData().

	public Quest CreateQuestFromData(QuestData q) {
		QuestData qsd = RetrieveQuest(q);
		return Quest.CreateFromData(qsd);
	} // End of CreateQuestFromData().

} // End of QuestSOContainer.
