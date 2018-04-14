using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Announce : BehaviourNode
{
    public string text;

    public Announce Initialize(string text)
    {
        this.text = text;
        return this;
    }

    public override IEnumerator<BehaviourStatus> GetEnumerator()
    {
        //Debug.DrawLine(context.unit.transform.position, context.target.transform.position, Color.magenta, 100, false);
        var textComponent = GameObject.Find("AnnounceText").GetComponent<PostBroadcast>();
        textComponent.Say(text);
        Debug.Log("MESSAGE: " + text);
        yield break;
    }

}
