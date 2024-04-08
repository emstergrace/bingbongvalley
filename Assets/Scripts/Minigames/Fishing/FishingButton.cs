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
	private RectTransform lineRect;
	private bool passedLine = false;
	[SerializeField] private float moveSpeed = 7f;

	public delegate void OnFishButtonPressed(bool isCorrect);
	public OnFishButtonPressed onFishButtonPressed;

	public bool isActiveButton = false;
	private bool succeeded = false;
	private bool isOverlapping = false;

   public void Init(Vector2 reqInput, float speed) {

		lineRect = FishingController.Inst.FishingLine.GetComponent<RectTransform>();
		requiredDirection = reqInput;
		//moveSpeed = speed;

		InputManager.GameplayMap.FindAction("Move").started += HandleInput;
		onFishButtonPressed += IsPressedCorrectly;
	} // End of Init().

	private void Update() {
		if (buttonRect.position.x < 100f && isActiveButton && !succeeded) { // We missed the button input
			isActiveButton = false;
			onFishButtonPressed?.Invoke(false);
			return;
		}

		transform.position = transform.position + Vector3.left * moveSpeed * Time.deltaTime * Screen.width;
	} // End of Update().

	private void HandleInput(InputAction.CallbackContext action) {
		if (!isActiveButton) return;

		isActiveButton = false;
		Vector2 dir = action.ReadValue<Vector2>();

		if (isOverlapping) {//(RectsOverlap(buttonRect, lineRect)) {
			if (dir == requiredDirection) {
				onFishButtonPressed?.Invoke(true);
			}
			else {
				onFishButtonPressed?.Invoke(false);
			}
		}
		else {
			onFishButtonPressed?.Invoke(false);
		}

	} // End of HandleInput().

	private void IsPressedCorrectly(bool val) {
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

	private void Disable() {

		StopAllCoroutines();
		succeeded = false;

		FishingController.Inst.fishingOrder.Dequeue();
		onFishButtonPressed -= FishingController.Inst.OnPressedButton;

		FishingButton nextButton = null;
		if (FishingController.Inst.fishingOrder.TryPeek(out nextButton)) {
			nextButton.isActiveButton = true;
		}
		Lean.Pool.LeanPool.Despawn(this.gameObject);
	} // End of Disable().

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject == FishingController.Inst.FishingLine) {
			isOverlapping = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.gameObject == FishingController.Inst.FishingLine) {
			isOverlapping = false;
		}
	}
}
