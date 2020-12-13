using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    public int attackDamage = 1;
    private PikemanEnemyMain Pikeman;
    private LizardEnemyMain Lizard;
    private KingEnemyMain King;

    void OnTriggerEnter2D(Collider2D other)
    {
        //this if chain semi fixes the issue with running up to the enemy and them killing you without attacking
        if (gameObject.CompareTag("Pikeman"))
        {

            Pikeman = FindObjectOfType<PikemanEnemyMain>();
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<PlayerAttack>().TakeDamage(attackDamage);
            }

        }
        if (gameObject.CompareTag("King"))
        {
            King = FindObjectOfType<KingEnemyMain>();
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<PlayerAttack>().TakeDamage(attackDamage);
            }

        }
        if (gameObject.CompareTag("Lizard"))
        {
            Lizard = FindObjectOfType<LizardEnemyMain>();
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<PlayerAttack>().TakeDamage(attackDamage);
            }
        }
        

    }


}
