using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUIContainer : MonoBehaviour
{
    public GameObject QuestPanel;
    public GameObject QuestUIPrefab;
	private List<QuestUIStruct> questUIList = new List<QuestUIStruct>();

    public static QuestUIContainer Inst { get; private set; }

	private void Awake() {
		Inst = this;

		QuestManager.QuestAcceptedEvent += AddQuest;
	}

	private void Start() {
		foreach(Quest q in QuestManager.Inst.Quests) {
			AddQuest(q);
		}
	}

	public void AddQuest(Quest q) {
		
		GameObject questUIGO = Instantiate(QuestUIPrefab, QuestPanel.transform);
		QuestUI qui = questUIGO.GetComponent<QuestUI>();
		questUIList.Add(new QuestUIStruct(qui, q));
		qui.Initialize(q);

		if (!QuestPanel.activeSelf)
			QuestPanel.SetActive(true);
	} // End of AddQuest().

	public void RemoveQuest(QuestUI qui) {
		QuestUIStruct match = questUIList.Find(x => x.questUI == qui);
		Destroy(match.questUI.gameObject);
		questUIList.Remove(match);
	} // End of RemoveQuest().

	public struct QuestUIStruct
	{
		public QuestUI questUI;
		public Quest quest;

		public QuestUIStruct (QuestUI qui, Quest q) {
			questUI = qui;
			quest = q;
		}
	} // End of QuestUIStruct.

} // End of QuestUIContainer().
