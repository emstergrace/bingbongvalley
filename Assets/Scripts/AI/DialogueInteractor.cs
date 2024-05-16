using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialogueInteractor : Interactable
{
	public NPCConversation rootConversation;
	public List<ConvoConditions> conditionalConversations;

	public override void Interact() {
		if (!ConversationManager.Instance.IsConversationActive) {
			bool conditional = false;
			foreach (ConvoConditions convo in conditionalConversations) {
				// Check quest condition
				if (convo.questNumber != 0) {
					if (convo.statusRequired == QuestManager.Inst.GetQuestStatus(convo.questNumber)) {
						conditional = true;
						ConversationManager.Instance.StartConversation(convo.conversation);
						break;
					}
				}
				// Check blackboard condition
				if (!string.IsNullOrEmpty(convo.name)) {
					object val = BlackboardManager.Inst.Remember<object>(convo.name);
					if (val is int && (int)val == convo.intVal) {
						conditional = true;
						ConversationManager.Instance.StartConversation(convo.conversation);
						break;
					}
					else if (val is bool && (bool)val == convo.boolVal) {
						conditional = true;
						ConversationManager.Instance.StartConversation(convo.conversation);
						break;
					}
				}
			}

			if (!conditional) ConversationManager.Instance.StartConversation(rootConversation);

			PlayerMoveController.Inst.StopMovement();
		}
		else {
			ConversationManager.Instance.DoConversationInteraction();
		}
	}
	
	[System.Serializable]
	public struct ConvoConditions
	{
		public NPCConversation conversation;
		[Header("Quest Conditions")]
		public int questNumber;
		public QuestStatus statusRequired;
		[Header("Blackboard Conditions")]
		public string name;
		public int intVal;
		public bool boolVal;
	}
}
