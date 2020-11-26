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
    private float offset = .6f;
    private Vector2 direction = new Vector2(1,0);


    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        pm = GetComponent<playerMovement>();
        playerBoxCollider = GetComponent<BoxCollider2D>();
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
            direction = new Vector2(-Mathf.Abs(direction.x), 0);
            offset = -Mathf.Abs(offset);
            dashDistance = -Mathf.Abs(dashDistance);
        }
        else
        {
            direction = new Vector2(Mathf.Abs(direction.x), 0);
            offset = Mathf.Abs(offset);
            dashDistance = Mathf.Abs(dashDistance);
        }

        if (Input.GetKeyDown("j") && hasDash && !dashCollision())
        {
            if (!pm.isGrounded())
                hasDash = false;
            playerRigidBody.position += new Vector2(dashDistance, 0);
        }
    }

    private bool dashCollision()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position + new Vector3(offset, 0), direction, Mathf.Abs(dashDistance), groundLayer);

        if (rayHit.collider != null)
            Debug.Log(rayHit.collider.gameObject);

        Debug.DrawRay(transform.position + new Vector3(offset, 0), direction * Mathf.Abs(dashDistance), Color.red);
        return rayHit.collider != null;
    }
}
