using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : BehaviourNode
{
    public override IEnumerator<BehaviourStatus> GetEnumerator()
    {
        foreach (var child in children)
        {
            foreach (var status in child)
            {
                yield return status;
            }
        }
    }
}
