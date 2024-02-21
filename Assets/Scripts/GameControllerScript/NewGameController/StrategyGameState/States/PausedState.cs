using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedState : StrategyGameState
{
    private KillBoxController killBoxController;
    private PlayerController playerController;
    private UIController uiController;
    public PausedState(NewGameController gameController) : base(gameController){
        killBoxController = gameController.GetKillBoxController();
        playerController = gameController.GetPlayerController();
        uiController = gameController.GetUIController();
        
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
        uiController.OpenPauseMenu();
    }

    public override void OnStateEnd()
    {
        uiController.ClosePauseMenu();
    }
}
