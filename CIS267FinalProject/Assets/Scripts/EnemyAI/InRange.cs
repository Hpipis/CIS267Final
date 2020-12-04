using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class InRange : MonoBehaviour
{
    public GameObject[] enemies;

    private GameObject enemy;
    public GameObject resetPos;
    public GameObject player;

    private void Start()
    {
        Debug.Log("enemy array " + enemies.Length);
        for (int i = 0; i < enemies.Length; i++)
        {

            enemy = enemies[i];
            Debug.Log(enemy.name + "enemy value");

            enemy.GetComponent<AIPath>().canSearch = false;


        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemy.GetComponent<AIDestinationSetter>().target = player.transform;
                enemy = enemies[i];

                enemy.GetComponent<AIPath>().canSearch = true;

            }
        }    
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                enemy = enemies[i];

                enemy.GetComponent<AIPath>().canSearch = false;
                enemy.GetComponent<AIDestinationSetter>().target = resetPos.transform;

            }
        }

    }


}
