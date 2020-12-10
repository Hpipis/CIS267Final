using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingMain : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    private AudioSource audioSource;

    public AudioClip kingChargingSound;
    public AudioClip kingImpactSound;
    public AudioClip kingAttackSound;
    public AudioClip kingFizzleSound;
    public AudioClip kingCrownDropSound;
    public AudioClip walking;
    public bool isFlipped = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }



    public void lookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }

        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    private void KingCharge()
    {
        audioSource.PlayOneShot(kingChargingSound);
    }

    private void KingAttack()
    {
        audioSource.PlayOneShot(kingAttackSound);
    }

    private void AttackImpact()
    {
        audioSource.PlayOneShot(kingImpactSound);
    }

    private void KingCrownDrop()
    {
        audioSource.PlayOneShot(kingCrownDropSound);
    }
    private void KingFizzle()
    {
        audioSource.PlayOneShot(kingFizzleSound);
    }

    private void WalkingHuman()
    {
        audioSource.PlayOneShot(walking);
    }



}
