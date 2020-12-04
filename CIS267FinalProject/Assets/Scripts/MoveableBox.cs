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
            if (distance >= 0 && canMoveLeft)
            {
                //Debug.Log("Im on the left");
                transform.position = new Vector2(Player.transform.position.x + .84f, transform.position.y);
                boxDirection = -1;
                Debug.Log("Box Direction: " + boxDirection);
            }
            else if (distance < 0 && canMoveRight)
            {
                //Debug.Log("Im on the right");
                transform.position = new Vector2(Player.transform.position.x - .833f, transform.position.y);
                boxDirection = 1;
                Debug.Log("Box Direction: " + boxDirection);
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
        if (collision.gameObject.CompareTag("Stopper"))
        {
            if (boxDirection == 1)
            {
                canMoveRight = false;
            }
            else if (boxDirection == -1)
            {
                canMoveLeft = false;
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
