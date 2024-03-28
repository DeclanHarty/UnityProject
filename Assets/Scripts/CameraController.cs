using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private float followDistance;
    private Vector2 position;
    private float travelToPosition;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float distanceMultipler;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void MoveCamera()
    {
        position = new Vector2(transform.position.x, transform.position.y);
        if(Mathf.Abs(player.GetPosition().y - position.y) > followDistance){
            travelToPosition = player.GetPosition().y;
        }

        float posDifference = travelToPosition - transform.position.y;
        if(Mathf.Abs(posDifference) > 0){
            float speedMultiplier = posDifference * distanceMultipler;

            transform.position += new Vector3(0, speedMultiplier * cameraSpeed * Time.deltaTime, 0);
        }

        transform.position = new Vector3(0, Mathf.Max(0,transform.position.y), -10);
    }
}
