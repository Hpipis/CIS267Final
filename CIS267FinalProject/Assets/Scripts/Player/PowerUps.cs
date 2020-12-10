using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip powerUp;
    public static bool collectedDash = false;
    public static bool collectedDoubleJump = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DoubleJumpPowerUp") && this.gameObject.CompareTag("Player"))
        {
            Debug.Log("You collected double jump");
            audioSource.PlayOneShot(powerUp);
            collectedDoubleJump = true;
            
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("DashPowerUp") && this.gameObject.CompareTag("Player"))
        {
            Debug.Log("You collected dash");
            audioSource.PlayOneShot(powerUp);
            collectedDash = true;
            Destroy(collision.gameObject);
        }
    }

    public bool getDashCollectionStatus()
    {
        return collectedDash;
    }

    public bool getDoubleJumpCollectionStatus()
    {
        return collectedDoubleJump;
    }
}
