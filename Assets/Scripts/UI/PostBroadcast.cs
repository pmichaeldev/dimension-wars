using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Text))]
public class PostBroadcast : MonoBehaviour
{
    public void Say(string text)
    {
        var textComponent = GetComponent<UnityEngine.UI.Text>();
        textComponent.text = text;
        StartCoroutine(ClearText());
    }

    
    IEnumerator ClearText()
    {
        yield return new WaitForSeconds(10);
        var textComponent = GameObject.Find("AnnounceText").GetComponent<UnityEngine.UI.Text>();
        textComponent.text = "";
        yield return null;
    }
}
