using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    private AudioSource audioSource;

    public AudioClip playerSwordSwingSound;

    public Transform Sword;
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
            Attack();
        }


    }


    void Attack()
    {
        animator.SetTrigger("attack");

        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(Sword.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemy)
        {
            //Debug.Log("HIT" + enemy.name);
            enemy.GetComponent<KingMain>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (Sword == null)
        
            return;
       

        Gizmos.DrawWireSphere(Sword.position, attackRange);
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

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;


    }

    private void Slash()
    {
        Debug.Log("sword");
        audioSource.PlayOneShot(playerSwordSwingSound);
    }
}
