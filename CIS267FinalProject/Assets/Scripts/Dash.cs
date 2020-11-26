using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private playerMovement pm;
    private BoxCollider2D playerBoxCollider;

    [SerializeField] private LayerMask groundLayer;

    public float dashDistance;
    private bool hasDash;
    private float offset;
    private Vector2 direction = new Vector2(2.5f,0);


    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        pm = GetComponent<playerMovement>();
        playerBoxCollider = GetComponent<BoxCollider2D>();
        offset = playerBoxCollider.size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        dashCollision();
        dash();
        if (pm.isGrounded())
            hasDash = true;
    }
    private void dash()
    {

        if (transform.eulerAngles.y == 180)
        {
            direction = new Vector2(-2.5f, 0);
        }
        else
        {
            direction = new Vector2(2.5f, 0);
        }

        if (Input.GetKeyDown("j") && hasDash && !dashCollision())
        {
            if (!pm.isGrounded())
                hasDash = false;
            playerRigidBody.position += direction;
        }
    }

    private bool dashCollision()
    {
        Debug.Log(Physics2D.Raycast(transform.position + new Vector3(offset, 0), direction, groundLayer).collider.gameObject);
        Debug.DrawRay(transform.position + new Vector3(offset, 0), direction, Color.red);
        return Physics2D.Raycast(transform.position + new Vector3(offset, 0), direction, groundLayer).collider != null;
    }
}
