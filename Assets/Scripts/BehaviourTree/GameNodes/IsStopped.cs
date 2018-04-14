using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsStopped : BehaviourNode
{
    public override IEnumerator<BehaviourStatus> GetEnumerator()
    {
        var velocity = context.unit.mover.agent.velocity;
        if (velocity.magnitude == 0)
        {
            yield break;
        }
        yield return BehaviourStatus.Failed;
    }
}
