using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Inst { get; private set; }

	#region sounds here
	// ADD SOUNDS BELOW
	[Header("Sounds")]
	[SerializeField] private AudioSource walkNoise = null; public void SetWalkSource(AudioSource aSO) { walkNoise = aSO; }
	[SerializeField] private List<AudioClip> footNoises = new List<AudioClip>();

	[SerializeField] private List<Sound> sounds = new List<Sound>(); 
	public Dictionary<string, AudioSource> SoundsDictionary { get; private set; } = new Dictionary<string, AudioSource>();

	public float walkNoiseBuffer = 1f;
	private float walkNoiseDelay = 1f;

	#endregion

	public void Walk() {
		if (walkNoiseDelay <= walkNoiseBuffer) {
			walkNoiseDelay += Time.deltaTime;
		}
		if (walkNoise != null && !walkNoise.isPlaying && walkNoiseDelay > walkNoiseBuffer) {
			footIndex = UnityEngine.Random.Range(0, footNoises.Count);
			walkNoise.clip = footNoises[footIndex];
			walkNoise.Play();
			walkNoiseDelay = 0f;
		}
	}
	public void Sprint(bool val) { if (val && walkNoise.pitch != 2f) walkNoise.pitch = 2f; else if (!val && walkNoise.pitch != 1f) walkNoise.pitch = 1f; }
	private int footIndex = 0;

	[SerializeField] private List<AudioSource> sceneAudio = new List<AudioSource>();
	private static float soundVolume = 0.5f; public static float Sound { get { return soundVolume; } }

	public Action<float> NewSoundVolume;

	[Header("Music")]
	[SerializeField] private AudioSource menuMusic = null;
	[SerializeField] private AudioSource mainTheme = null; public void SetMainTheme(AudioSource aSo) { mainTheme = aSo; aSo.Play(); }
	private static float musicVolume = 0.5f; public static float Music { get { return musicVolume; } }

	private void Awake() {
		Inst = this;
		for (int i = 0; i < sounds.Count; i++) {
			SoundsDictionary.Add(sounds[i].name, sounds[i].audio);
		}
	}

	private void Start() {

		SetSoundLevel(soundVolume);
		SetMusicLevel(musicVolume);
	}

	public void SubscribeSFXAudio(AudioSource source) {
		sceneAudio.Add(source);
		source.volume = soundVolume;
	}
	public void UnsubscribeSFXAudio(AudioSource source) {
		sceneAudio.Remove(source);
	}

	public void SetSoundLevel(float level) {
		soundVolume = level;
		foreach(AudioSource source in sceneAudio) {
			source.volume = soundVolume;
		}
		foreach (KeyValuePair<string, AudioSource> keys in SoundsDictionary) {
			if (keys.Value != null)
				keys.Value.volume = level;
		}

		NewSoundVolume?.Invoke(level);
	}

	public void SetMusicLevel(float level) {
		musicVolume = level;
		if (menuMusic != null) menuMusic.volume = musicVolume;
		if (mainTheme != null) mainTheme.volume = musicVolume;
	}

	public void PlayMainTheme() {
		mainTheme.Play();
	}

	public void PlaySound(string name) {
		if (SoundsDictionary.ContainsKey(name)) {
			if (SoundsDictionary[name] != null)
				SoundsDictionary[name].Play();
			else {
				Debug.LogError("Sounds dictionary contains listing for [" + name + "] but no sound associated");
			}
		}
		else
			Debug.LogError("Sounds dictionary does not contain sound for " + name);
	}

	public void StopSound(string name) {
		if (SoundsDictionary.ContainsKey(name)) {
			if (SoundsDictionary[name] != null)
				SoundsDictionary[name].Stop();
			else {
				Debug.LogError("Sounds dictionary contains listing for [" + name + "] but no sound associated");
			}
		}
		else
			Debug.LogError("Sounds dictionary does not contain sound for " + name);
	}

}
[System.Serializable]
public class Sound
{
	public string name;
	public AudioSource audio;
}
