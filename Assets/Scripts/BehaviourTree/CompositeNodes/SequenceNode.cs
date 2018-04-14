using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sequence", menuName = "Nodes/Composites/Sequence", order = 1)]
public class SequenceNode : BehaviourNode
{
    public override IEnumerator<BehaviourStatus> GetEnumerator()
    {
        foreach (var child in children)
        {
            foreach (var status in child)
            {
                yield return status;
                if (status == BehaviourStatus.Failed)
                {
                    yield break;
                }
            }
        }
    }
}
