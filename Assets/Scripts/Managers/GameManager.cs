using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Controls ctrls; public Controls Control { get { return ctrls; } }
    public static Controls.GameplayActions Gameplay { get { return Inst.ctrls.gameplay; } }
    public static Controls.UiActions UIKeys { get { return Inst.ctrls.ui; } }

    private void Awake() {
        Inst = this;
        ctrls = new Controls();

        if (!ResourceLibrary.Inst)
            resourceLibrary.Init();
        if (!OptionsManager.Inst)
            optionsManager.Init();

        Control.gameplay.Enable();
        Control.ui.Enable();
    }

	private void Update() {

        if (activeMenuUI.Count > 0) {
            if (UIKeys.pausegame.triggered || UIKeys.cancel.triggered) {
                PopMenuUI();
                if (activeMenuUI.Count == 0) {
                    PauseGame(false);
                }
            }
        }
        else {
            if (UIKeys.pausegame.triggered) {
                PauseGame(true);
                PushMenuUI(pauseMenu);
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

	#region active menu methods
	public static void PushMenuUI(Menu menu) {
        if (ActiveMenuUI.Count > 0)
            ActiveMenuUI.Peek().gameObject.SetActive(false);
        menu.gameObject.SetActive(true);
        ActiveMenuUI.Push(menu);
	} // End of PushMenuUI().
    
    public static void PopMenuUI() {
        ActiveMenuUI.Pop().gameObject.SetActive(false);
        if (ActiveMenuUI.Count > 0)
            ActiveMenuUI.Peek().gameObject.SetActive(true);
	} // End of PopMenuUI().
	#endregion

	private void OnDisable() {
        Control.gameplay.Disable();
        Control.ui.Disable();
	}
}
