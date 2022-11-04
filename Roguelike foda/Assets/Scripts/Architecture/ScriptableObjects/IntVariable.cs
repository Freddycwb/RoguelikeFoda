using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IntVariable : NumericVariable<int>
{
    public override void Add(int amount)
    {
        Value += amount;
    }

    public override void Add(Variable<int> amount)
    {
        Value += amount.Value;
    }
}
