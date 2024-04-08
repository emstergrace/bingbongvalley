using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "OptionsManager", menuName = "Singletons/Options Manager")]
public class OptionsManager : SingletonScriptableObject<OptionsManager>
{

	public override void Init() {
		base.Init();
		InitSoundSettings();
	}

	private void InitSoundSettings() {

	}
}
