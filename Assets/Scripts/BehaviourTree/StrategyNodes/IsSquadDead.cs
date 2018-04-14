using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSquadDead : BehaviourNode
{
    public Squad squad;

    public IsSquadDead Initialize(Squad squad)
    {
        this.squad = squad;
        return this;
    }

    public override IEnumerator<BehaviourStatus> GetEnumerator()
    {
        if (squad.IsDead())
        {
            yield break;
        }
        yield return BehaviourStatus.Failed;
    }
}
