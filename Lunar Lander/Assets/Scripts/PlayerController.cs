using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject[] landers;

    ShipController currentShip;

    InputController inputController;

    int currentPlayer = 1;

    bool[] joystickLoggedIn = new bool[] {false, false, false, false};

    private void Awake()
    {
        inputController = GetComponent<InputController>();
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
        GetPlayers();
	}

    void GetPlayers()
    {
        for (int i = 1; i < 5; i++)
        {
            if (inputController.SelectIsPressed(i))
            {
                Debug.Log("pressed");
                if (joystickLoggedIn[i - 1])
                {
                    Debug.Log("Player is already logged in");
                }
                else
                {
                    joystickLoggedIn[i - 1] = true;
                    currentShip = Instantiate(landers[currentPlayer - 1], spawnPoints[currentPlayer - 1].position, Quaternion.identity).GetComponent<ShipController>();
                    currentShip.SetPlayerID(i);
                    currentPlayer++;
                }
            }
        }
    }
}
