using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BehaviourTree))]
[RequireComponent(typeof(Unit))]
public class SoldierBehaviourTree : MonoBehaviour
{
    public BehaviourTree bt;
    public Unit unit;

    void Awake()
    {
        unit = GetComponent<Unit>();
        bt = GetComponent<BehaviourTree>();

        bt.context = new BehaviourNode.Context
        {
            unit = unit
        };

        if (unit.unitClass == Unit.Class.HeavyAssault)
        {
            bt.root = HeavyAssaultBehaviour();
        }
        else if (unit.unitClass == Unit.Class.Sniper)
        {
            bt.root = SniperBehaviour();
        }
        //else if (unit.unitClass == Unit.Class.MG)
        //{

        //}
        else
        {
            bt.root = RiflemanBehaviour();
        }
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

    BehaviourNode HeavyAssaultBehaviour()
    {
        var root = bt.CreateNode<SelectorNode>();
        root.children = new List<BehaviourNode>
        {
            ChargeSequence(),
        };
        return root;
    }

    BehaviourNode SniperBehaviour()
    {
        var root = bt.CreateNode<SelectorNode>();
        root.children = new List<BehaviourNode>
        {
            RetreatSequence(),
            AttackSequence(150),
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
            bt.CreateNode<IsNotInCover>(),
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

    BehaviourNode ChargeSequence()
    {
        var sequence = bt.CreateNode<SequenceNode>();
        sequence.children = new List<BehaviourNode>
        {
            bt.CreateNode<SetTarget>(),
            bt.CreateNode<SetDestinationToTarget>(),
            bt.CreateNode<IsGunCooledDown>(),
            bt.CreateNode<ShootTarget>(),
        };
        return sequence;
    }

    BehaviourNode RetreatSequence()
    {
        var sequence = bt.CreateNode<SequenceNode>();
        sequence.children = new List<BehaviourNode>
        {
            bt.CreateNode<IsUnderAttack>(),
            bt.CreateNode<Retreat>(),
            bt.CreateNode<MoveToDestination>(),
        };
        return sequence;
    }
}
