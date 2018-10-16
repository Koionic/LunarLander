using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour 
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject[] landers;
    [SerializeField] TextMeshProUGUI[] playerTexts;
    [SerializeField] ShipController[] lobbyRoster = new ShipController[4];

    ShipController currentShip;

    InputController inputController;

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
        for (int shipNum = 1; shipNum < 5; shipNum++)
        {
            if (inputController.SelectIsPressed(shipNum))
            {
                Debug.Log("pressed");
                if (joystickLoggedIn[shipNum - 1])
                {
                    Debug.Log("Player is already logged in");
                }
                else
                {
                    string playerName = "Player " + shipNum + " has joined";
                    playerTexts[shipNum - 1].text = playerName;
                    joystickLoggedIn[shipNum - 1] = true;
                    currentShip = Instantiate(landers[shipNum - 1], spawnPoints[shipNum - 1].position, Quaternion.identity).GetComponent<ShipController>();
                    currentShip.SetPlayerID(shipNum);
                    lobbyRoster[shipNum - 1] = currentShip;
                }
            }

            if (inputController.CancelIsPressed(shipNum))
            {
                if (joystickLoggedIn[shipNum - 1])
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (lobbyRoster[i] != null && lobbyRoster[i].GetPlayerID() == shipNum)
                        {
                            playerTexts[i].text = "Press A to join";
                            joystickLoggedIn[i] = false;
                            Destroy(lobbyRoster[i].gameObject);
                            lobbyRoster[i] = null;
                            break;
                        }
                    }
                }

            }
        }
    }
}
