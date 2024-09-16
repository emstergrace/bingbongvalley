using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using FMODUnity;
//using FMOD.Studio;

public class FMODEvents : MonoBehaviour
{
    
    [field: Header("Test Event")]
   // [field: SerializeField] public EventReference TestEvent { get; private set; }
    
    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events script in the scene.");
        }
        instance = this;
    }
}
