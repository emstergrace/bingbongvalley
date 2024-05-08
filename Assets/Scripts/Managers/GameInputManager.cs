using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour
{
    public static GameInputManager Inst { get; private set; }

    [SerializeField] private InputActionAsset _controls; public static InputActionAsset controls { get { return Inst._controls; } }

    private InputActionMap uiMap; public static InputActionMap UIMap { get { return Inst.uiMap; } }
    private InputActionMap gameplayMap; public static InputActionMap GameplayMap { get { return Inst.gameplayMap; } }

    private Gamepad activeGamepad; public static Gamepad ActiveGamepad { get { return Inst.activeGamepad; } }
    
    // Input Actions
    private InputAction cancelAction; public static InputAction CancelAction { get { return Inst.cancelAction; } }
    private InputAction interactAction; public static InputAction InteractAction { get { return Inst.interactAction; } }
    private InputAction testAction; public static InputAction TestAction { get { return Inst.testAction; } }

	private void Awake() {
        Inst = this;
        controls.Enable();

        uiMap = controls.FindActionMap("UI");
        gameplayMap = controls.FindActionMap("Gameplay");

        activeGamepad = Gamepad.current;

        UIMap.Enable();
        gameplayMap.Enable();

        RefreshActions();
    } // End of Awake().

    public void RefreshActions() {
        cancelAction = UIMap.FindAction("Cancel");
        interactAction = gameplayMap.FindAction("Interact");
        testAction = gameplayMap.FindAction("TestKey");
        
        //UpAction = GameplayMap.FindAction("Move").bindings[0];

    } // End of RefreshActions().
}
