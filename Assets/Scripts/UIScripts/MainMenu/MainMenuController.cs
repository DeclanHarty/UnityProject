using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private string levelSceneName;
    [SerializeField] private TitleScreenController titleScreenController;
    [SerializeField] private OptionsScreenController optionsScreenController;

    // Title Screen Methods
    public void PlayButtonHit(){
        Debug.Log("Play");
        SceneManager.LoadScene(levelSceneName);
    }

    public void Quit(){
        Debug.Log("Quit");
        Application.Quit();
    }

    // Options Screen Methods
    public void OpenOptions(){
        titleScreenController.CloseMenu();
        optionsScreenController.OpenMenu();
    }
    public void Back(){
        optionsScreenController.CloseMenu();
        titleScreenController.OpenMenu();
    }
}
