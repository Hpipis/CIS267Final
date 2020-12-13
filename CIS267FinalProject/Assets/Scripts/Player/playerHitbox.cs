using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHitbox : MonoBehaviour
{
    public int attackDamage = 1;
    private PlayerAttack hasPlayerAttacked;

    void OnTriggerEnter2D(Collider2D other)
    {
        hasPlayerAttacked = FindObjectOfType<PlayerAttack>();
        Debug.Log("Hit");

        if (hasPlayerAttacked.getAttacking() == true)
        {
            if (other.gameObject.tag == "Enemy" && other.gameObject.name == "Bat Enemy")
            {
                Debug.Log("bat hit!");
                other.GetComponent<EnemyHealth>().Die();
            }

            else if (other.gameObject.tag == "Enemy")
            {
                Debug.Log("Hit Enemy");
                other.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
            }
        }

    }
}
