using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverState : StrategyGameState
{
    private KillBoxController killBoxController;
    private PlayerController playerController;
    public GameOverState(NewGameController gameController) : base(gameController){
        killBoxController = gameController.GetKillBoxController();
        playerController = gameController.GetPlayerController();
    }
    public override void FixedUpdateBehavior()
    {

    }

    public override void OnStateBegin()
    {
        killBoxController.StopMoving();
        playerController.FreezePlayer();
    }

    public override void OnStateEnd()
    {
        // Nothing Here
    }

    public override void UpdateBehavior()
    {
        if(Input.GetKeyDown("space")){
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
