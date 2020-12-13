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
            //if (Pikeman.getAttacking() == true)
            //{
                if (other.gameObject.tag == "Player")
                {
                    other.GetComponent<PlayerAttack>().TakeDamage(attackDamage);
                }
           // }
            
        }
        if (gameObject.CompareTag("King"))
        {
            //commented out because I cannot find where the king or lizard attacks
            King = FindObjectOfType<KingEnemyMain>();
            //if (King.getAttacking() == true)
            //{
                if (other.gameObject.tag == "Player")
                {
                    other.GetComponent<PlayerAttack>().TakeDamage(attackDamage);
                }
            //}

        }
        if (gameObject.CompareTag("Lizard"))
        {
            if (gameObject.CompareTag("Lizard")) 
            {
                //commented out because I cannot find where the king or lizard attacks
                Lizard = FindObjectOfType<LizardEnemyMain>();
                //if (Lizard.getAttacking() == true)
                //{
                    if (other.gameObject.tag == "Player")
                    {
                        other.GetComponent<PlayerAttack>().TakeDamage(attackDamage);
                    }
                //}
            }
        }
        

    }


}
