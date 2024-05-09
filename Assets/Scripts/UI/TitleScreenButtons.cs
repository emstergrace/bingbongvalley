using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenButtons : MonoBehaviour
{
    public string startSceneName;
    public Vector3 startPos;

    public GameObject loadButton;

	private void Awake() {
        // If there is a save, show the load game button
        if (SaveManager.SaveExists()) {
            loadButton.SetActive(true);
		}
         else {
            loadButton.SetActive(false);
		}
	}

	public void StartGame() {
        GameManager.LoadScene(startSceneName, startPos);
        // Need warning here for starting a new game - or maybe just change the text?? to say restart game?? idk

        StartCoroutine(StartSaveCorout());

        GameManager.startedGame = true;
    } // End of StartGame().

    private IEnumerator StartSaveCorout() {
        yield return null;
        BayatGames.SaveGameFree.SaveGame.DeleteAll();
        yield return null;
        BayatGames.SaveGameFree.SaveGame.Save<bool>("existing save", true);
    }

    public void LoadGame() {
        SaveManager.Load();
        // we should be having savemanager tp them wherever it needs to but for now
        GameManager.LoadScene(startSceneName, startPos);
        GameManager.startedGame = true;
    } // End of LoadGame().

    public void QuitGame() {
        Application.Quit();
	} // End of QuitGame().
}
