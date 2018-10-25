using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour 
{
    
    int numberOfPlayers;

    int[] joystickAssignment = new int[4];

    public Color[] playerColours;

	void Start () 
    {
		
	}
	
	void Update () 
    {
		
	}

    public void SetNumberOfPlayers(int num)
    {
        numberOfPlayers = num;
    }

    public int GetNumberOfPlayers()
    {
        return numberOfPlayers;
    }

    public void AssignJoystick(int pos, int id)
    {
        joystickAssignment[pos] = id;
    }

    public int GetJoystick(int pos)
    {
        return joystickAssignment[pos];
    }
}
