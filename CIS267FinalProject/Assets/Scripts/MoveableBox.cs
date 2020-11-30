using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableBox : MonoBehaviour
{
    GameObject Player;
    Rigidbody2D boxRigidbody;
    private AudioSource audioSource;
    public AudioClip boxMoving;
    
    bool playerCollision = false;

    // Start is called before the first frame update
    void Start()
    {
        boxRigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //if they are pressing the pull key and colliding then dont let them jump
        if (playerCollision && Input.GetKey("k") && !playerOnTopofBox())
        {
            

            float distance = transform.position.x - Player.transform.position.x;
            Player.GetComponent<playerMovement>().setJumping(false);
            if (distance >= 0)
            {
                //Debug.Log("Im on the left");
                transform.position = new Vector2(Player.transform.position.x + 1.13f, transform.position.y);
                
            }
            else
            {
                //Debug.Log("Im on the right");
                transform.position = new Vector2(Player.transform.position.x - 1.13f, transform.position.y);
                
            }
            

        }
        else if (Player != null)
        {
            Player.GetComponent<playerMovement>().setJumping(true);
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player = collision.gameObject;
            playerCollision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerCollision = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Stopper"))
        {
            Debug.Log("Colliding with " + collision.gameObject.tag);
            playerCollision = false;
        }
    }

    private bool playerOnTopofBox()
    {
        if (Player)
        {
            
            float difference = Mathf.Abs(Mathf.Abs(Player.transform.position.y) - Mathf.Abs(transform.position.y));
            //Debug.Log(difference);
            if (difference > .1f)
                return true;
        }

        return false;
    }

   

}
