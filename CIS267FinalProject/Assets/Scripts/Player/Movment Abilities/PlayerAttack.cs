using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    private AudioSource audioSource;

    public AudioClip playerSwordSwingSound;
    public AudioClip playerHurt;

    public Transform Weapon;
    public float attackRange = 0.5f;
    public int attackDamage = 1;

    public int maxHealth = 1;
    int currentHealth;

    private float resetTime = 0;
    private float resetOffset = 1f;
    private bool playerDead = false;

    public LayerMask enemyLayers;

    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            animator.SetTrigger("attack");
        }

        if(playerDead && Time.time >= resetTime)
        {
            Scene cur = SceneManager.GetActiveScene();
            SceneManager.LoadScene(cur.name);
            this.enabled = false;
        }

    }


    void AttackPlayer()
    {
        //Debug.Log("HIT");
        

        Collider2D colInfo = Physics2D.OverlapCircle(Weapon.position, attackRange, enemyLayers);

        if (colInfo != null)
        {
            Debug.Log(colInfo.gameObject.name);
            colInfo.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
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
        audioSource.PlayOneShot(playerHurt);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("DEAD");
        playerDead = true;
        animator.SetBool("isDead", true);
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<Collider2D>().enabled = false;
        resetTime += Time.time + resetOffset;
    }

    private void Slash()
    {
        Debug.Log("sword");
        audioSource.PlayOneShot(playerSwordSwingSound);
    }
}
