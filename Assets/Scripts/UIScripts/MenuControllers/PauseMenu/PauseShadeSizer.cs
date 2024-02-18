using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseShadeSizer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
    }

    
}
