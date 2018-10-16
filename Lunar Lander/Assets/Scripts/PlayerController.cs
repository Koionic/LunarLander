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

    bool finished;

    bool[] joystickLoggedIn = new bool[] {false, false, false, false};

    private void Awake()
    {
        inputController = FindObjectOfType<InputController>();
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
        if (!finished)
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
                    FinishLobby();
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (!SlotIsTaken(i))
                        {
                            string playerName = "Player " + shipNum + " has joined";
                            playerTexts[i].text = playerName;
                            joystickLoggedIn[shipNum - 1] = true;
                            currentShip = Instantiate(landers[i], spawnPoints[i].position, Quaternion.identity).GetComponent<ShipController>();
                            currentShip.SetPlayerID(shipNum);
                            lobbyRoster[i] = currentShip;
                            break;
                        }
                    }
                }
            }

            if (inputController.CancelIsPressed(shipNum))
            {
                if (joystickLoggedIn[shipNum - 1])
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (SlotIsTaken(i) && lobbyRoster[i].GetPlayerID() == shipNum)
                        {
                            playerTexts[i].text = "Press A to join";
                            joystickLoggedIn[shipNum - 1] = false;
                            Destroy(lobbyRoster[i].gameObject);
                            lobbyRoster[i] = null;
                            break;
                        }
                    }
                }
            }
        }
    }

    bool SlotIsTaken(int pos)
    {
        if (lobbyRoster[pos] != null)
        {
            return true;
        }

        return false;
    }

    void FinishLobby()
    {
        PlayerInfo playerInfo = FindObjectOfType<PlayerInfo>();

        int numOfPlayers = 0;

        for (int i = 0; i < 4; i++)
        {
            if (SlotIsTaken(i))
            {
                playerInfo.AssignJoystick(i, lobbyRoster[i].GetPlayerID());
                numOfPlayers++;
            }
            else
            {
                playerInfo.AssignJoystick(i, 0);
            }
        }

        playerInfo.SetNumberOfPlayers(numOfPlayers);

        finished = true;
        SceneController sceneController = FindObjectOfType<SceneController>();
        sceneController.GameScene();
    }


}
