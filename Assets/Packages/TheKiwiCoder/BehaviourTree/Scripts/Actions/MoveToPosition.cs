using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class MoveToPosition : ActionNode
{

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        if (CloseToPosition()) {
            context.moveController.HandleInput(Vector2.zero);
            return State.Success;
		}

        context.moveController.HandleInput((blackboard.targetPosition - context.gameObject.transform.position).normalized);
        return State.Running;
    }

    private bool CloseToPosition() {
        return Vector2.SqrMagnitude(context.gameObject.transform.position - blackboard.targetPosition) < 0.1f;
    }
}
