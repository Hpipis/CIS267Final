using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private playerMovement pm;

    public float dashDistance;
    private bool hasDash;
    private Vector2 direction = new Vector2(1,0);


    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        pm = GetComponent<playerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        dash();
        if (pm.isGrounded())
            hasDash = true;
    }
    private void dash()
    {
        if (Input.GetKeyDown("j") && hasDash && canDash(direction, dashDistance))
        {
            if (!pm.isGrounded())
                hasDash = false;

            if (transform.eulerAngles.y == 180)
            {
                direction = new Vector2(-1, 0);
                playerRigidBody.position += new Vector2(-dashDistance, 0);
            }
            else
            {
                direction = new Vector2(1, 0);
                playerRigidBody.position += new Vector2(dashDistance, 0);
            }
        }
    }

    private bool canDash(Vector3 dir, float distance)
    {
        return Physics2D.Raycast(transform.position, dir, distance).collider == null;
    }
}
