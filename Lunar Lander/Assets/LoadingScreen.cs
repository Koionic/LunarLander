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
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        inputController = FindObjectOfType<InputController>();
        StartCoroutine(Loading(scene));
    }

    IEnumerator Loading(string scene)
    {
        loadingScreen.SetActive(true);
        async = SceneManager.LoadSceneAsync(scene);
        async.allowSceneActivation = false;

        while (async.isDone == false)
        {
            slider.value = async.progress;
            continueText.text = "";

            if(async.progress == 0.9f)
            {
                slider.value = 1f;
                continueText.text = "Press X to continue";
                if (inputController.AnySelectIsPressed())
                {
                    Debug.Log("Scene is loaded");
                    async.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        loadingScreen.SetActive(false);
    }

}
