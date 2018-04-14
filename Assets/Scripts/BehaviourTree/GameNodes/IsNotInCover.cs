using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsNotInCover : BehaviourNode
{
    public override IEnumerator<BehaviourStatus> GetEnumerator()
    {
        if (context.unit.cover == null)
        {
            yield break;
        }
        yield return BehaviourStatus.Failed;
    }
}
