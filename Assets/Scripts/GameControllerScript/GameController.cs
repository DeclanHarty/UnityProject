using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private CameraController mainCamera;

    [SerializeField] private KillBoxController killBox;

    private GameState state = GameState.PLAYING;

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
            killBox.Move();
            player.HandleController();
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

    public void PlayerDies(){
        state = GameState.GAMEOVER;
    }
}
