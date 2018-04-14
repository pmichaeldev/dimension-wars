using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsDead : BehaviourNode
{
    public override IEnumerator<BehaviourStatus> GetEnumerator()
    {
        var health = context.unit.GetComponent<UnitHealth>();
        if (health.isDead)
        {
            yield break;
        }
        yield return BehaviourStatus.Failed;
    }
}
