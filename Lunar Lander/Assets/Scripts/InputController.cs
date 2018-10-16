using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour 
{

    string throttleInputKey;

    bool mac;

    void Start()
    {
        if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX)
        {
            throttleInputKey = "ThrottleMac";
            mac = true;
        }
        else
        {
            throttleInputKey = "Throttle";
            mac = false;
        }
    }

    void Update () 
    {
        
	}

    /// <summary>
    /// Gets the horizontal input of the corresponding joystick.
    /// </summary>
    /// <returns>The horizontal input as a float.</returns>
    /// <param name="id">Player ID.</param>
    public float GetHorizontalInput(int id)
    {
        return Input.GetAxis("Horizontal" + id);
    }

    /// <summary>
    /// Gets the throttle input of the corresponding joystick.
    /// </summary>
    /// <returns>The throttle input as a float.</returns>
    /// <param name="id">Player ID.</param>
    public float GetThrottleInput(int id)
    {
        return Input.GetAxis(throttleInputKey + id);
    }

    /// <summary>
    /// Checks if the select button is pressed on the corresponding joystick.
    /// </summary>
    /// <returns><c>true</c>, if select was pressed, <c>false</c> otherwise.</returns>
    /// <param name="id">Identifier.</param>
    public bool SelectIsPressed(int id)
    {
        switch (id)
        {
            case 1:
                {
                    return Input.GetKey(mac ? KeyCode.Joystick1Button16 : KeyCode.Joystick1Button0);
                }
            case 2:
                {
                    return Input.GetKey(mac ? KeyCode.Joystick2Button16 : KeyCode.Joystick2Button0);
                }
            case 3:
                {
                    return Input.GetKey(mac ? KeyCode.Joystick3Button16 : KeyCode.Joystick3Button0);
                }
            case 4:
                {
                    return Input.GetKey(mac ? KeyCode.Joystick4Button16 : KeyCode.Joystick4Button0);
                }
            default:
                {
                    return Input.GetKey(mac ? KeyCode.JoystickButton16 : KeyCode.JoystickButton0);
                }
        }
    }

    /// <summary>
    /// Checks if the cancel button is pressed on the corresponding joystick.
    /// </summary>
    /// <returns><c>true</c>, if cancel was pressed, <c>false</c> otherwise.</returns>
    /// <param name="id">Identifier.</param>
    public bool CancelIsPressed(int id)
    {
        switch (id)
        {
            case 1:
                {
                    return Input.GetKeyDown(mac ? KeyCode.Joystick1Button17 : KeyCode.Joystick1Button1);
                }
            case 2:
                {
                    return Input.GetKeyDown(mac ? KeyCode.Joystick2Button17 : KeyCode.Joystick2Button1);
                }
            case 3:
                {
                    return Input.GetKeyDown(mac ? KeyCode.Joystick3Button17 : KeyCode.Joystick3Button1);
                }
            case 4:
                {
                    return Input.GetKeyDown(mac ? KeyCode.Joystick4Button17 : KeyCode.Joystick4Button1);
                }
            default:
                {
                    return Input.GetKeyDown(mac ? KeyCode.JoystickButton17 : KeyCode.JoystickButton1);
                }
        }
    }
}
