using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : MonoBehaviour
{
    public BehaviourNode root;

    public BehaviourNode.Context context;

    void Start()
    {
        StartCoroutine(Execute());
    }

    IEnumerator Execute()
    {
        var health = context.unit.GetComponent<UnitHealth>();
        while (true)
        {
            // Handle death
            if (health.isDead)
            {
                HandleDie();
                yield break;
            }

            yield return StartCoroutine(ExecuteRoot());
        }
    }

    IEnumerator ExecuteRoot()
    {
        var health = context.unit.GetComponent<UnitHealth>();
        foreach (var status in root)
        {
            // Handle death
            if (health.isDead)
            {
                HandleDie();
                yield break;
            }

            // Do actions
            if (status == BehaviourStatus.Running && context.waitFor != 0)
            {
                var waitFor = context.waitFor;
                context.waitFor = 0;
                yield return new WaitForSeconds(waitFor);
            }
            else
            {
                yield return new WaitForFixedUpdate();
            }
        }
    }

    void HandleDie()
    {
        if (context.unit.cover != null)
        {
            context.unit.cover.CheckOut(context.unit);
        }
    }

    public T CreateNode<T>() where T : BehaviourNode
    {
        var instance = ScriptableObject.CreateInstance<T>();
        instance.context = context;
        return instance;
    }
}
