using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class IsTargetWithinRange : ActionNode
{
    public float range = 0.2f;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (Vector2.SqrMagnitude(blackboard.targetPosition - context.transform.position) < range)
            return State.Success;
        else
            return State.Failure;
    }
}
