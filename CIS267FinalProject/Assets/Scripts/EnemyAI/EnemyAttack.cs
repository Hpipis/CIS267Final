using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Animator animator;
    public LayerMask attackMask;
    public Transform Weapon;

    private float timeBtwAttack;
    public float startTimeBtwAttack;
    
    public float attackRange = 1f;
    public int attackDamage = 1;
    


    private void Update()
    {
        if (timeBtwAttack <= 0)
        {
            timeBtwAttack = startTimeBtwAttack;

            AttackEnemy();
        }

        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    public void AttackEnemy()
    {
        Collider2D colInfo = Physics2D.OverlapCircle(Weapon.position, attackRange, attackMask);

        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerAttack>().TakeDamage(attackDamage);

        }
    }

    private void OnDrawGizmosSelected()
    {
        if (Weapon == null)

            return;


        Gizmos.DrawWireSphere(Weapon.position, attackRange);
    }



}
