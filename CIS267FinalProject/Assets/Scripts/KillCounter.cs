using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillCounter : MonoBehaviour
{
    static int killCounter;
    private int sceneKills = 0;
    static bool addKills = false;

    private void Start()
    {
        sceneKills = 0;
        addKills = false;
    }

    private void Update()
    {
        Debug.Log("Kill Counter: " + killCounter);
        Debug.Log("Scene Kill Counter: " + sceneKills);
        //SceneManager.sceneUnloaded += MyFunction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SwitchScene"))
        {
            if (addKills == false)
            {
                killCounter += sceneKills;
                addKills = true;
            }
        }
    }

    //void MyFunction<Scene>(Scene scene)
    //{


    //}

    public int getKillCounter()
    {
        return killCounter;
    }

    public void setKillCounter(int k)
    {
        killCounter = k;
    }

    public int getSceneKills()
    {
        return sceneKills;
    }

    public void setSceneKills(int k)
    {
        sceneKills = k;
    }
}
