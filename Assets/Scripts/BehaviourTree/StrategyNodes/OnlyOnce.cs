using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyOnce : BehaviourNode
{
    public bool fired;

    public override IEnumerator<BehaviourStatus> GetEnumerator()
    {
        if (fired)
        {
            yield return BehaviourStatus.Failed;
        }
        fired = true;
    }
}
