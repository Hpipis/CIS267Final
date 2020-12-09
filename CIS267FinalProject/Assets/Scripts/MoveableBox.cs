using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableBox : MonoBehaviour
{
    GameObject Player;
    Rigidbody2D boxRigidbody;
    private AudioSource audioSource;
    public AudioClip boxMoving;

    private bool playerCollision = false;
    private float boxDirection;
    private bool canMoveLeft = true;
    private bool canMoveRight = true;

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
            if (distance >= 0 && canMoveRight)
            {
                //Debug.Log("Player on the left");
                transform.position = new Vector2(Player.transform.position.x + .84f, transform.position.y);
                boxDirection = -1;
                //Debug.Log("Box Direction: " + boxDirection);
            }
            else if (distance < 0 && canMoveLeft)
            {
                //Debug.Log("Player on the right");
                transform.position = new Vector2(Player.transform.position.x - .833f, transform.position.y);
                boxDirection = 1;
                //Debug.Log("Box Direction: " + boxDirection);
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
        if (collision.gameObject.CompareTag("Stopper"))
        {
            canMoveRight = true;
            canMoveLeft = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Stopper"))
        {
            //Debug.Log("Box location: " + transform.position.x);
            //Debug.Log("Collider Location: " + collision.transform.position.x);

            float boxX = transform.position.x;
            float colliderX = collision.transform.position.x;

            if(boxX > colliderX)
            {
                //box is on the right of the collider
                //remove ability for the box to move left
                //Debug.Log("Box cannot move more left!");
                canMoveLeft = false;
            }
            else if(boxX < colliderX)
            {
                //box is on the left of the collider
                //remove ability for the box to move right
                //Debug.Log("Box cannot move more right!");
                canMoveRight = false;
            }
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
