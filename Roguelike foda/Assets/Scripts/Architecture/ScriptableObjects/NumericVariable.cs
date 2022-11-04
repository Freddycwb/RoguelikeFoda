using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NumericVariable<T> : Variable<T>
{
    public abstract void Add(T amount);

    public abstract void Add(Variable<T> amount);
}
