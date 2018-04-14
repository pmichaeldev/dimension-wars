using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : BehaviourNode
{
    public override IEnumerator<BehaviourStatus> GetEnumerator()
    {
        //Destroy(context.unit.gameObject, 1);

        yield break;
    }
}
