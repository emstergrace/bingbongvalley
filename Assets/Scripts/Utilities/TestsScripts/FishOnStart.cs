using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        FishingController.Inst.InitializeFishing();
    }
}