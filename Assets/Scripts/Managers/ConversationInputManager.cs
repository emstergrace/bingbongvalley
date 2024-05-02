using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.InputSystem;

public class ConversationInputManager : MonoBehaviour
{


	private void OnEnable() {
        if (ConversationManager.Instance != null && GameInputManager.Inst != null) {
            GameInputManager.UIMap.FindAction("Selection").performed += HandleInput;
            GameInputManager.UIMap.FindAction("Submit").performed += (x) => SelectOption();
		}
    }

	private void HandleInput(InputAction.CallbackContext action) {
        if (ConversationManager.Instance.IsConversationActive) {
            Vector2 input = action.ReadValue<Vector2>();
            if (input == Vector2.up) {
                ConversationManager.Instance.SelectPreviousOption();
			}
            else if (input == Vector2.down) {
                ConversationManager.Instance.SelectNextOption();
			}
		}
	} // End of HandleInput().

    private void SelectOption() {
        if (ConversationManager.Instance.IsConversationActive) {
            ConversationManager.Instance.PressSelectedOption();
        }
	} // End of SelectOption().

	private void OnDisable() {
        if (ConversationManager.Instance != null && GameInputManager.Inst != null) {
            GameInputManager.UIMap.FindAction("Selection").performed -= HandleInput;
            GameInputManager.UIMap.FindAction("Submit").performed -= (x) => SelectOption();
        }
    }
}
