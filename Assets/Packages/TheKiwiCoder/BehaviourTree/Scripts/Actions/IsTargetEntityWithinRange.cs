using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class IsTargetEntityWithinRange : ActionNode
{
    public float range = 0.2f;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (blackboard.targetEntity == null) return State.Failure;

        if (Vector2.SqrMagnitude(blackboard.targetEntity.transform.position - context.transform.position) < range)
            return State.Success;
        else
            return State.Failure;
    }
}
