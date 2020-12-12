using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;

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

    //king variables
    private bool kingDead = false;
    private float sceneEndTime = 0;
    private float sceneEndDelay = 5f;



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
        if(kingDead && Time.time >= sceneEndTime)
        {
            SceneManager.LoadScene("EndScreen");
            this.enabled = false;
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

    public void Die()
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

        else if(gameObject.name == "King")
        {
            kingDead = true;
            sceneEndTime += Time.time + sceneEndDelay;
            animator.SetBool("isDead", true);
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            GetComponent<Collider2D>().enabled = false;
            KillCounter k = FindObjectOfType<KillCounter>();
            k.setSceneKills(k.getSceneKills() + 1);
            GetComponent<KingEnemyMain>().enabled = false;
        }

        else if(gameObject.name == "Pikeman")
        {
            Debug.Log("DEAD");
            animator.SetBool("isDead", true);
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            GetComponent<Collider2D>().enabled = false;
            KillCounter k = FindObjectOfType<KillCounter>();
            k.setSceneKills(k.getSceneKills() + 1);
            GetComponent<PikemanEnemyMain>().enabled = false;
            Debug.Log("pikeman enemy script disabled");
            this.enabled = false;
        }

        else if (gameObject.name == "Lizard")
        {
            Debug.Log("DEAD");
            animator.SetBool("isDead", true);
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            GetComponent<Collider2D>().enabled = false;
            KillCounter k = FindObjectOfType<KillCounter>();
            k.setSceneKills(k.getSceneKills() + 1);
            GetComponent<LizardEnemyMain>().enabled = false;
            this.enabled = false;
        }
    }
}
