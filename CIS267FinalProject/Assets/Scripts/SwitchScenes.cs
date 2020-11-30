using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Scene currentLevel = SceneManager.GetActiveScene();
            Debug.Log("Current Scene" + currentLevel.name);

            if (currentLevel.name == "Level 1")
            {
                Debug.Log("You cleared Level 1, Enter Level 2");
                SceneManager.LoadScene("Level 2");

            }
            else if (currentLevel.name == "Level 2")
            {
                Debug.Log("You cleared Level 2, Enter Level 3");
                SceneManager.LoadScene("Level 3");

            }
            else if (currentLevel.name == "Level 3")
            {
                Debug.Log("You cleared Level 3, Enter Boss");
                SceneManager.LoadScene("Boss Level");
            }
        }
    }

}
