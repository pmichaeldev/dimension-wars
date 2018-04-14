using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    public float health;
    public bool isDead;
    public Unit attacker;
    public bool isUnderAttack;

    public void TakeDamage(float damage, Unit attacker)
    {
        if (GetComponent<Unit>().cover != null)
        {
            damage *= 0.8f;
        }

        health -= damage;
        health = Mathf.Max(health, 0);

        StartCoroutine(SetUnderAttack());
        this.attacker = attacker;

        if (health == 0)
        {
            GetComponent<Collider>().enabled = false;
            isDead = true;
        }
    }

    IEnumerator SetUnderAttack()
    {
        if (isUnderAttack)
        {
            yield break;
        }

        isUnderAttack = true;
        yield return new WaitForSeconds(1);
        isUnderAttack = false;
    }
}
