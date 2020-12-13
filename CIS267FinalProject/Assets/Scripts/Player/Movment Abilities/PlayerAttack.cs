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

    private bool hasAttacked = false;
    private float hasAttackedResetTimer = 0f;
    private float hasAttackedNextStateSwitch = .6f;

    public LayerMask enemyLayers;

    void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //reset the variable
        if (hasAttacked == true && Time.time >= hasAttackedResetTimer)
        {
            hasAttacked = false;
        }


        if (Input.GetKeyDown(KeyCode.H))
        {
            hasAttackedResetTimer = Time.time + hasAttackedNextStateSwitch;
            hasAttacked = true;
            animator.SetTrigger("attack");
        }

        if(playerDead && Time.time >= resetTime)
        {
            Scene cur = SceneManager.GetActiveScene();
            SceneManager.LoadScene(cur.name);
            this.enabled = false;
        }
        Debug.Log("hasAttacked Status" + hasAttacked);

       
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

    public bool getAttacking()
    {
        return hasAttacked;
    }
}
