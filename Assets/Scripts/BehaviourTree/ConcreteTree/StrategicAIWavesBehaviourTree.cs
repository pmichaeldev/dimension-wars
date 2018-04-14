using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BehaviourTree))]
[RequireComponent(typeof(Unit))]
public class StrategicAIWavesBehaviourTree : MonoBehaviour
{
    [SerializeField]
    private BehaviourTree bt;
    public Unit unit;

    void Awake()
    {
        unit = GetComponent<Unit>();
        bt = GetComponent<BehaviourTree>();

        bt.context = new BehaviourNode.Context
        {
            unit = unit
        };

        bt.root = StrategicBehaviour();
    }

    BehaviourNode StrategicBehaviour()
    {
        var root = bt.CreateNode<SelectorNode>();
        root.children = new List<BehaviourNode>
        {
            WinBehaviour(),
            AnnBehaviour(),
            RiflemanBehaviour(),
        };
        return root;
    }

    BehaviourNode AnnBehaviour()
    {
        var root = bt.CreateNode<SequenceNode>();
        root.children = new List<BehaviourNode>
        {
            bt.CreateNode<SetTarget>(),
            bt.CreateNode<OnlyOnce>(),
            bt.CreateNode<Announce>().Initialize("Commander 4: Don't let them escape! Stop them here!"),
        };
        return root;
    }

    BehaviourNode WinBehaviour()
    {
        var p1 = bt.CreateNode<PrintNode>();
        p1.message = "A";
        var p2 = bt.CreateNode<PrintNode>();
        p2.message = "B";
        var root = bt.CreateNode<SequenceNode>();
        root.children = new List<BehaviourNode>
        {
            bt.CreateNode<IsDead>(),
                        p1,
            bt.CreateNode<OnlyOnce>(),
            p2,
            bt.CreateNode<Announce>().Initialize("Final commander killed. Ready to evacuate. You win!"),
        };
        return root;
    }

    BehaviourNode RiflemanBehaviour()
    {
        var root = bt.CreateNode<SelectorNode>();
        root.children = new List<BehaviourNode>
        {
            MoveSequence(),
            CoverSequence(),
            AttackSequence(),
        };
        return root;
    }

    BehaviourNode AttackSequence(float range = 60)
    {
        var sequence = bt.CreateNode<SequenceNode>();
        sequence.children = new List<BehaviourNode>
        {
            bt.CreateNode<SetTarget>().Initialize(range),
            bt.CreateNode<IsGunCooledDown>(),
            bt.CreateNode<ShootTarget>(),
        };
        return sequence;
    }

    BehaviourNode CoverSequence()
    {
        var sequence = bt.CreateNode<SequenceNode>();
        sequence.children = new List<BehaviourNode>
        {
            bt.CreateNode<IsUnderAttack>(),
            bt.CreateNode<SetDestinationToCover>(),
            bt.CreateNode<MoveToDestination>(),
        };
        return sequence;
    }

    BehaviourNode MoveSequence()
    {
        var sequence = bt.CreateNode<SequenceNode>();
        sequence.children = new List<BehaviourNode>
        {
            bt.CreateNode<MoveToDestination>(),
        };
        return sequence;
    }

}
