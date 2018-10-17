using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] Button[] buttons;

    Button selectedButton;

    enum NavDirection { Horizontal, Vertical };

    [SerializeField] NavDirection navDirection;

    [SerializeField] GameObject parentCanvasObj;

    InputController inputController;



	void Start () 
    {
        inputController = FindObjectOfType<InputController>();

        selectedButton = buttons[1];
	}
	
	void Update () 
    {
		if (parentCanvasObj.activeInHierarchy == true)
        {
            
        }
	}
}
