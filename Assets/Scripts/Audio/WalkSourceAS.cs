using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSourceAS : MonoBehaviour
{
    public AudioSource walkSource;
    public float soundMultiplier = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Inst.SetWalkSource(walkSource);
        walkSource.volume = AudioManager.Sound * soundMultiplier;
        AudioManager.Inst.NewSoundVolume += (x) => { walkSource.volume = x * soundMultiplier; };
    }
}
