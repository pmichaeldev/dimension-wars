using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDestinationToCover : BehaviourNode
{
    float maxDistance = 30;

    public override IEnumerator<BehaviourStatus> GetEnumerator()
    {
        var coverSelectionBoxes = GameObject.FindGameObjectsWithTag("CoverPoint");
        var closest = 100f;
        Cover target = null;
        foreach (var cover in coverSelectionBoxes)
        {
            var controller = cover.transform.parent.GetComponent<Cover>();
            var spot = controller.CheckIn(context.unit, context.target, false);
            if (!spot.HasValue)
            {
                continue;
            }

            //Debug.DrawLine(context.unit.transform.position, cover.transform.position, Color.magenta, 100, false);
            var d = Vector3.Distance(context.unit.transform.position, spot.Value);
            if (d < maxDistance && d < closest)
            {
                closest = d;
                target = controller;
            }
        }

        if (target != null)
        {
            var p = target.CheckIn(context.unit, context.target, true);
            context.unit.mover.SetTarget(p.Value);
            context.unit.cover = target;
            //Debug.DrawLine(context.unit.transform.position, node.transform.position, Color.white, 100, false);
        }
        else
        {
            yield return BehaviourStatus.Failed;
        }
    }
}
