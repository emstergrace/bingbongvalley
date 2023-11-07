using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu]
public class OptionsManager : SingletonScriptableObject<OptionsManager>
{

	public override void Init() {
		base.Init();
		InitKeyboardSettings();
		InitSoundSettings();
	}

	private void InitKeyboardSettings() {
		/*
		if (!ResourceLibrary.SettingsFile.KeyExists("BasicAttackKey"))
			ResourceLibrary.SettingsFile.Write("BasicAttackKey", ogBasicAtkKey);
		if (!ResourceLibrary.SettingsFile.KeyExists("SkillOneKey"))
			ResourceLibrary.SettingsFile.Write("SkillOneKey", ogSkillOneKey);
		if (!ResourceLibrary.SettingsFile.KeyExists("SkillTwoKey"))
			ResourceLibrary.SettingsFile.Write("SkillTwoKey", ogSkillTwoKey);
		if (!ResourceLibrary.SettingsFile.KeyExists("SkillThreeKey"))
			ResourceLibrary.SettingsFile.Write("SkillThreeKey", ogSkillThreeKey);
		if (!ResourceLibrary.SettingsFile.KeyExists("SkillFourKey"))
			ResourceLibrary.SettingsFile.Write("SkillFourKey", ogSkillFourKey);
		if (!ResourceLibrary.SettingsFile.KeyExists("JumpKey"))
			ResourceLibrary.SettingsFile.Write("JumpKey", ogJumpKey);
		if (!ResourceLibrary.SettingsFile.KeyExists("SprintKey", ogSprintKey))
			ResourceLibrary.SettingsFile.Write("SprintKey", ogSprintKey);
		if (!ResourceLibrary.SettingsFile.KeyExists("UpKey", ogUpKey))
			ResourceLibrary.SettingsFile.Write("UpKey", ogUpKey);
		if (!ResourceLibrary.SettingsFile.KeyExists("DownKey", ogDownKey))
			ResourceLibrary.SettingsFile.Write("DownKey", ogDownKey);
		if (!ResourceLibrary.SettingsFile.KeyExists("LeftKey", ogLeftKey))
			ResourceLibrary.SettingsFile.Write("LeftKey", ogLeftKey);
		if (!ResourceLibrary.SettingsFile.KeyExists("RightKey", ogRightKey))
			ResourceLibrary.SettingsFile.Write("RightKey", ogRightKey);
		*/

		/*
		if (!ResourceLibrary.SettingsFile.KeyExists(ResourceLibrary.rebinds)) {
			ResourceLibrary.SettingsFile.Write(ResourceLibrary.rebinds, Control.SaveBindingOverridesAsJson());
		}
		else {
			Control.LoadBindingOverridesFromJson(ResourceLibrary.rebinds);
		}
		*/

		/*
		basicAttackKey = (KeyCode)System.Enum.Parse(typeof(KeyCode),ResourceLibrary.SettingsFile.Read("BasicAttackKey"));
		skillOneKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), ResourceLibrary.SettingsFile.Read("SkillOneKey"));
		skillTwoKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), ResourceLibrary.SettingsFile.Read("SkillTwoKey"));
		skillThreeKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), ResourceLibrary.SettingsFile.Read("SkillThreeKey"));
		skillFourKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), ResourceLibrary.SettingsFile.Read("SkillFourKey"));
		jumpKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), ResourceLibrary.SettingsFile.Read("JumpKey"));
		sprintKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), ResourceLibrary.SettingsFile.Read("SprintKey"));
		upKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), ResourceLibrary.SettingsFile.Read("UpKey"));
		downKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), ResourceLibrary.SettingsFile.Read("DownKey"));
		leftKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), ResourceLibrary.SettingsFile.Read("LeftKey"));
		rightKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), ResourceLibrary.SettingsFile.Read("RightKey"));
		*/

		
	}

	private void InitSoundSettings() {

	}
}
