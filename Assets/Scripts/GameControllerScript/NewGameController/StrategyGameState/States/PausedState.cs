using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedState : StrategyGameState
{
    private KillBoxController killBoxController;
    public PausedState(NewGameController gameController) : base(gameController){
        killBoxController = gameController.GetKillBoxController();
    }
    public override void FixedUpdateBehavior(){}

    public override void UpdateBehavior()
    {
        if(Input.GetKeyDown("escape")){
            GetGameController().UpdateState(new PlayState(GetGameController()));
        }
    }

    public override void OnStateBegin()
    {
    }

    public override void OnStateEnd()
    {
        // Nothing Here
    }
}
