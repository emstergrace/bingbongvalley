using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
     
    public Transform target;
    public float smoothTime = 0.3f;
    public float lookAheadFactor = 4f;

    public static Vector3 offsetZ;
    private Vector3 lastTargetPosition;
    private Vector3 velocity;
    private Vector3 lookAtPos;
    [SerializeField] private float moveThreshold = 0.25f;

    private Vector3 LookDirection = Vector3.zero;
    public static Vector3 AimDirection = Vector3.zero;

    // Use this for initialization
    private void Start()
    {
        if (target == null) {
            SetTarget(PlayerController.Inst.transform);
		}

        offsetZ = Vector3.forward * (transform.position - target.position).z;
        transform.position = new Vector3(target.position.x, target.position.y, offsetZ.z);
        lastTargetPosition = target.position;
    }

    public void SetTarget(Transform t) {
        target = t;
	} // End of SetTarget().

	// Update is called once per frame
	private void FixedUpdate()
    {
        if (target == null) {
            SetTarget(PlayerController.Inst.transform);
		}

        if (Vector3.SqrMagnitude(lastTargetPosition - target.position) > moveThreshold ) {
            float xMoveDelta = (target.position.x - lastTargetPosition.x);
            float yMoveDelta = (target.position.y - lastTargetPosition.y);
            lastTargetPosition = new Vector3(target.position.x, target.position.y, 0f);
			LookDirection = new Vector3(xMoveDelta, yMoveDelta, 0f).normalized;
		}

		AimDirection = Camera.main.ScreenToWorldPoint(Mouse.current.position.value) - offsetZ - target.position;
		lookAtPos = target.position + Vector3.ClampMagnitude(LookDirection * lookAheadFactor, lookAheadFactor) + offsetZ;

		transform.position = Vector3.SmoothDamp(new Vector3(target.position.x, target.position.y, offsetZ.z), lookAtPos, ref velocity, smoothTime);

	} // End of FixedUpdate().

}
