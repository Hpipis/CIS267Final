using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyHealth : MonoBehaviour
{
    public Animator animator;
    private AudioSource audioSource;

    public AudioClip enemyHurt;

    public int maxHealth = 1;
    int currentHealth;

    //bat variables
    private bool batDead = false;
    private float despawnTime = 0;
    private float despawnOffset = 1f;

    void Start()
    {        
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (batDead)
        {
            if (Time.time >= despawnTime)
            {
                //Debug.Log("DESPAWN TIME: " + despawnTime);
                //Debug.Log("NORMAL TIME: " + Time.time);
                GetComponentInChildren<SpriteRenderer>().enabled = false;
                this.enabled = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        audioSource.PlayOneShot(enemyHurt);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if(gameObject.name == "Bat Enemy")
        {
            batDead = true;
            despawnTime += Time.time + despawnOffset;
            Debug.Log("DEAD");
            animator.SetBool("isDead", true);
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<AIDestinationSetter>().enabled = false;
            KillCounter k = FindObjectOfType<KillCounter>();
            k.setSceneKills(k.getSceneKills() + 1);
        }

        else
        {
            Debug.Log("DEAD");
            animator.SetBool("isDead", true);
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            GetComponent<EnemyAttack>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            KillCounter k = FindObjectOfType<KillCounter>();
            k.setSceneKills(k.getSceneKills() + 1);
            this.enabled = false;
        }
    }
}
