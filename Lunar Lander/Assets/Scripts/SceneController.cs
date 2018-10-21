using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour 
{
    [SerializeField] string mainMenuSceneName, gameSceneName;

    LoadingScreen loadingScreenController;

    void Start () 
    {
		
	}
	
	void Update () 
    {
		
	}

    public void GameScene()
    {
        LoadScene(gameSceneName);
    }

    public void MainMenu()
    {
        LoadScene(mainMenuSceneName);
    }

    void LoadScene(string scene)
    {
        loadingScreenController = GetLoadingScreen();
        loadingScreenController.StartLoadingScreen(scene);
    }

    LoadingScreen GetLoadingScreen()
    {
        return FindObjectOfType<LoadingScreen>();
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}
