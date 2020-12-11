using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCounter : MonoBehaviour
{
    static int killCounter = 0;

    private void Update()
    {
        Debug.Log("Kill Counter: " + killCounter);
    }

    public int getKillCounter()
    {
        return killCounter;
    }

    public void setKillCounter(int k)
    {
        killCounter = k;
    }
}
