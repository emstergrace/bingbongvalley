using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TemperatureGauge : MonoBehaviour
{
    public Slider gauge = null;
	private bool reachedMinTemp = false;
	public int rateTempIncrease = 100;
	public int rateTempDecrease = 20;

	private int minTemperature; // optimal values
	private int maxTemperature;
	private int actualHighestTemp;

	private float timer = 0f;
	private float requiredTime = 0f;
	private bool reachedOptimalTime = false;

	private float secondsUnderTemp = 0;
	private float secondsOverTemp = 0;


	private bool isActive = false;
	private int maxNormalTemperature = 1100; // This is celsius, not that it matters


	private void Update() {
		if (isActive) {
			// Raise and lower temperature independent of bellows to at least minimum maxNormalTemperature
			if (gauge.value < maxNormalTemperature) {
				gauge.value += rateTempIncrease * Time.deltaTime;
				if (gauge.value > actualHighestTemp) {
					actualHighestTemp = (int)gauge.value;
				}
			}
			else {
				gauge.value -= rateTempDecrease * Time.deltaTime;
			}

			// Start keeping score, if the temperature is within optimal range and otherwise
			if (gauge.value > minTemperature) {
				reachedMinTemp = true;
			}

			if (reachedMinTemp && gauge.value < minTemperature) {
				secondsUnderTemp += Time.deltaTime;
			}

			if (gauge.value > minTemperature && gauge.value < maxTemperature) {
				timer += Time.deltaTime;
				if (timer > requiredTime) {
					reachedOptimalTime = true;
				}
			}

			if (gauge.value > maxTemperature) {
				secondsOverTemp += Time.deltaTime;
			}


		}
	} // End of Update().

	public void Activate() {
		isActive = true;
		BlacksmithingController.Inst.IncreaseTemperature += UpdateGauge;
		ResetValues();
	} // End of Activate().

	public void Deactivate() {
		isActive = false;
		BlacksmithingController.Inst.IncreaseTemperature -= UpdateGauge;
		ResetValues();
	} // End of Deactivate().

	private void ResetValues() {
		reachedMinTemp = false;
		reachedOptimalTime = false;
		timer = 0f;
		actualHighestTemp = 0;
		secondsUnderTemp = 0;
		secondsOverTemp = 0;
	} // End of ResetValues().

	// Formula for perfect forge score is 100% within temperature and with enough time. Start losing % based on how much time spent over the temperature and how hot &&
	// under once minimum temperature has been reached.
	// Score = 70 + 20 * (timer / requiredTime) - (seconds under temp * 2) - (seconds over temp * percentage of temp / max temp) + ( 10 from pouring handle)
	public int GetHeatScore() {
		int score = 70;
		score += reachedOptimalTime ? 20 : 0;
		score -= (int)secondsUnderTemp * 2;
		score -= (int)secondsOverTemp * actualHighestTemp / maxTemperature;

		return score;
	} // End of GetHeatScore().

	// Time = how long it needs to be within the optimal temperature range before it's perfectly ready. 
	public void SetOptimalTemperature(int min, int max, float minTime) {
		requiredTime = minTime;

		// This is just supposed to show the green bar or something idk
		minTemperature = min;
		maxTemperature = max;

	} // End of SetOptimalTemperature().

	private void UpdateGauge(int newTemp) {
		gauge.value += newTemp;
		
		if (gauge.value > actualHighestTemp) {
			actualHighestTemp = (int)gauge.value;
		}
	} // End of UpdateGauge().
} // End of TemperatureGauge().