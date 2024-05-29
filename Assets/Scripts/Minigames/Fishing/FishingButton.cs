using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FishingButton : MonoBehaviour
{

	[SerializeField] private RectTransform buttonRect;
	[SerializeField] private Image image;

	private Vector2 requiredDirection = Vector2.zero;
	private RectTransform canvasRect;

	public delegate void OnFishButtonPressed(bool isCorrect);
	public OnFishButtonPressed onFishButtonPressed;

	public bool isActiveButton = false;
	private bool succeeded = false;

   public void Init(Vector2 reqInput) {

		canvasRect = FishingController.Inst.canvasRect;
		requiredDirection = reqInput;

		GameInputManager.GameplayMap.FindAction("Move").started += HandleInput;
		onFishButtonPressed += IsPressedCorrectly;
	} // End of Init().

	private void HandleInput(InputAction.CallbackContext action) {
		if (!isActiveButton) return;

		isActiveButton = false;
		Vector2 dir = action.ReadValue<Vector2>();

		if (dir == requiredDirection) {
			onFishButtonPressed?.Invoke(true);
		}
		else {
			onFishButtonPressed?.Invoke(false);
		}

	} // End of HandleInput().

	private void IsPressedCorrectly(bool val) {
		if (gameObject.activeSelf)
			StartCoroutine(AfterPressedCorout(val));
	} // End of IsPressedCorrectly().

	private IEnumerator AfterPressedCorout(bool success) {
		if (success) {
			image.color = Color.green;
			succeeded = true;
		}
		else {
			image.color = Color.red;
		}


		float t = 0f;
		while (t < 0.2f) {
			t += Time.unscaledDeltaTime;
			yield return null;
		}

		Disable();
		
	} // End of AfterPressedCorout().

	public void Disable() {

		GameInputManager.GameplayMap.FindAction("Move").started -= HandleInput;
		onFishButtonPressed -= FishingController.Inst.OnPressedButton;

		StopAllCoroutines();
		succeeded = false;

		FishingButton t = null;
		FishingController.Inst.fishingOrder.TryDequeue(out t);

		FishingButton nextButton = null;
		if (FishingController.Inst.fishingOrder.TryPeek(out nextButton)) {
			nextButton.isActiveButton = true;
		}
		Lean.Pool.LeanPool.Despawn(this.gameObject);
	} // End of Disable().

}
