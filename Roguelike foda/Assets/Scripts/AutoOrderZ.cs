using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoOrderZ : MonoBehaviour
{
    public int priority;

    void Start()
    {
        GetComponent<SpriteRenderer>().sortingOrder = Mathf.FloorToInt(transform.position.y * -10) + priority * 10;
    }
}
