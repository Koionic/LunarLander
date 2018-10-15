using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject[] landers;

    ShipController currentShip;

    int currentPlayer = 1;

    bool joystick1In, joystick2In, joystick3In, joystick4In;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        GetPlayers();
	}

    void GetPlayers()
    {
        if (Input.GetButtonDown("Submit1"))
        {
            if (joystick1In)
            {

            }
            else
            {
                joystick1In = true;
                currentShip = Instantiate(landers[currentPlayer - 1], spawnPoints[currentPlayer - 1].position, Quaternion.identity).GetComponent<ShipController>();
                currentShip.SetPlayerID(1);
                currentPlayer++;
            }
        }

        if (Input.GetButtonDown("Submit2"))
        {
            if (joystick2In)
            {

            }
            else
            {
                joystick2In = true;
                currentShip = Instantiate(landers[currentPlayer - 1], spawnPoints[currentPlayer - 1].position, Quaternion.identity).GetComponent<ShipController>();
                currentShip.SetPlayerID(2);
                currentPlayer++;
            }
        }

        if (Input.GetButtonDown("Submit3"))
        {
            if (joystick3In)
            {

            }
            else
            {
                joystick3In = true;
                currentShip = Instantiate(landers[currentPlayer - 1], spawnPoints[currentPlayer - 1].position, Quaternion.identity).GetComponent<ShipController>();
                currentShip.SetPlayerID(3);
                currentPlayer++;
            }
        }

        if (Input.GetButtonDown("Submit4"))
        {
            if (joystick4In)
            {

            }
            else
            {
                joystick4In = true;
                currentShip = Instantiate(landers[currentPlayer - 1], spawnPoints[currentPlayer - 1].position, Quaternion.identity).GetComponent<ShipController>();
                currentShip.SetPlayerID(4);
                currentPlayer++;
            }
        }
    }
}
