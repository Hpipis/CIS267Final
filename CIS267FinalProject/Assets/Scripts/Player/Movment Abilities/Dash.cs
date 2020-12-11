using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private playerMovement pm;
    private PowerUps pu;
    private TrailRenderer tr;
    private Animator animator;

    [SerializeField] private LayerMask groundLayer;

    private AudioSource audioSource;
    public AudioClip playerDashSound;
    public float dashDistance;
    private bool hasDash;
    private float offset = .0f;
    private Vector2 direction = new Vector2(1, 0);

    private float cooldownTime = 1;
    private float nextDashTime = -1;
    public float effectCooldownTime = .2f;
    private float effectTurnOffTime = -1;



    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        pm = GetComponent<playerMovement>();
        audioSource = GetComponent<AudioSource>();
        pu = GetComponent<PowerUps>();
        tr = GetComponent<TrailRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!animator.GetBool("isDead"))
        {
            if (pu.getDashCollectionStatus() == true)
            {
                dashCollision();
                dash();
                if (pm.isGrounded())
                {
                    hasDash = true;
                }
            }
        }
    }
    private void dash()
    {
        //set dash direction
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

        //dash action
        if (Input.GetKeyDown("j") && hasDash && !dashCollision() && (nextDashTime == -1 || Time.time >= nextDashTime))
        {
            //set dash cooldown
            nextDashTime = Time.time + cooldownTime;

            effectTurnOffTime = Time.time + effectCooldownTime;
            if (Time.time <= effectTurnOffTime)
            {
                tr.enabled = true;

            }


            //no double dash in air
            if (!pm.isGrounded())
            {
                hasDash = false;
            }

            //dash movement
            playerRigidBody.position += new Vector2(dashDistance, 0);

            //dash sound
            audioSource.PlayOneShot(playerDashSound);
        }

        //trail effects turn off

        if (Time.time >= effectTurnOffTime)
        {
            tr.enabled = false;
        }
    }

    private bool dashCollision()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position + new Vector3(offset, 0), direction, Mathf.Abs(dashDistance), groundLayer);

        if (rayHit.collider != null)
            //Debug.Log(rayHit.collider.gameObject);

        Debug.DrawRay(transform.position + new Vector3(offset, 0), direction * Mathf.Abs(dashDistance), Color.red);
        return rayHit.collider != null;
    }
}
