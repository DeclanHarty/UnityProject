using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.PlasticSCM.Editor.WebApi;
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

    // Update is called once per frame
    void Update()
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
    }
}