using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst { get; private set; } = null;

    [Header("Managers")]

    [Header("Singletons")]
    [SerializeField] private ResourceLibrary resourceLibrary = null;
    [SerializeField] private OptionsManager optionsManager = null;
    [Header("")]
    [SerializeField] private Menu pauseMenu = null;

    private bool gamePaused = false; public static bool IsGamePaused { get { return Inst.gamePaused; } }
    private Stack<Menu> activeMenuUI = new Stack<Menu>(); public static Stack<Menu> ActiveMenuUI { get { return Inst.activeMenuUI; } }

    public static Menu TopmostMenu { get; private set; } = null;

    private void Awake() {
        Inst = this;

        if (resourceLibrary != null && !ResourceLibrary.Inst)
            resourceLibrary.Init();
        if (optionsManager != null && !OptionsManager.Inst)
            optionsManager.Init();
    }

	private void Update() {

        if (TopmostMenu != null) {
            if (InputManager.CancelAction.triggered) {
                TopmostMenu.PopMenu();
            }
        }
        else {
            if (InputManager.CancelAction.triggered) {
                PushMenuUI(pauseMenu);
                InputManager.GameplayMap.Disable();
            }
        }
        
	} // End of Update().

	public static bool PauseGame(bool val) {
        Inst.gamePaused = val;

        if (val == true) {
            Time.timeScale = 0f;
        }
        else {
            Time.timeScale = 1f;
        }

        return val;
	} // End of PauseGame().

    public static bool AllowPlayerInput() {

        if (GameManager.TopmostMenu != null) {
            return false;
        }
        return true;
    }
	#region active menu methods
	public static void PushMenuUI(Menu menu) {
        if (menu == null) return;

        if (TopmostMenu != null)
            TopmostMenu.gameObject.SetActive(false);

        menu.gameObject.SetActive(true);
        menu.OnEnter();

        ActiveMenuUI.Push(menu);
        TopmostMenu = menu;
	} // End of PushMenuUI().
    
    public static void PopMenuUI() { // Call from Menu and nowhere else
        if (ActiveMenuUI.Count == 0) return;

		ActiveMenuUI.Pop().gameObject.SetActive(false);
		
		if (ActiveMenuUI.Count > 0) {
            Menu topMenu = ActiveMenuUI.Peek();
            topMenu.gameObject.SetActive(true);
			TopmostMenu = topMenu;
            // Do we activate TopmostMenu.OnEnter() here?
		}
		else {
			TopmostMenu = null;
            InputManager.GameplayMap.Enable();
		}
	} // End of PopMenuUI().
	#endregion
}
