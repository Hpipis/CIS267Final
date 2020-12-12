using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHitbox : MonoBehaviour
{
    public int attackDamage = 1;


    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit");

        if (other.gameObject.tag == "Enemy" && other.gameObject.name == "Bat Enemy")
        {
            Debug.Log("bat hit!");
            other.GetComponent<EnemyHealth>().Die();
        }

        else if (other.gameObject.tag == "Enemy")
        {
            other.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        }

    }
}
