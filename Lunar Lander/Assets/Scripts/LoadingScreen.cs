using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScreen : MonoBehaviour 
{

    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI continueText;

    AsyncOperation async;

    InputController inputController;

    public void StartLoadingScreen(string scene)
    {
        Debug.Log("LOADING STARTED");
        inputController = FindObjectOfType<InputController>();
        StartCoroutine(Loading(scene));
    }

    IEnumerator Loading(string scene)
    {
        //enables the black screen and other ui
        loadingScreen.SetActive(true);
        //starts loading the next scene
        async = SceneManager.LoadSceneAsync(scene);
        Debug.Log("Loading scene");
        //prevents the scene from being activated before it's fully loaded
        async.allowSceneActivation = false;

        while (async.isDone == false)
        {
            //changes the bar to show loading progress
            slider.value = async.progress;
            continueText.text = "";
            Debug.Log("Iterating loading...");

            //async describes 0.9 as fully loaded
            if(async.progress == 0.9f)
            {
                Debug.Log("Scene is ready");
                slider.value = 1f;
                continueText.text = "Press X to continue";
                //changes the scene when anyone presses select
                if (inputController.AnySelectIsPressed())
                {
                    Debug.Log("Scene is loaded");
                    async.allowSceneActivation = true;
                    loadingScreen.SetActive(false);
                    yield break;
                }

            }
            yield return null;
        }
    }

}
