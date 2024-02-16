using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StrategyGameState
{
    public abstract void UpdateBehavior();
    public abstract void FixedUpdateBehavior();
}
