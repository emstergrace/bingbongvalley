using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuOptionsBtn : MonoBehaviour
{
    public GameObject optionsPanel;

    public void OnClick() {
        optionsPanel.SetActive(!optionsPanel.activeSelf);
	}
}
