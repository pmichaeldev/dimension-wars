using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMovement : BehaviourNode
{
    public override IEnumerator<BehaviourStatus> GetEnumerator()
    {
        var mover = context.unit.GetComponent<UnitMover>();
        mover.agent.isStopped = true;
        yield break;
    }
}
