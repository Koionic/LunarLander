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
<<<<<<< HEAD
    }
=======
        SortPlayers();
	}
>>>>>>> e86e5472823dcd335d0093fc5b0011656afe2246
	
	void Update ()
	{
		
	}

    public void InvokeRespawn(ShipController newShip)
    {
        respawnQueue.Add(newShip);

        Invoke("RespawnLanders", respawnRate);
    }

    void SortPlayers()
    {
        ShipController[] sortingList = FindObjectsOfType<ShipController>();
        foreach (ShipController player in sortingList)
        {
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


}
