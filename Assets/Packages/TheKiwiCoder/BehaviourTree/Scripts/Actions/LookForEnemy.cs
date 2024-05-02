using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class LookForEnemy : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        bool foundEntity = context.perception.LookForPlayer(context.perception.AlertRange);
        if (foundEntity) {
            blackboard.targetEntity = PlayerController.Inst;
            blackboard.targetPosition = PlayerController.Inst.transform.position;
            return State.Success;
        }
        else return State.Failure;
    }
}
