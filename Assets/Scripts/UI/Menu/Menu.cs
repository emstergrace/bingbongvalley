using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public virtual void OnEnter() {

	} // End of OnEnter().
    
    public void PopMenu() {
        OnExit();
        GameManager.PopMenuUI();
	}

    public virtual void OnExit() {

	} // End of OnExit().
}