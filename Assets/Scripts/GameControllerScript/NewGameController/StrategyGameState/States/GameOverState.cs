using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverState : StrategyGameState
{
    private KillBoxController killBoxController;
    private PlayerController playerController;
    private UIController uiController;
    public GameOverState(NewGameController gameController) : base(gameController){
        killBoxController = gameController.GetKillBoxController();
        playerController = gameController.GetPlayerController();
        uiController = gameController.GetUIController();

    }
    public override void FixedUpdateBehavior()
    {

    }

    public override void OnStateBegin()
    {
        killBoxController.StopMoving();
        playerController.FreezePlayer();
        uiController.OpenGameOverMenu(gameController.GetScore());
        uiController.HideScore();
    }

    public override void OnStateEnd()
    {
        uiController.CloseGameOverMenu();
    }

    public override void UpdateBehavior()
    {
        if(Input.GetKeyDown("space")){
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
