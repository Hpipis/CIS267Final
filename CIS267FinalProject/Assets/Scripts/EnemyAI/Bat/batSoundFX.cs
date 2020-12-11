using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batSoundFX : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip screach;
    public AudioClip flapping;
    public AudioClip death;

    public Transform player;

    float distanceToPlayer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.position);
    }
    private void BatAttack()
    {
        audioSource.PlayOneShot(screach);
    }

    private void BatDead()
    {
        audioSource.PlayOneShot(death);
    }

    private void BatFlapping()
    {
        if (distanceToPlayer < 30 && distanceToPlayer >= 20)
        {
            audioSource.volume = 0.15f;
            audioSource.PlayOneShot(flapping);
        }
        if (distanceToPlayer < 20 && distanceToPlayer >= 14)
        {
            audioSource.volume = 0.25f;
            audioSource.PlayOneShot(flapping);
        }

        if (distanceToPlayer < 14)
        {
            audioSource.volume = .5f;
            audioSource.PlayOneShot(flapping);
        }
    }

}
