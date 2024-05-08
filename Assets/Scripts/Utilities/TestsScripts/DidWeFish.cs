using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DidWeFish : MonoBehaviour
{

	public static DidWeFish Inst { get; private set; }
	private void Awake() {
		Inst = this;
	}
	public void FinishedFishing() {
		GameManager.LoadScene("TestScene");
	}
}
