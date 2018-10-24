﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
    [SerializeField] Transform[] spawnPoints;

    [SerializeField] float respawnRate;

    [SerializeField] GameObject[] players;

    ShipController[] shipControllers;

    List<ShipController> respawnQueue = new List<ShipController>(4);

    AudioManager audioManager;

    PlayerInfo playerInfo;

	void Awake ()
	{
        audioManager = AudioManager.instance;

        playerInfo = FindObjectOfType<PlayerInfo>();

        shipControllers = FindObjectsOfType<ShipController>();
	}

	void Start () 
	{
        SortPlayers();

        if(playerInfo.GetNumberOfPlayers() > 1)
        {
            //play singleplayer song
        }
        else
        {
            audioManager.PlaySound("MainTheme");
        }

	}
	
	void Update ()
	{
		
	}

    public void InvokeRespawn(ShipController newShip)
    {
        respawnQueue.Add(newShip);

        if (AreAllPlayersOut())
        {

        }
        else
        {
            Invoke("RespawnLanders", respawnRate);
        }
    }

    void SortPlayers()
    {
        foreach (ShipController player in shipControllers)
        {
            if (player.gameObject.activeInHierarchy)
                players[player.GetPlayerID() - 1] = player.gameObject;
        }
    }

    void RespawnLanders()
    {
        if (respawnQueue[0] != null)
        {
            int respawnNum = respawnQueue[0].GetPlayerID() - 1;
            GameObject spawningPlayer = players[respawnNum];
            spawningPlayer.SetActive(true);
            spawningPlayer.transform.position = spawnPoints[respawnNum].position;
            spawningPlayer.transform.rotation = Quaternion.identity;
            spawningPlayer.GetComponent<Rigidbody>().Sleep();
            respawnQueue.RemoveAt(0);
        }
    }

    bool AreAllPlayersOut()
    {
        foreach(ShipController player in shipControllers)
        {
            if (!player.IsOutOfFuel())
                return false;
        }
        return true;
    }
}
