using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    public int attackDamage = 1;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerAttack>().TakeDamage(attackDamage);
        }

    }


}
