using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Game Objects
    [SerializeField] private PlayerController player;
    [SerializeField] private CameraController mainCamera;
    [SerializeField] private KillBoxController killBox;
    [SerializeField] private ScoreController scoreController;
    [SerializeField] private LevelBuilder levelBuilder;

    private GameState state = GameState.PLAYING;

    // Score Fields 
    private double score;
    [SerializeField] private int pointsPerSec;

    public static GameController instance;
    // Start is called before the first frame update
    public void Awake(){
        if(instance == null){
            instance = this;
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if(state == GameState.PLAYING){
            // Call all Basic Controller Methods
            killBox.StartMoving();
            player.CollectInput();
            
            mainCamera.MoveCamera();

            if(Input.GetKeyDown("escape")){
                state = GameState.PAUSED;
            }
            return;
        }

        if(state == GameState.PAUSED){
            if(Input.GetKeyDown("escape")){
                state = GameState.PLAYING;
            }

            return;
        }

        if(state == GameState.GAMEOVER){
            if(Input.GetKeyDown("space")){
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
        }
    }

    void FixedUpdate(){
        // Update Score and Change the UI to match
        if(state == GameState.PLAYING){
            score += Mathf.Floor(pointsPerSec * Time.deltaTime);
            scoreController.UpdateScore(score);

            player.HandleController();
        }
        
    }

    public void PlayerDies(){
        state = GameState.GAMEOVER;
    }
}
