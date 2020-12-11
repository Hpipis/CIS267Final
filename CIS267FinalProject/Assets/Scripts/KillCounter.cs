using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillCounter : MonoBehaviour
{
    static int killCounter = 0;
    private int sceneKills = 0;

    private void Start()
    {
        sceneKills = 0;
    }

    private void Update()
    {
        Debug.Log("Kill Counter: " + killCounter);
        Debug.Log("Scene Kill Counter: " + sceneKills);
        SceneManager.sceneUnloaded += MyFunction;
    }

    void MyFunction<Scene>(Scene scene)
    {
        killCounter += sceneKills;
    }

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
