using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPerception : MonoBehaviour
{
    [SerializeField] private float sightRange = 10f; public float SightRange { get { return sightRange; } }// Sight range after alert
    [SerializeField] private float alertRange = 7.5f;
    public float AlertRange { get { return Mathf.Pow(alertRange, 2f); } }

    [SerializeField] private float maxAngle = 75f;

    private float sqrDistToPlayer = 0f;
    private Vector2 directionToPlayer; public Vector2 DirectionToPlayer { get { return directionToPlayer; } }

    private Vector2 lineOfSight = Vector2.zero;

    [SerializeField] private BaseCombatController combatController = null;

    public void SetLOS(Vector2 dir) {
        lineOfSight = dir.normalized * alertRange;
    } // End of SetLOS().

    public bool LookForPlayer(float range = 0f) {
        // Default to alert range
        if (range == 0f) range = alertRange;
        // Distance
        if (sqrDistToPlayer > range) return false;

        // Angle
        float angle = Vector2.Angle(directionToPlayer, lineOfSight);
        if (angle > maxAngle) return false;

        // Line of sight, can see player
        if (DirectLOSToPlayer(range))
            return true;
        return false;
    } // End of LookForPlayer().

    private bool DirectLOSToPlayer(float distance) {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, distance, ~(ResourceLibrary.AttackMask | ResourceLibrary.EnemyMask | ResourceLibrary.CharacterBlockerMask)); // Ignore attacks, other enemies, and kinetic colliders
        return hit.collider != null && hit.collider.gameObject.layer == 8;
    } // End of CanSeePlayer().

    private void Update() {
        sqrDistToPlayer = Vector2.SqrMagnitude(transform.position - PlayerController.Inst.transform.position);
        directionToPlayer = PlayerController.Inst.transform.position - transform.position;
    } // End of Update().

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position, alertRange);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(gameObject.transform.position, lineOfSight);
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(gameObject.transform.position, directionToPlayer);
    } // End of OnDrawGizmos().
#endif
}
