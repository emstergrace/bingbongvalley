using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Bellows : MonoBehaviour
{
	public Slider bellowHandle = null;
	public int tempIncreasePerFlap = 100;
	public int maxSliderAmt = 30;
	private int highestStroke = 0;

	private void Start() {
		bellowHandle.maxValue = maxSliderAmt;
		bellowHandle.onValueChanged.AddListener((x) => CheckCompression());
	}

	private void CheckCompression() {
		if (bellowHandle.value > highestStroke) {
			highestStroke = (int)bellowHandle.value;
		}

		if (bellowHandle.value < 2 && highestStroke > 2) {
			BlacksmithingController.Inst.StokeFire(highestStroke / maxSliderAmt * tempIncreasePerFlap);
			highestStroke = 0;
		}

	} // End of CheckCompression().

} // End of Bellows.