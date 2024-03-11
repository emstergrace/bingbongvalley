using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu]
public class ResourceLibrary : SingletonScriptableObject<ResourceLibrary>
{

	[Header("Fishing")]
	[SerializeField] private Sprite fishSprite; public static Sprite FishSprite { get { return Inst.fishSprite; } }
	[SerializeField] private Sprite upSprite; public static Sprite UpSprite { get { return Inst.upSprite; } }
	[SerializeField] private Sprite downSprite; public static Sprite DownSprite { get { return Inst.downSprite; } }
	[SerializeField] private Sprite leftSprite; public static Sprite LeftSprite { get { return Inst.leftSprite; } }
	[SerializeField] private Sprite rightSprite; public static Sprite RightSprite { get { return Inst.rightSprite; } }


	[Header("Miscellaneous")]
	private string settingsFileLoc; public static string SettingsFileLoc { get { return Inst.settingsFileLoc; } }

	private IniFile settingsFile; public static IniFile SettingsFile { get { return Inst.settingsFile; } }

	public override void Init() {
		base.Init();
		settingsFileLoc = Application.persistentDataPath + "/settings.ini";
		settingsFile = new IniFile(@settingsFileLoc);
	}
}
