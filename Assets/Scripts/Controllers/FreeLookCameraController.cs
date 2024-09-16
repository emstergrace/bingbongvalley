using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class FreeLookCameraController : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook freeLookCam;

	private void Start() {
        GameInputManager.GameplayMap.FindAction("View").performed += HandleLook;

    }

	// Update is called once per frame
	void FixedUpdate()
    {
        if (!GameManager.AllowPlayerInput()) {
            freeLookCam.m_XAxis.m_MaxSpeed = 0;
            return;
		}

    } // End of FixedUpdate().

    public void HandleLook(InputAction.CallbackContext action) {
        if (Mouse.current.rightButton.isPressed) {
            Vector2 mouseDelta = action.ReadValue<Vector2>();
            freeLookCam.m_XAxis.Value += mouseDelta.x * 0.5f;
        }
	} // End of HandleLook().

}
