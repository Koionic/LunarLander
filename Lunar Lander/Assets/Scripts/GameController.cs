using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour 
{
    [SerializeField] Transform[] spawnPoints;

    [SerializeField] float respawnRate;

    [SerializeField] GameObject[] players;

    [SerializeField] int startingZoneAmount;

    [SerializeField] float startVelocity;

    ZoneController zoneController;

    ShipController[] shipControllers;

    List<ShipController> respawnQueue = new List<ShipController>(4);

    AudioManager audioManager;

    PlayerInfo playerInfo;

    CameraController cameraController;

	void Awake ()
	{
        audioManager = AudioManager.instance;

        playerInfo = FindObjectOfType<PlayerInfo>();

        shipControllers = FindObjectsOfType<ShipController>();

        zoneController = FindObjectOfType<ZoneController>();

        cameraController = FindObjectOfType<CameraController>();
	}

	void Start () 
	{
        SortPlayers();

        zoneController.AddZonesToLimit(startingZoneAmount);

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

        if (newShip.IsOutOfFuel())
        {
            cameraController.SetZoomedCamera(newShip.GetPlayerID());
            respawnQueue.Remove(newShip);
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
            if (player.gameObject.activeInHierarchy )
                players[player.GetJoystickID() - 1] = player.gameObject;
        }
    }

    void RespawnLanders()
    {
        if (respawnQueue[0] != null)
        {
            int respawnNum = respawnQueue[0].GetJoystickID() - 1;
            GameObject spawningPlayer = players[respawnNum];
            spawningPlayer.SetActive(true);
            spawningPlayer.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length - 1)].position;
            spawningPlayer.transform.rotation = Quaternion.Euler(0,0,90);
            Rigidbody spawningPlayerRB = spawningPlayer.GetComponent<Rigidbody>();
            spawningPlayerRB.Sleep();
            spawningPlayerRB.AddForce(Vector3.right * startVelocity, ForceMode.Impulse);
            respawnQueue[0].DeGround();
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
