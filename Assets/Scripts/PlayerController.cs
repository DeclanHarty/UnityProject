using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Movement movement;

    [SerializeField] private string state = "Moving";

    // Start is called before the first frame update
    void Start()
    {
        movement = gameObject.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(state == "Moving"){
            if(Input.GetKeyDown("escape")){
                state = "Paused";
            }
            movement.Move();
        }else {
           if(Input.GetKeyDown("escape")){
                state = "Moving";
            } 
        }   
    }
}
