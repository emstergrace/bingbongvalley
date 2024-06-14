using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PourHandle : MonoBehaviour
{
    public Slider handle = null;
    public int pourPerPull = 9;
    public int rateOfPour = 2;
    [ShowOnly]public int minimumSlideValue = 0;

    [SerializeField] private float baseAmountMetal = 99;
    [ShowOnly]public float amountMetalLeft = 99;
    private bool isActive = false;

    public Action<int> pourRating;
    private float overPourTimer = 0f;

    public void Activate() {
        isActive = true;
	}
    public void Deactivate() {
        isActive = false;
	}

	private void Start() {
        handle.value = 0;
        amountMetalLeft = baseAmountMetal;
        handle.maxValue = baseAmountMetal / pourPerPull;

        isActive = true;
	} // End of Start().

	// Update is called once per frame
	void Update() {
		if (isActive) {
			if (handle.value > 0 && amountMetalLeft >= baseAmountMetal - handle.value * pourPerPull) {
                minimumSlideValue = Mathf.FloorToInt(amountMetalLeft / pourPerPull);
                amountMetalLeft -= (rateOfPour - rateOfPour * (handle.maxValue - minimumSlideValue - handle.value)) * Time.deltaTime;

                if (handle.maxValue - minimumSlideValue - handle.value <= -2) {
                    overPourTimer += Time.deltaTime;
				}

                if (amountMetalLeft <= 0) {
                    isActive = false;
                    int rating = 10 - Mathf.FloorToInt(overPourTimer * 4);
                    pourRating?.Invoke(rating);
				}
			} 
		}
	} // End of Update().

} // End of PourHandle.
