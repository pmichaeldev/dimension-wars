using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Print", menuName = "Nodes/Composites/Print", order = 2)]
public class PrintNode : BehaviourNode
{
    public string message;

    public override IEnumerator<BehaviourStatus> GetEnumerator()
    {
        MonoBehaviour.print(message);
        yield break;
    }
}
