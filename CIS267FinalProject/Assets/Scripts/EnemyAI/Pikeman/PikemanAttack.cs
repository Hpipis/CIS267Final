﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikemanAttack : MonoBehaviour
{

    public int attackDamage = 1;

    public Transform Pike;

    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;

    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);

        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerAttack>().TakeDamage(attackDamage);

        }
    }

    private void OnDrawGizmosSelected()
    {
        if (Pike == null)

            return;


        Gizmos.DrawWireSphere(Pike.position, attackRange);
    }


}
