using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : MonoBehaviour
{
    public bool moveX;
    public float moveTime;
    public float amplitude = 0.5f;
    public float frequency = 1f;

    private Vector3 posOffset;
    private Vector3 tempPos;

    private void Start()
    {
        posOffset = transform.position;
        if (moveTime == 0)
        {
            Move(Time.fixedTime);
        }
    }

    public IEnumerator Play()
    {
        float currentMoveTime = 0;
        while (currentMoveTime <= moveTime)
        {
            Move(currentMoveTime);
            currentMoveTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        while (Vector3.Distance(transform.position, posOffset) > 0.1f)
        {
            Move(currentMoveTime);
            currentMoveTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = posOffset;
    }

    private void Move(float time)
    {
        tempPos = posOffset;
        tempPos += moveX ? new Vector3(Mathf.Sin(time * Mathf.PI * frequency) * amplitude, 0, 0) : new Vector3(0, Mathf.Sin(time * Mathf.PI * frequency) * amplitude, 0);
        transform.position = tempPos;
    }
}
