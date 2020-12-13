using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hazard : MonoBehaviour
{
    private bool enabled = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(enabled)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Hazard Trigger");
                Scene cur = SceneManager.GetActiveScene();
                SceneManager.LoadScene(cur.name);
            }
        }
    }

    public void setEnabled(bool e)
    {
        enabled = e;
    }
}
