using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Inst { get; private set; }

    [SerializeField] private InputActionAsset _controls; public static InputActionAsset controls { get { return Inst._controls; } }

    private InputActionMap uiMap; public static InputActionMap UIMap { get { return Inst.uiMap; } }
    private InputActionMap gameplayMap; public static InputActionMap GameplayMap { get { return Inst.gameplayMap; } }
    
    // Input Actions
    private InputAction cancelAction; public static InputAction CancelAction { get { return Inst.cancelAction; } }

	private void Awake() {
        Inst = this;
        controls.Enable();

        uiMap = controls.FindActionMap("UI");
        gameplayMap = controls.FindActionMap("Gameplay");

        UIMap.Enable();
        gameplayMap.Enable();

        RefreshActions();
    } // End of Awake().

    public void RefreshActions() {
        cancelAction = UIMap.FindAction("Cancel");
        
        //UpAction = GameplayMap.FindAction("Move").bindings[0];

    } // End of RefreshActions().
}
