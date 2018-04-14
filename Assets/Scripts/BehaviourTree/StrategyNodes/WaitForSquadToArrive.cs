using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForSquadToArrive : BehaviourNode
{
    public Squad target;
    public GameObject destination;
    public float distance;

    public WaitForSquadToArrive Initialize(Squad target, GameObject destination, float distance)
    {
        this.target = target;
        this.destination = destination;
        this.distance = distance;
        return this;
    }

    public override IEnumerator<BehaviourStatus> GetEnumerator()
    {
        float started = Time.time;
        while (true)
        {
            if (Time.time - started > 30)
            {
                Debug.Log("Wait failed!");
                yield return BehaviourStatus.Failed;
            }
            foreach (var unit in target.Units())
            {
                var distance = Vector3.Distance(unit.transform.position, destination.transform.position);
                if (distance < this.distance)
                {
                    yield break;
                }
            }
            yield return BehaviourStatus.Running;
        }
    }
}
