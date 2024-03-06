using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : Menu
{
	public override void OnEnter() {
		GameManager.PauseGame(true);
	}

	public override void OnExit() {
		GameManager.PauseGame(false);
	}
}
