using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    GameObject[] front = new GameObject[4];
    GameObject[] back = new GameObject[4];
    Unit[] frontUnits = new Unit[4];
    Unit[] backUnits = new Unit[4];
    Transform f;
    Transform b;

    void Awake()
    {
        f = transform.Find("Front");

        b = transform.Find("Back");

        for (int i = 0; i < 4; i++)
        {
            var c = f.transform.Find("CoverPos" + (i + 1));
            front[i] = c.gameObject;
        }
        for (int i = 0; i < 4; i++)
        {
            var c = b.transform.Find("CoverPos" + (i + 1));
            back[i] = c.gameObject;
        }
    }

    public Vector3? CheckIn(Unit unit, Unit enemy, bool set)
    {
        if (enemy != null)
        {
            var fd = Vector3.Distance(f.position, enemy.transform.position);
            var bd = Vector3.Distance(b.position, enemy.transform.position);
            if (fd < bd)
            {
                return Park(unit, back, backUnits, set);

            }
            else
            {
                return Park(unit, front, frontUnits, set);

            }
        }
        else
        {
            var fd = Vector3.Distance(f.position, unit.transform.position);
            var bd = Vector3.Distance(b.position, unit.transform.position);
            if (fd < bd)
            {
                return Park(unit, front, frontUnits, set);
            }
            else
            {
                return Park(unit, back, backUnits, set);
            }

        }
    }

    Vector3? Park(Unit unit, GameObject[] icos, Unit[] held, bool set)
    {
        for (int i = 0; i < 4; i++)
        {
            if (held[i] == null)
            {
                if (set)
                {
                    held[i] = unit;
                }
                return icos[i].transform.position;
            }
        }
        return null;
    }

    public void CheckOut(Unit unit)
    {
        for (int i = 0; i < 4; i++)
        {
            if (frontUnits[i] != null && frontUnits[i].Equals(unit))
            {
                frontUnits[i] = null;
            }
            else if (backUnits[i] != null && backUnits[i].Equals(unit))
            {

                backUnits[i] = null;
            }
        }
    }
}
