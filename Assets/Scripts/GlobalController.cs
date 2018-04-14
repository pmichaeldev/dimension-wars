using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{
    public Squad c1;
    public Squad c2;
    public Squad c3;

    public bool c2Active;
    public bool c3Active;
    public bool c4Active;

    public GameObject c1Root;
    public GameObject c2Root;
    public GameObject c3Root;
    public GameObject c4Root;

    private void Update()
    {
        if (c1.IsDead() && !c2Active)
        {
            c2Active = true;
            c2Root.SetActive(true);
        }

        if (c2.IsDead() && !c3Active)
        {
            c3Active = true;
            c3Root.SetActive(true);
            c1Root.SetActive(false);
        }

        if (c3.IsDead() && !c4Active)
        {
            c4Active = true;
            c4Root.SetActive(true);
            c2Root.SetActive(false);
        }
    }
}
