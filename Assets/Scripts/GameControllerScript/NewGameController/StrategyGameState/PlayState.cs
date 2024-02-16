using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : StrategyGameState
{
    private NewGameController gameController;

    private PlayerController player;
    private KillBoxController killBox;
    private CameraController camera;
    private ScoreController score;

    public PlayState(NewGameController newGameController){
        this.gameController = newGameController;
        player = gameController.GetPlayerController();
        killBox = gameController.GetKillBoxController();
        camera = gameController.GetCameraController();
        score = gameController.GetScoreController();
    }
    public override void FixedUpdateBehavior()
    {
        if(score){
            score.UpdateScore(0);
        }
    }

    public override void UpdateBehavior()
    {
        if(player){
            killBox.Move();
            camera.MoveCamera();
        }
        

        if(Input.GetKeyDown("escape")){
            gameController.UpdateState(new TestState());
        }
    }
  
}
