using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Animator animator;
    public LayerMask attackMask;
    public Transform Weapon;

    private bool cooling;
    public float timer;
    private float intTimer;
    
    public float attackRange = 1f;
    public int attackDamage = 1;

    private void Awake()
    {
        intTimer = timer;
    }

    private void Update()
    {
        if (cooling == false)
        {            
            AttackEnemy();            
        }

        if (cooling == true)
        {
            Cooldown();
        }
       
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling)
        {
            cooling = false;
            timer = intTimer;
        }
    }
    public void TriggerCooling()
    {
        cooling = true;
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
