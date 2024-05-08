using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialogueInteractor : Interactable
{
	public NPCConversation conversation;

	public override void Interact() {
		if (!ConversationManager.Instance.IsConversationActive) {
			ConversationManager.Instance.StartConversation(conversation);
			PlayerMoveController.Inst.StopMovement();
		}
		else {
			ConversationManager.Instance.DoConversationInteraction();
		}
	}
}
