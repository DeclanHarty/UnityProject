using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayState : StrategyGameState
{
    

    private PlayerController player;
    private KillBoxController killBox;
    private CameraController camera;
    private ScoreController scoreObject;

    public PlayState(NewGameController newGameController) : base(newGameController){
        NewGameController gameController = GetGameController();

        player = gameController.GetPlayerController();
        killBox = gameController.GetKillBoxController();
        camera = gameController.GetCameraController();
        scoreObject = gameController.GetScoreController();
    }
    public override void FixedUpdateBehavior()
    {
        if(scoreObject){
            NewGameController gameController = GetGameController();
            player.HandleController();
            gameController.UpdateScore(gameController.GetScore() + Mathf.Floor(gameController.GetPointsPerSec() * Time.deltaTime));
            scoreObject.UpdateScore(gameController.GetScore());
        }
    }

    public override void OnStateBegin()
    {
        killBox.StartMoving();
    }

    public override void OnStateEnd()
    {
        killBox.StopMoving();
    }

    public override void UpdateBehavior()
    {
        if(player){
            player.CollectInput();
            camera.MoveCamera();
        }
        

        if(Input.GetKeyDown("escape")){
            GetGameController().UpdateState(new PausedState(GetGameController()));
        }
    }
  
}
