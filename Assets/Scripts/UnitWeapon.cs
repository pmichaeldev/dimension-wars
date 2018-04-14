using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitWeapon : MonoBehaviour
{
    public Transform bulletSpawn;
    public float speed;

    public void Shoot(GameObject target)
    {
        var t = target.transform.Find("RaycastTarget");
        if (t != null)
        {
            var direction = t.transform.position - bulletSpawn.transform.position;
            Debug.DrawRay(bulletSpawn.position, direction, Color.yellow, 5 * Time.deltaTime);
        }

        var unit = GetComponent<Unit>();
        var health = target.GetComponent<UnitHealth>();
        if (health != null)
        {
            health.TakeDamage(10, unit);
        }

        if (unit.unitClass == Unit.Class.HeavyAssault)
        {
            var ds = Vector3.Distance(transform.position, target.transform.position);
            if (ds <= 30f)
            {
                var dir = target.transform.position - transform.position;
                dir.Normalize();
                dir *= 750f;
                var b = target.GetComponent<Rigidbody>();
                if (b != null)
                {
                    b.isKinematic = false;
                    b.AddForce(dir);
                    StartCoroutine(CooldownPush(b));
                    //b.MovePosition(b.transform.position + dir);
                    //b.isKinematic = true;
                }
            }
        }
    }

    IEnumerator CooldownPush(Rigidbody t)
    {
        yield return new WaitForSeconds(0.9f);
        t.isKinematic = true;
    }
}
