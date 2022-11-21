using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : MonoBehaviour
{
    public GameObject flashbackFragment;

    private void OnDisable()
    {
        if(GetComponent<BattleEntity>().currentHealth <= 0)
        {
            Instantiate(flashbackFragment, transform.position, transform.rotation);
        }
    }
}
