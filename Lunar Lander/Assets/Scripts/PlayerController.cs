using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour 
{
    //the points the players "spawn" in the lobby
    [SerializeField] Transform[] spawnPoints;
    //the models corresponding to each spawn point
    [SerializeField] GameObject[] landers;
    //the texts at each lobby slot
    [SerializeField] TextMeshProUGUI[] playerTexts;
    //the colours the text and models take (possibly)
    [SerializeField] Color[] playerColours;
    //the script components assigned to each lander logged in
    [SerializeField] ShipController[] lobbyRoster = new ShipController[4];

    //the script the controller is currently assigning a joystick to
    ShipController currentShip;

    SceneController sceneController;

    InputController inputController;

    [SerializeField] Button[] menuButtons;

    //whether or not the lobby is accepting players
    bool lobby = false;

    bool[] joystickLoggedIn = new bool[] {false, false, false, false};

    void Start ()
    {
        inputController = FindObjectOfType<InputController>();

        sceneController = FindObjectOfType<SceneController>();
	}
	
	void Update ()
    {
        if (lobby)
        GetPlayers();
	}

    void GetPlayers()
    {
        //for loop that cycles through each joystick number to check inputs
        for (int shipNum = 1; shipNum < 5; shipNum++)
        {
            
            if (inputController.SelectIsPressed(shipNum))
            {
                //if the player is already logged in, progresses the players to the next scene
                if (joystickLoggedIn[shipNum - 1] && lobby)
                {
                    FinishLobby();
                }
                else
                {
                    //for loop going through each lobby slot
                    for (int i = 0; i < 4; i++)
                    {
                        //checks if the slot is empty
                        if (!SlotIsTaken(i))
                        {
                            //changes the text to display the player number
                            playerTexts[i].text = "Player " + shipNum + " has joined";
                            //changes the text colour to the player colour
                            playerTexts[i].color = playerColours[shipNum];
                            //changes the bool to declare the joystick has been assigned
                            joystickLoggedIn[shipNum - 1] = true;
                            //creates a lander at the according lobby slot and assign its shipController to currentShip
                            currentShip = Instantiate(landers[i], spawnPoints[i].position, Quaternion.identity).GetComponent<ShipController>();
                            currentShip.SetPlayerID(shipNum);
                            //adds the shipController to the lobby
                            lobbyRoster[i] = currentShip;
                            break;
                        }
                    }
                }
            }

            if (inputController.CancelIsPressed(shipNum))
            {
                //logs out the player if they are logged in and exits
                if (joystickLoggedIn[shipNum - 1])
                {
                    //for loop that goes through the lobby slots
                    for (int i = 0; i < 4; i++)
                    {
                        //checks if the landers player id matches the joystick pressing cancel
                        if (SlotIsTaken(i) && lobbyRoster[i].GetPlayerID() == shipNum)
                        {
                            //changes the text back to normal
                            playerTexts[i].text = "Press A to join";
                            //changes colour text back to white
                            playerTexts[i].color = playerColours[0];
                            //switches the logged in bool back off
                            joystickLoggedIn[shipNum - 1] = false;
                            //destroys the lander
                            Destroy(lobbyRoster[i].gameObject);
                            //clears the lobby slot
                            lobbyRoster[i] = null;
                            break;
                        }
                    }
                }
                else
                {
                    CameraController cameraController = FindObjectOfType<CameraController>();
                    cameraController.LobbyToMain();
                    SetLobby(false);
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
        
        sceneController.GameScene();
        SetLobby(false);
    }

    public void SetLobby(bool boolean)
    {
        lobby = boolean;

        foreach (Button button in menuButtons)
        {
            button.interactable = !boolean;
        }
    }
}