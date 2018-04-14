using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BehaviourTree))]
[RequireComponent(typeof(Unit))]
public class StrategicAISpotterBehaviourTree : MonoBehaviour
{
    [SerializeField]
    private BehaviourTree bt;
    public Unit unit;

    public Squad watch1;
    //public Squad watch2;
    public Squad reinf1;
    public Squad reinf2;
    public Squad reinf3;
    public Squad comm;
    public GameObject r1;
    public GameObject r2;
    public GameObject r3;
    public GameObject r4;

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
            ChargeSequence(),
            RetreatSequence(),
            RiflemanBehaviour(),
        };
        return root;
    }

    BehaviourNode ChargeSequence()
    {
        var root = bt.CreateNode<SequenceNode>();
        root.children = new List<BehaviourNode>
        {
            bt.CreateNode<SetTarget>(),
            bt.CreateNode<OnlyOnce>(),
            bt.CreateNode<Announce>().Initialize("Commander 2: There he is! Charge!"),
        };
        return root;
    }

    BehaviourNode RetreatSequence()
    {
        var root = bt.CreateNode<SequenceNode>();
        root.children = new List<BehaviourNode>
        {
            bt.CreateNode<IsSquadDead>().Initialize(watch1),
            //bt.CreateNode<IsSquadDead>().Initialize(watch2),
            bt.CreateNode<OnlyOnce>(),
            bt.CreateNode<Announce>().Initialize("Commander 2: We lost our front line! Retreat!"),
            bt.CreateNode<SetSquadDestination>().Initialize(reinf1, r1),
                        bt.CreateNode<SetSquadDestination>().Initialize(reinf2, r2),
                                    bt.CreateNode<SetSquadDestination>().Initialize(reinf3, r3),
                                                bt.CreateNode<SetSquadDestination>().Initialize(comm, r4),

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
