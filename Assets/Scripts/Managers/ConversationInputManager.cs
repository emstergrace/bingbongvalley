using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
using UnityEngine.InputSystem;

public class ConversationInputManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (ConversationManager.Instance != null) {
            InputManager.UIMap.FindAction("Selection").performed += HandleInput;
            InputManager.UIMap.FindAction("Submit").performed += (x) => SelectOption();
		}
    } // End of Start().

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
}
