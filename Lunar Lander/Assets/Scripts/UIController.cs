using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    [Header("Place Selectables Here")]
    //Add a new array for any new menu screens. You can place any type of ui element in here
    public Selectable[] mainSelectables, optionsSelectables;

    [SerializeField] GameObject firstOptionsButton;

    //Add a new bool for any new menu screens
    bool main = true, options = false;

    [SerializeField] Animator camera;

    EventSystem eventSystem;

    AudioManager audioManager;

    void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        audioManager = AudioManager.instance;
    }

    private void Start()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }

    void UpdateUI() 
    {
        //add a new loop for any new menu screen
        foreach (Selectable selectable in mainSelectables)
        {
            selectable.interactable = main;
        }

        foreach (Selectable selectable in optionsSelectables)
        {
            selectable.interactable = options;
        }
	}

    public void Options()
    {
        camera.Play("Main-Options");
        main = false;
        options = true;
        UpdateUI();
        audioManager.PlaySound("SelectButton");
        eventSystem.SetSelectedGameObject(firstOptionsButton);
    }

    public void Main()
    {
        if(!main)
        {
            if (options)
            {
                camera.Play("Options-Main");
                audioManager.PlaySound("SelectButton");
            }
            else
            {
                camera.Play("Lobby-Main");
                audioManager.PlaySound("Back");
            }
        }
        main = true;
        options = false;
        UpdateUI();
        eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
    }

    public void Lobby()
    {
        camera.Play("Main-Lobby");
        main = false;
        options = false;
        UpdateUI();
        audioManager.PlaySound("SelectButton");
    }

    public void Highlight(GameObject firstSelected)
    {
        eventSystem.SetSelectedGameObject(firstSelected);
        audioManager.PlaySound("ChangeSelect");
    }

    public void PlayHighlightSound()
    {
        audioManager.PlaySound("ChangeSelect");
    }
}
