using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestState : StrategyGameState
{
    public TestState(){}
    public override void FixedUpdateBehavior()
    {
        Debug.Log("FixedTest");
    }

    public override void UpdateBehavior()
    {
        Debug.Log("Test");
    }
}
