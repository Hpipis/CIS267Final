using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aipath;

    void Update()
    {
        if(aipath.desiredVelocity.x >= .01f)
        {
            transform.localScale = new Vector3(3f, 3f, 1f);
        }
        else if (aipath.desiredVelocity.x <= -.01f)
        {
            transform.localScale = new Vector3(-3f, 3f, 1f);
        }
    }
}
