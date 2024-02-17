using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedState : StrategyGameState
{
    private KillBoxController killBoxController;
    private PlayerController playerController;
    public PausedState(NewGameController gameController) : base(gameController){
        killBoxController = gameController.GetKillBoxController();
        playerController = gameController.GetPlayerController();
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
        playerController.FreezePlayer();
    }

    public override void OnStateEnd()
    {
        // Nothing Here
    }
}
