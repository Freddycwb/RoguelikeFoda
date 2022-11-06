using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashbackFragment : MonoBehaviour
{
    public GameEvent StartFlashback;
    public GameObjectVariable Player;

    private void Start()
    {
        StartFlashback.Raise();
        StartCoroutine("StartMovement");
    }

    public IEnumerator StartMovement()
    {
        yield return Movement(new Vector2(transform.position.x, transform.position.y + 3), 4);
        yield return new WaitForSeconds(0.33f);
        yield return Movement(Player.Value.transform.position, 6);
        Destroy(gameObject);
    }

    public IEnumerator Movement(Vector3 target, float speed)
    {
        while (Vector3.Distance(transform.position, target) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
            yield return new WaitForEndOfFrame();
        }
        transform.position = target;
    }
}
