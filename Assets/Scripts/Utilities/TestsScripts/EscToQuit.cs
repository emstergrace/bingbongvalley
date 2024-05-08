using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscToQuit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameInputManager.UIMap.FindAction("Cancel").performed += (x) => QuitApplication();
    }

    void QuitApplication() {
        Application.Quit();
	}
}
