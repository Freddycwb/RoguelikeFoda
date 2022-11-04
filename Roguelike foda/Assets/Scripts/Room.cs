using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private Vector2[] enemiesPositions =
        {
            new Vector2(0, 0),
            new Vector2(-1.6f, 0.8f),
            new Vector2(1.6f, -0.8f),
            new Vector2(-3.2f, 1.6f),
            new Vector2(3.2f, -1.6f),

            new Vector2(1.6f, 0.8f),
            new Vector2(0, 1.6f),
            new Vector2(3.2f, 0),
            new Vector2(-1.6f, 2.4f),
            new Vector2(4.8f, -0.8f),

            new Vector2(3.2f, 1.6f),
            new Vector2(1.6f, 2.4f),
            new Vector2(4.8f, 0.8f),
            new Vector2(0, 3.2f),
            new Vector2(6.4f, 0),
        };

    public List<BattleEntity> enemiesBattleEntitys = new List<BattleEntity>();
    public List<GameObject> enemiesGameObject = new List<GameObject>();
    public GameObject[] enemiesOrder;
    public bool cameFromRight;

    public void CreateEnemies()
    {
        Vector2[] positions =
        {
            enemiesPositions[Random.Range(0,3)],
            enemiesPositions[Random.Range(3,8)],
            enemiesPositions[Random.Range(8,11)],
            enemiesPositions[Random.Range(11,15)]
        };
        for (int i = 0; i < enemiesOrder.Length && i < 4; i++)
        {
            var a = Instantiate(enemiesOrder[i], positions[i], transform.rotation);
            enemiesBattleEntitys.Add(a.GetComponent<BattleEntity>());
            enemiesGameObject.Add(a.gameObject);
            a.transform.SetParent(transform);
        }
    }
}
