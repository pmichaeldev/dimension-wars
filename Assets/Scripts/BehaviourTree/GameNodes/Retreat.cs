using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retreat : BehaviourNode
{
    bool hasRetreated;

    public override IEnumerator<BehaviourStatus> GetEnumerator()
    {
        if (hasRetreated)
        {
            yield break;
        }

        var health = context.unit.GetComponent<UnitHealth>();
        var retreatDirection = context.unit.transform.position - health.attacker.transform.position;
        retreatDirection.Normalize();
        retreatDirection *= 50f;

        //Debug.DrawLine(context.unit.transform.position, health.attacker.transform.position, Color.blue, 100, false);

        var retreatTarget = context.unit.transform.position + retreatDirection;

        //Debug.DrawLine(context.unit.transform.position, retreatTarget, Color.magenta, 100, false);

        context.unit.mover.SetTarget(retreatTarget);
        hasRetreated = true;
        yield break;
    }
}
