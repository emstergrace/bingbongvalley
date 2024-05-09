using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DialogueEditor;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst { get; private set; } = null;

    [Header("Managers")]

    [Header("Singletons")]
    [SerializeField] private ResourceLibrary resourceLibrary = null;
    [SerializeField] private OptionsManager optionsManager = null;
    [SerializeField] private QuestSOContainer questLibrary = null;
    [SerializeField] private LocationManager locationManager = null;
    [Header("")]
    [SerializeField] private Menu pauseMenu = null;
    [SerializeField] private GameObject loadingScreen = null;

    private bool gamePaused = false; public static bool IsGamePaused { get { return Inst.gamePaused; } }
    private Stack<Menu> activeMenuUI = new Stack<Menu>(); public static Stack<Menu> ActiveMenuUI { get { return Inst.activeMenuUI; } }

    public static Menu TopmostMenu { get; private set; } = null;

    public static bool isFishingActive = false;
    public static bool isCloudActive = false;

    public static bool startedGame = false;

    private void Awake() {
        if (Inst == null)
            Inst = this;
        else {
            Destroy(this.gameObject);
            return;
		}

        if (resourceLibrary != null && !ResourceLibrary.Inst)
            resourceLibrary.Init();
        if (optionsManager != null && !OptionsManager.Inst)
            optionsManager.Init();
        if (questLibrary != null && !QuestSOContainer.Inst)
            questLibrary.Init();
        if (locationManager != null && !LocationManager.Inst)
            locationManager.Init();

        DontDestroyOnLoad(this.gameObject);
    }


    private void Update() {
		// Menu Handling
		if (GameInputManager.CancelAction.triggered) {
			if (TopmostMenu != null) {
				TopmostMenu.PopMenu();
			}
			else {
				if (startedGame) {
					PushMenuUI(pauseMenu);
				}
			}
		}

		// Backpack
		if (GameInputManager.BackpackKey.triggered) {
            if (startedGame) {
				if (InventoryPlaceholder.Inst == null) {
					Instantiate(ResourceLibrary.InventoryCanvas);

				}
				if (TopmostMenu == InventoryPlaceholder.Inst) {
					PopMenuUI();
				}
				else {
					PushMenuUI(InventoryPlaceholder.Inst);
				}
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

    public static void PushPauseMenu() {
        PushMenuUI(Inst.pauseMenu);
	} // End of PushPauseMenu().

    public static bool AllowPlayerInput() {

        if (GameManager.TopmostMenu != null) {
            return false;
        }
        if (ConversationManager.Instance != null && ConversationManager.Instance.IsConversationActive) {
            return false;
		}
        if (isFishingActive /*|| isCloudActive*/) {
            return false;
		}


        return true;
    } // End of AllowPlayerInput().

    public static void LoadScene(string name, Vector3 position = default(Vector3)) {
        Inst.StartCoroutine(Inst.LoadSceneCorout(name, position));
	}

    public IEnumerator LoadSceneCorout(string name, Vector3 position) {

        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(LocationManager.GetScene(name), LoadSceneMode.Single);

        loadingScreen.SetActive(true);

        if (PlayerController.Inst != null)
            PlayerController.Inst.gameObject.SetActive(false);

        while (!asyncLoadLevel.isDone) {
            yield return null;
        }

        //QuestManager.Inst.LoadAllQuests();
        if (PlayerController.Inst != null) {
            PlayerController.Inst.Teleport(position);
            PlayerController.Inst.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(0.5f);
        loadingScreen.SetActive(false);
    } // End of LoadScene().

    #region active menu methods
    public static void PushMenuUI(Menu menu) {
        if (menu == null) return;
        if (ActiveMenuUI.Contains(menu)) return;

        if (TopmostMenu != null)
            TopmostMenu.gameObject.SetActive(false);

        GameInputManager.GameplayMap.Disable();

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
            GameInputManager.GameplayMap.Enable();
		}
	} // End of PopMenuUI().
	#endregion
}
