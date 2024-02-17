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

    // Score Fields 
    private double score;
    [SerializeField] private int pointsPerSec;

    private StrategyGameState state;

    public static NewGameController instance;

    void Start(){
        UpdateState(new PlayState(this));
        if(instance == null){
            instance = this;
        }
    }

    public void PlayerDies(){
        UpdateState(new GameOverState(this));
    }

    public void UpdateState(StrategyGameState newState){
        if(state != null) state.OnStateEnd();
        state = newState;
        state.OnStateBegin();
    }

    public void Update(){
        state.UpdateBehavior();
    }

    public void FixedUpdate(){
        state.FixedUpdateBehavior();
    }


    // Getter Methods
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

    public double GetScore(){
        return score;
    }

    public void UpdateScore(double score){
        this.score = score;
    }

    public int GetPointsPerSec(){
        return pointsPerSec;
    }
}
