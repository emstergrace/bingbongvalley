using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class RandomPosition : ActionNode
{
    public Vector2 min = Vector2.one * -10;
    public Vector2 max = Vector2.one * 10;
    public bool basedOnLocalPos = false;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (basedOnLocalPos) {
            blackboard.targetPosition.x = context.gameObject.transform.position.x + Random.Range(min.x, max.x);
            blackboard.targetPosition.y = context.gameObject.transform.position.y + Random.Range(min.y, max.y);
        }
        else {
            blackboard.targetPosition.x = Random.Range(min.x, max.x);
            blackboard.targetPosition.y = Random.Range(min.y, max.y);
        }
        return State.Success;
    }
}
