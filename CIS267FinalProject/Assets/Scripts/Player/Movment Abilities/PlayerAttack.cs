using System.Collections;
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
        Debug.Log("HIT");
        

        Collider2D colInfo = Physics2D.OverlapCircle(Weapon.position, attackRange, enemyLayers);

        if (colInfo != null)
        {
            colInfo.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
            colInfo.GetComponent<LizardHealth>().TakeDamage(attackDamage);

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
