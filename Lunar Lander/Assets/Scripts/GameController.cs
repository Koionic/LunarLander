using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
    [SerializeField] Transform[] spawnPoints;

    [SerializeField] float respawnRate;

    [SerializeField] GameObject[] players;

    List<ShipController> respawnQueue = new List<ShipController>(4);

	void Awake ()
	{
		
	}

	void Start () 
	{
        
	}
	
	void Update ()
	{
		
	}

    public void InvokeRespawn(ShipController newShip)
    {
        respawnQueue.Add(newShip);

        Invoke("RespawnLanders", respawnRate);
    }

    void RespawnLanders()
    {
        if (respawnQueue[0] != null)
        {
            int respawnNum = respawnQueue[0].GetPlayerID() - 1;
            players[respawnNum].SetActive(true);
            players[respawnNum].transform.position = spawnPoints[respawnNum].position;
            respawnQueue.RemoveAt(0);
        }
    }
}
