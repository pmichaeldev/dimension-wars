using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BehaviourTree))]
[RequireComponent(typeof(Unit))]
public class StrategicAIFinalBossBehaviourTree : MonoBehaviour
{
    [SerializeField]
    private BehaviourTree bt;
    public Unit unit;
    public Squad reinforcementGroup;
    public GameObject reinforcementTarget;
    public Squad pincerGroup;
    public GameObject pincerTarget;
    public Squad remainderGroup;
    public GameObject remainderTarget;
    public Squad commandSquad;

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
            ReinforceBehaviour(),
            RiflemanBehaviour(),
        };
        return root;
    }

    BehaviourNode ReinforceBehaviour()
    {
        var root = bt.CreateNode<SequenceNode>();
        root.children = new List<BehaviourNode>
        {
            bt.CreateNode<SetTarget>(),
            bt.CreateNode<OnlyOnce>(),
            bt.CreateNode<Announce>().Initialize("Commander 3: There they are! Hold the line!"),
            bt.CreateNode<SetSquadDestination>().Initialize(reinforcementGroup, reinforcementTarget),
            bt.CreateNode<WaitForSquadToArrive>().Initialize(reinforcementGroup, reinforcementTarget, 50),
            bt.CreateNode<Announce>().Initialize("Commander 3: They're pinned! Attack now!"),
            bt.CreateNode<SetSquadDestination>().Initialize(pincerGroup, pincerTarget),
        };
        return root;
    }

    BehaviourNode RetreatBehaviour()
    {
        var root = bt.CreateNode<SequenceNode>();
        root.children = new List<BehaviourNode>
        {
        bt.CreateNode<IsSquadDead>().Initialize(pincerGroup),
        bt.CreateNode<OnlyOnce>(),
                    bt.CreateNode<Announce>().Initialize("Commander 3: We've lost too many forces! Retreat!"),
                    bt.CreateNode<SetSquadDestination>().Initialize(commandSquad, remainderTarget),
                                bt.CreateNode<SetSquadDestination>().Initialize(remainderGroup, remainderTarget),
                                bt.CreateNode<SetSquadDestination>().Initialize(reinforcementGroup, reinforcementTarget),

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
