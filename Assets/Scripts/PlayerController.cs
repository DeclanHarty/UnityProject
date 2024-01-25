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
            movement.Move(Input.GetAxisRaw("Horizontal"));
            if(Input.GetKeyDown("space")) movement.Jump();
        }
    }

    public void TogglePause(){
        paused = !paused;
    }
}
