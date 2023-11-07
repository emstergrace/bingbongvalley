using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicThemeAS : MonoBehaviour
{
    public AudioSource source = null;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        AudioManager.Inst.SetMainTheme(source);
        source.volume = AudioManager.Music;
    }
}
