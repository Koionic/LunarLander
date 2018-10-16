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
        loadingScreenController = GetLoadingScreen();
        loadingScreenController.StartLoadingScreen(gameSceneName);
    }

    public void MainMenu()
    {
        loadingScreenController = GetLoadingScreen();
        loadingScreenController.StartLoadingScreen(mainMenuSceneName);
    }

    LoadingScreen GetLoadingScreen()
    {
        return FindObjectOfType<LoadingScreen>();
    }
}
