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

    InputController inputController;

    UIController uiController;

    SceneController sceneController;

    [SerializeField] GameObject pauseMenu, endScreen, firstPauseButton, firstGameOverButton;

    public bool finished, paused;

	void Awake ()
	{
        audioManager = AudioManager.instance;

        playerInfo = FindObjectOfType<PlayerInfo>();

        sceneController = FindObjectOfType<SceneController>();

        shipControllers = FindObjectsOfType<ShipController>();

        zoneController = FindObjectOfType<ZoneController>();

        cameraController = FindObjectOfType<CameraController>();

        inputController = FindObjectOfType<InputController>();

        uiController = FindObjectOfType<UIController>();
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
		if (inputController.PauseIsPressed())
        {
            TogglePause();
        }
	}

    public void InvokeRespawn(ShipController newShip)
    {
        respawnQueue.Add(newShip);

        newShip.gameObject.SetActive(false);

        if (newShip.IsOutOfFuel())
        {
            cameraController.SetZoomedCamera(newShip.GetPlayerID());
            respawnQueue.Remove(newShip);
            if (AreAllPlayersOut())
            {
                FinishGame();
            }
        }
        else
        {
            Invoke("RespawnLanders", respawnRate);
        }
    }

    public void InvokeRespawn(ShipController newShip, float respawnTime)
    {
        respawnQueue.Add(newShip);

        newShip.gameObject.SetActive(false);

        if (newShip.IsOutOfFuel())
        {
            cameraController.SetZoomedCamera(newShip.GetPlayerID());
            respawnQueue.Remove(newShip);
            if (AreAllPlayersOut())
            {
                FinishGame();
            }
        }
        else
        {
            Invoke("RespawnLanders", respawnTime);
        }
    }

    void SortPlayers()
    {
        foreach (ShipController player in shipControllers)
        {
            if (player.gameObject.activeInHierarchy)
                players[player.GetJoystickID() - 1] = player.gameObject;
        }
    }

    void RespawnLanders()
    {
        if (respawnQueue[0] != null)
        {
            int respawnNum = respawnQueue[0].GetPlayerID() - 1;
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

    void FinishGame()
    {
        finished = true;

        cameraController.EndScreenCamera();

        endScreen.SetActive(true);

        endScreen.GetComponent<EndScreen>().ChangeTexts();

        uiController = FindObjectOfType<UIController>();

        uiController.Highlight(firstGameOverButton);
    }

    bool AreAllPlayersOut()
    {
        foreach(ShipController player in shipControllers)
        {
            if (player.isAssigned)
            {
                if (!player.IsOutOfFuel() || player.gameObject.activeInHierarchy)
                return false;
            }
        }
        return true;
    }

    public string[] GrabScores()
    {
        float[] scores = new float[4];

        float[] scoreboard = new float[4];

        string[] scoreBoardTexts = new string [4];

        for (int possibleScore = 0; possibleScore < 4; possibleScore++)
        {
            ShipController currentShip = players[possibleScore].GetComponent<ShipController>();

            scores[possibleScore] = currentShip.GetScore();

            for (int currentScore = 0; currentScore < 4; currentScore++)
            {
                if (currentShip.GetPlayerID() == 0)
                {
                    scoreBoardTexts[currentScore] = "";
                }
                else
                {
                    if (scores[possibleScore] > scoreboard[currentScore])
                    {
                        for (int i = 3; i > currentScore; i--)
                        {
                            scoreboard[i] = scoreboard[i - 1];
                            scoreBoardTexts[i] = scoreBoardTexts[i - 1];
                        }

                        scoreboard[currentScore] = scores[possibleScore];

                        scoreBoardTexts[currentScore] = "Player " + (possibleScore + 1) + ": " + scoreboard[currentScore];

                        break;
                    }
                }
            }

        }

        return scoreBoardTexts;

    }

    public void RestartGame()
    {
        sceneController.GameScene();
    }

    public void LeaveGame()
    {
        TogglePause();
        sceneController.MainMenu();
        endScreen.SetActive(false);
    }

    public void TogglePause()
    {
        if (paused)
        {
            paused = false;
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
        else if (!finished)
        {
            paused = true;
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            uiController.Highlight(firstPauseButton);
        }
    }
}
