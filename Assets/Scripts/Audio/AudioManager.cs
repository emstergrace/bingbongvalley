using UnityEngine;
using System;
using FMOD.Studio;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Inst { get; private set; }

	[Header("Sounds")]
	public string soundVCAPath = "vca:/VCA Name"; 
	private static float soundVolume = 0.5f; public static float Sound { get { return soundVolume; } }

	public Action<float> NewSoundVolume;

	[Header("Ambience")]
	public string ambienceVCAPath = "vca:/VCA Name";
	private static float ambienceVolume = 0.5f; public static float Ambience { get { return ambienceVolume; } }

	public Action<float> NewAmbienceVolume;

	[Header("Music")]
	public string musicVCAPath = "vca:/VCA Name";
	private static float musicVolume = 0.5f; public static float Music { get { return musicVolume; } }

	private void Awake() {
		Inst = this;
	} // End of Awake().

	private void Start() {
		SetSoundLevel(soundVolume);
		SetMusicLevel(musicVolume);
	} // End of Start().

	public void SetSoundLevel(float level) {
		// FMOD stuff
		VCA soundVCA = RuntimeManager.GetVCA(soundVCAPath);
		soundVCA.setVolume(level);

		soundVolume = level;
		NewSoundVolume?.Invoke(level);
	} // End of SetSoundLevel().

	public void SetAmbienceLevel(float level) {
		VCA ambVCA = RuntimeManager.GetVCA(ambienceVCAPath);
		ambVCA.setVolume(level);

		ambienceVolume = level;
		NewAmbienceVolume?.Invoke(level);
	} // End of SetAmbienceLevel().

	public void SetMusicLevel(float level) {
		VCA musicVCA = RuntimeManager.GetVCA(musicVCAPath);
		musicVCA.setVolume(level);
	} // End of SetMusicLevel().

	// These could totally be redundant, no clue how fmod works
	public void PlayMainTheme() {

	}

	public void PlaySound(string name) {
	}

	public void StopSound(string name) {
	}

}