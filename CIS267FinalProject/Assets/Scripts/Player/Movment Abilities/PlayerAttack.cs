﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    private AudioSource audioSource;

    public AudioClip playerSwordSwingSound;

    public Transform Weapon;
    public float attackRange = 0.5f;
    public int attackDamage = 1;

    public int maxHealth = 1;
    int currentHealth;

    public LayerMask enemyLayers;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            animator.SetTrigger("attack");
        }


    }


    void AttackPlayer()
    {
        

        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(Weapon.position, attackRange, enemyLayers);

        for (int i = 0; i < hitEnemy.Length; i++)
        {
            Debug.Log("HIT");
            hitEnemy[i].GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (Weapon == null)
        
            return;
       

        Gizmos.DrawWireSphere(Weapon.position, attackRange);
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("DEAD");
        animator.SetBool("isDead", true);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;


    }

    private void Slash()
    {
        Debug.Log("sword");
        audioSource.PlayOneShot(playerSwordSwingSound);
    }
}