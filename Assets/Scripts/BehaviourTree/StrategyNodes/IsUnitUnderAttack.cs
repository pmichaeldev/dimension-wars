using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsUnitUnderAttack : BehaviourNode
{
    public Unit target;

    public IsUnitUnderAttack Initialize(Unit target)
    {
        this.target = target;
        return this;
    }

    public override IEnumerator<BehaviourStatus> GetEnumerator()
    {
        var health = target.GetComponent<UnitHealth>();
        if (health.isUnderAttack)
        {
            yield break;
        }
        else
        {
            yield return BehaviourStatus.Failed;
        }
    }
}
