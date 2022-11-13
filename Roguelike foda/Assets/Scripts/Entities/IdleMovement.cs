using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleMovement : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float frequency = 1f;
    public bool stopped;

    private Vector3 posOffset;
    private Vector3 tempPos;
    private int startAnimRandom;

    private void Start()
    {
        posOffset = transform.localScale;
        startAnimRandom = Random.Range(0, 5);
    }

    private void Update()
    {
        if (!stopped)
        {
            tempPos = posOffset;
            tempPos += new Vector3(0, Mathf.Sin((Time.fixedTime - (startAnimRandom * 0.2f)) * Mathf.PI * frequency) * amplitude, 0);
            transform.localScale = tempPos;
        }
    }
}
