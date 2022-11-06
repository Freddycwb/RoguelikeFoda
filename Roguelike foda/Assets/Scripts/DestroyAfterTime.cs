using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    private float timePassed;
    public float timeToDestroy;

    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed >= timeToDestroy)
        {
            Destroy(gameObject);
        }
    }
}
