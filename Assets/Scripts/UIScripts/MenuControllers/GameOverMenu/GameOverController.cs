using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverController : MenuController
{
    [SerializeField] private GameObject scoreObj;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetFinalScore(double finalScore){
        scoreObj.GetComponent<TextMeshProUGUI>().text = "Score : " + finalScore;
    }

    

}
