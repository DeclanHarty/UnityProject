using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Movement movement;

    [SerializeField] private bool paused;

    // Start is called before the first frame update
    void Start()
    {
        movement = gameObject.GetComponent<Movement>();
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("escape")) TogglePause();
        
        if(!paused){
            movement.Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
            if(Input.GetKeyDown("space")) movement.HandleSpaceInput();
        }
    }

    public void TogglePause(){
        paused = !paused;
    }

    public Vector2 GetPosition(){
        return movement.GetPosition();
    }

    public float GetVerticalVelocity(){
        return movement.GetVerticalVelocity();
    }
}
