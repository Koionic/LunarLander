﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour 
{

    string throttleInputKey, submitKey, cancelKey, pauseKey;

    public bool debugMode;

    void Start()
    {
        if (!debugMode)
        {
            Destroy(this);
        }

        if (SystemInfo.operatingSystemFamily == OperatingSystemFamily.MacOSX)
        {
            throttleInputKey = "ThrottleMac";
            submitKey = "SubmitMac";
            cancelKey = "CancelMac";
            pauseKey = "PauseMac";
        }
        else
        {
            throttleInputKey = "Throttle";
            submitKey = "Submit";
            cancelKey = "Cancel";
            pauseKey = "Pause";
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
        return Input.GetButtonDown(submitKey + id);
    }

    public bool AnySelectIsPressed()
    {
        if (Input.GetButtonDown("Submit"))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Checks if the cancel button is pressed on the corresponding joystick.
    /// </summary>
    /// <returns><c>true</c>, if cancel was pressed, <c>false</c> otherwise.</returns>
    /// <param name="id">Identifier.</param>
    public bool CancelIsPressed(int id)
    {
        return Input.GetButtonDown(cancelKey + id);
    }

    public bool PauseIsPressed()
    {
        return Input.GetButtonDown(pauseKey);
    }
}
