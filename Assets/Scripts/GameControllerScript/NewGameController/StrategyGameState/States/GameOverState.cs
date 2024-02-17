using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverState : StrategyGameState
{
    public GameOverState(NewGameController gameController) : base(gameController){}
    public override void FixedUpdateBehavior()
    {

    }

    public override void OnStateBegin()
    {
        GetGameController().GetKillBoxController().Stop();
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
