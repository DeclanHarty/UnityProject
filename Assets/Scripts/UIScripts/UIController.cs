using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private ScoreController scoreObj;
    [SerializeField] private PauseMenuController pauseMenu;
    [SerializeField] private GameOverController gameOverMenu;

    // Score Methods
    public void UpdateScore(double score){
        scoreObj.UpdateScore(score);
    }

    public void HideScore(){
        scoreObj.HideScore();
    }

    // Pause Menu Methods
    public void OpenPauseMenu(){
        pauseMenu.OpenMenu();
    }

    public void ClosePauseMenu(){
        pauseMenu.CloseMenu();
    }

    // GameOver Menu Methods
    public void OpenGameOverMenu(double finalScore){
        gameOverMenu.SetFinalScore(finalScore);
        gameOverMenu.OpenMenu();
    }

    public void CloseGameOverMenu(){
        gameOverMenu.CloseMenu();
    }




}
