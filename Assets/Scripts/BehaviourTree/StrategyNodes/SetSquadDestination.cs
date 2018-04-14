using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSquadDestination : BehaviourNode
{
    public Squad target;
    public GameObject destination;

    public SetSquadDestination Initialize(Squad target, GameObject destination)
    {
        this.target = target;
        this.destination = destination;
        return this;
    }

    public override IEnumerator<BehaviourStatus> GetEnumerator()
    {
        foreach (var unit in target.Units())
        {
            unit.mover.SetTarget(destination.transform.position);
        }

        yield break;
    }
}
