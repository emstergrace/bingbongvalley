using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUIContainer : MonoBehaviour
{
    public Transform QuestPanel;
    public GameObject QuestUIPrefab;
	private List<QuestUI> questUIList = new List<QuestUI>();

    public static QuestUIContainer Inst { get; private set; }

	private void Awake() {
		Inst = this;

		QuestManager.QuestAcceptedEvent += AddQuest;
	}
	// We need to re-instantiate the quest container every time it's enabled. 

	public void AddQuest(Quest q) {
		GameObject questUIGO = Instantiate(QuestUIPrefab, QuestPanel);
		QuestUI qui = questUIGO.GetComponent<QuestUI>();
		questUIList.Add(qui);
		qui.Initialize(q);

	} // End of AddQuest().

	public void RemoveQuest(QuestUI qui) {
		Destroy(questUIList.Find(x => x == qui).gameObject);
		questUIList.Remove(qui);
	} // End of RemoveQuest().
}
