using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class NewGameController : MonoBehaviour
{
    // Game Objects
    [SerializeField] private PlayerController player;
    [SerializeField] private CameraController mainCamera;

    [SerializeField] private KillBoxController killBox;
    [SerializeField] private ScoreController scoreController;

    private StrategyGameState state;

    void Awake(){
        UpdateState(new PlayState(this));
    }

    public void UpdateState(StrategyGameState newState){
        state = newState;
    }

    public void Update(){
        state.UpdateBehavior();
    }

    public void FixedUpdate(){
        state.FixedUpdateBehavior();
    }

    public PlayerController GetPlayerController(){
        return player;
    }

    public CameraController GetCameraController(){
        return mainCamera;
    }

    public KillBoxController GetKillBoxController(){
        return killBox;
    }

    public ScoreController GetScoreController(){
        return scoreController;
    }
}
