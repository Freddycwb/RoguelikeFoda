using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float frequency = 1f;

    private float count;
    private Vector3 posOffset;
    private Vector3 tempPos;
    private Vector3 endPos;
    private TextMeshPro value;

    private void Start()
    {
        posOffset = transform.position;
        endPos = new Vector3(Random.Range(posOffset.x + 1.5f, posOffset.x + 2.5f), Random.Range(posOffset.y + 0.7f, posOffset.y + 1.8f), 0);
        value = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        count += Time.deltaTime * frequency;
        tempPos = new Vector3(Vector3.Lerp(posOffset, endPos, count).x, Vector3.Lerp(posOffset, endPos, count).y, Vector3.Lerp(posOffset, endPos, count).z);
        tempPos += new Vector3(0, Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude,0);
        transform.position = tempPos;
        value.color = new Color(value.color.r, value.color.g, value.color.b, 1 - count);
        if (count >= 1)
        {
            Destroy(gameObject);
        }
    }
}
