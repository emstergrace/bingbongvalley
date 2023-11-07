using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform target;
    private Transform origTarget;

    [Tooltip("The speed at which the camera pans when doing cinematics")]
    [SerializeField] private float panSpeed = 1f;

    [Tooltip("")]
    [SerializeField] private float damping = 1;

    [Tooltip("How far the player looks ahead")]
    [SerializeField] private float lookAheadFactor = 3;

    [Tooltip("The speed at which the camera returns to the player")]
    [SerializeField]private float lookAheadReturnSpeed = 0.5f;

    [Tooltip("How much movement before the camera updates")]
    [SerializeField]private float lookAheadMoveThreshold = 0.1f;

    [Tooltip("How much sudden change in position before camera locks to player")]
    [SerializeField] private float suddenMoveThreshold = 1f;

    private float offsetZ;
    private Vector3 lastTargetPosition;
    private Vector3 currentVelocity;
    private Vector3 lookAheadPos;
    [SerializeField] private float yRest;
    [SerializeField] private float yMargin = 1f; // Distance in the y axis the player can move before the camera follows.

    private float origLookAheadFactor;

    private bool isCinematic = false;

    #region singleton
    private static CameraController _instance;
    public static CameraController Inst { get { return _instance; } }

    private void Awake()
    {
        //Removes player duplicates
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion


    // Use this for initialization
    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("Error! No target for the camera found");
            return;
        }
        origTarget = target;
        lastTargetPosition = target.position;
        offsetZ = (transform.position - target.position).z;
        yRest = Camera.main.orthographicSize * 3 / 8; // / 2;
        yMargin = Camera.main.orthographicSize / 2; // 5 / 8;
       // returnedToPlayer = false;    
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!isCinematic)
            TrackTarget();
    } 

    private void TrackTarget() {
        
        if (target != null) {
            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target.position - lastTargetPosition).x;
            float yMoveDelta = (target.position - lastTargetPosition).y;
            
            if (xMoveDelta > suddenMoveThreshold || yMoveDelta > suddenMoveThreshold) {
                transform.position = new Vector3(target.position.x, target.position.y + yMargin, -1f);
                lastTargetPosition = target.position;
                return;
			}

            bool updateLookAheadTargetX = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;
            bool updateLookAheadTargetY = Mathf.Abs(yMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTargetX || updateLookAheadTargetY) {
                Vector3 dir = new Vector3(1, 1, 0); //Direction of look ahead

                if (xMoveDelta == 0)
                    dir.x = 0;
                else dir.x *= Mathf.Sign(xMoveDelta);

                if (yMoveDelta == 0)
                    dir.y = 0;
                else dir.y *= Mathf.Sign(yMoveDelta);

                lookAheadPos = lookAheadFactor * dir;
            } else {
                lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.fixedDeltaTime * lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target.position + lookAheadPos + Vector3.up * yRest + Vector3.forward * offsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref currentVelocity, damping);
            if (CheckYMargin()) {
                //aheadTargetPos = new Vector3(aheadTargetPos.x, target.position.y + yMargin, aheadTargetPos.z);
                newPos = new Vector3(newPos.x, target.position.y + yMargin, newPos.z);
            }

            transform.position = newPos;
            lastTargetPosition = target.position;
        }

    } // End of TrackTarget().

    private bool CheckYMargin() {
        // Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
        return Mathf.Abs(transform.position.y - target.position.y) > yMargin;
    }

    public void FollowTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void FollowTarget(Transform newTarget, float time)
    {
        StartCoroutine(FollowTargetRoutine(newTarget, time, false));
    }

    public void FollowTarget(Transform newTarget, float time, bool pauseEnemies)
    {
        StartCoroutine(FollowTargetRoutine(newTarget, time, pauseEnemies));
    }

    IEnumerator FollowTargetRoutine(Transform t, float seconds, bool pauseEnemies)
    {
        origTarget = target;
        isCinematic = true;
        origLookAheadFactor = lookAheadFactor;
        lookAheadFactor = 0f;
        FollowTarget(t);

        yield return new WaitForSeconds(seconds);
        
        FollowTarget(origTarget);
        yield return new WaitForSeconds(0.5f);

        lookAheadFactor = origLookAheadFactor;
        isCinematic = false;
    }

    public void ShakeScreen(float t, float intensity)
    {
        StartCoroutine(ShakeEffect(t, intensity));
    }

    IEnumerator ShakeEffect(float t, float intensity)
    {
        float timer = t;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            Vector2 nextPos = new Vector2(transform.position.x, transform.position.y) + Random.insideUnitCircle * intensity;
            transform.position = new Vector3(nextPos.x, nextPos.y, -10f);
            yield return new WaitForFixedUpdate();
        }

        yield return null;
    }

}
