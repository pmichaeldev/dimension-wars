using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsUnderAttack : BehaviourNode
{
    public override IEnumerator<BehaviourStatus> GetEnumerator()
    {
        var health = context.unit.GetComponent<UnitHealth>();
        if (health.isUnderAttack)
        {
            yield break;
        } else
        {
            yield return BehaviourStatus.Failed;
        }
    }
}
