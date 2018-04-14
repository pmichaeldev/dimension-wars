using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BehaviourNode : ScriptableObject, IEnumerable<BehaviourStatus>
{
    public List<BehaviourNode> children;

    public Context context;

    public abstract IEnumerator<BehaviourStatus> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new System.NotImplementedException();
    }

    public class Context
    {
        public Unit unit;
        public Unit target;
        public float waitFor;
    }
}
