using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private GameObject objectiveUIPrefab;
    [SerializeField] private Transform objectiveContainer;
	private Dictionary<Objective, TextMeshProUGUI> objectiveTextDict = new Dictionary<Objective, TextMeshProUGUI>();

	public Quest quest { get; private set; }

	private void Start() {
	}

	public void Initialize(Quest q) {
		questName.text = q.Name;
		quest = q;

		foreach(Objective obj in q.objectives) {
			GameObject objUI = Instantiate(objectiveUIPrefab, objectiveContainer);
			TextMeshProUGUI objText = objUI.GetComponent<TextMeshProUGUI>();
			objText.text = obj.Description;
			objectiveTextDict.Add(obj, objText);
			obj.OnProgressChangedCallback += UpdateObjective;

		}

		// Check for what objectives are valid

		q.OnStatusChanged += (status, quest) => {
			if (status == QuestStatus.Completed) {
				QuestFinished();
			}
		};
	} // End of Initialize().

    private void UpdateObjective(int val, Objective obj) {
		TextMeshProUGUI text = objectiveTextDict[obj];
		text.text = obj.Description;
		Debug.Log("Updated objective");
		if (obj.Completed) {
			text.color = Color.green;
		}
	} // End of UpdateObjective().

    private void QuestFinished() {
		//QuestUIContainer.Inst.RemoveQuest(this);
		questName.color = Color.green;
	}
}
