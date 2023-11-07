using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Slimes;

[CreateAssetMenu]
public class ResourceLibrary : SingletonScriptableObject<ResourceLibrary>
{


	[Header("Miscellaneous")]
	private string settingsFileLoc; public static string SettingsFileLoc { get { return Inst.settingsFileLoc; } }

	private IniFile settingsFile; public static IniFile SettingsFile { get { return Inst.settingsFile; } }
	public static readonly string rebinds = "inputscheme";

	public override void Init() {
		base.Init();
		settingsFileLoc = Application.persistentDataPath + "/settings.ini";
		settingsFile = new IniFile(@settingsFileLoc);
	}
}
