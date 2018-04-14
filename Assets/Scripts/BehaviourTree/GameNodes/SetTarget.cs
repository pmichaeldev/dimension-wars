using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTarget : BehaviourNode
{
    public float targetingDistance = 75;

    public SetTarget Initialize(float targetingDistance)
    {
        this.targetingDistance = targetingDistance;
        return this;
    }

    public override IEnumerator<BehaviourStatus> GetEnumerator()
    {
        var units = FindObjectsOfType<Unit>();
        foreach (var unit in units)
        {
            // Don't target dead units
            var targetHealth = unit.GetComponent<UnitHealth>();
            if (targetHealth.isDead)
            {
                continue;
            }

            // Don't target own team
            if (unit.squad.team == context.unit.squad.team)
            {
                continue;
            }

            // Don't target already targeted
            if (targetHealth.isUnderAttack)
            {
                continue;
            }


            var distance = Vector3.Distance(context.unit.transform.position, unit.transform.position);
            if (distance < targetingDistance)
            {
                // Trace LOS
                var start = context.unit.transform.Find("RaycastTarget").transform.position;
                var end = unit.transform.Find("RaycastTarget").transform.position;
                var direction = end - start;
                var d = Vector3.Distance(start, end);
                RaycastHit hit;

                //Debug.DrawRay(start, direction, Color.white, Time.deltaTime);

                // If found target
                if (Physics.Raycast(start, direction, out hit, d))
                {
                    // Check if valid target
                    var hitUnit = hit.collider.GetComponent<Unit>();
                    if (hitUnit == unit)
                    {
                        context.target = unit;
                        yield break;
                    }
                }
            }
        }

        // Try 2
        foreach (var unit in units)
        {
            // Don't target dead units
            var targetHealth = unit.GetComponent<UnitHealth>();
            if (targetHealth.isDead)
            {
                continue;
            }

            // Don't target own team
            if (unit.squad.team == context.unit.squad.team)
            {
                continue;
            }

            var distance = Vector3.Distance(context.unit.transform.position, unit.transform.position);
            if (distance < targetingDistance)
            {
                // Trace LOS
                var start = context.unit.transform.Find("RaycastTarget").transform.position;
                var end = unit.transform.Find("RaycastTarget").transform.position;
                var direction = end - start;
                var d = Vector3.Distance(start, end);
                RaycastHit hit;

                //Debug.DrawRay(start, direction, Color.white, Time.deltaTime);

                // If found target
                if (Physics.Raycast(start, direction, out hit, d))
                {
                    // Check if valid target
                    var hitUnit = hit.collider.GetComponent<Unit>();
                    if (hitUnit == unit)
                    {
                        context.target = unit;
                        yield break;
                    }
                }
            }
        }

        yield return BehaviourStatus.Failed;
    }
}
