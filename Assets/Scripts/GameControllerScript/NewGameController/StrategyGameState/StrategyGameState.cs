using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StrategyGameState
{
    private NewGameController gameController;
    public StrategyGameState(NewGameController gameController){
        this.gameController = gameController;
    }

    public NewGameController GetGameController(){
        return gameController;
    }

    public abstract void UpdateBehavior();
    public abstract void FixedUpdateBehavior();

    public abstract void OnStateBegin();

    public abstract void OnStateEnd();
}
