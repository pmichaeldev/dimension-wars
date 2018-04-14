using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squad : MonoBehaviour
{
    public int team;

    public Unit[] Units()
    {
        return transform.GetComponentsInChildren<Unit>();
    }

    public void SetTarget(Unit target)
    {
        var squadUnits = transform.GetComponentsInChildren<Unit>();

        foreach (var squadUnit in squadUnits)
        {
            squadUnit.SetTarget(target);
        };

    }

    public void SetImmediateMoveTarget(Vector3 pos)
    {
        var squadUnits = transform.GetComponentsInChildren<Unit>();

        foreach (var squadUnit in squadUnits)
        {
            squadUnit.SetImmediateMoveTarget(pos);
        }
    }

    public void SetCoverTarget(GameObject cover)
    {
        // TODO Use context sens. choice.
        var side = cover.transform.Find("Front");
        var i = 1;
        var squadUnits = transform.GetComponentsInChildren<Unit>();
        foreach (var unit in squadUnits)
        {
            var pointName = "CoverPos" + i;
            var pointTarget = side.Find(pointName);
            unit.SetImmediateMoveTarget(pointTarget.transform.position);
            i++;
        }
        print("Moved to cover!");
    }

    public bool IsDead()
    {
        var unitCount = Units().Length;
        var deadCount = 0;
        foreach (var unit in Units())
        {
            if (unit.GetComponent<UnitHealth>().isDead)
            {
                deadCount++;
            }
        }
        return deadCount == unitCount;
    }
}
