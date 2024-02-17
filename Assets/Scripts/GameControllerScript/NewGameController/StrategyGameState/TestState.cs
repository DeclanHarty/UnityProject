using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestState : StrategyGameState
{
    public TestState(NewGameController gameController) : base(gameController){}
    public override void FixedUpdateBehavior()
    {
        Debug.Log("FixedTest");
    }

    public override void UpdateBehavior()
    {
        Debug.Log("Test");
    }

    public override void OnStateBegin()
    {
        Debug.Log("TestState Begins");
    }

    public override void OnStateEnd()
    {
        Debug.Log("TestState Ends");
    }

    
}
