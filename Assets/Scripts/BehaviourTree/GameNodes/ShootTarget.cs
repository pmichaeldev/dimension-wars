using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTarget : BehaviourNode
{
    public float cooldown = 1;

    public override IEnumerator<BehaviourStatus> GetEnumerator()
    {
        if (context.target != null)
        {
            Shoot();
        }

        if (context.unit.target != null)
        {
            Shoot();

        }

        yield break;
    }

    void Shoot()
    {
        // Shoot weapon
        context.unit.weapon.Shoot(context.target.gameObject);
        context.unit.gunCooldown += cooldown;

        // Look at target
        context.unit.transform.LookAt(context.target.transform);

        // Update animation
        var animController = context.unit.GetComponent<UnitAnimationController>();
        if (animController != null)
        {
            animController.Shoot();
        }
    }
}
