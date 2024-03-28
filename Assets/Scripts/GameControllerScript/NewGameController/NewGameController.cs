using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class NewGameController : MonoBehaviour
{
    // Game Objects
    [SerializeField] private PlayerController player;
    [SerializeField] private CameraController mainCamera;

    [SerializeField] private KillBoxController killBox;
    [SerializeField] private UIController uiController;
    [SerializeField] private LevelBuilder levelBuilder;

    // Score Fields 
    private double score;
    [SerializeField] private int pointsPerSec;

    [SerializeField] private StrategyGameState state;

    // testing fields

    private float startTime;


    void Start(){
        UpdateState(new PlayState(this));
        killBox.SetGameController(this);
    }

    public void PlayerDies(){
        UpdateState(new GameOverState(this));
    }

    // Methods For Pause/Game Over MenuMenu
    public void GoToMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void Resume(){
        UpdateState(new PlayState(this));
    }

    public void UpdateState(StrategyGameState newState){
        state?.OnStateEnd();
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

    public UIController GetUIController(){
        return uiController;
    }

    public LevelBuilder GetLevelBuilder(){
        return levelBuilder;
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

    public StrategyGameState GetState(){
        return state;
    }
}
