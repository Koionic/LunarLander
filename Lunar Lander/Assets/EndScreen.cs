using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour 
{
    [SerializeField] TextMeshProUGUI[] playerScoreText;

    PlayerInfo playerInfo;

    GameController gameController;

    private void Awake()
    {
        playerInfo = FindObjectOfType<PlayerInfo>();

        gameController = FindObjectOfType<GameController>();

    }

    public void ChangeTexts()
    {
        string[] scoreboard = gameController.GrabScores();

        for (int i = 0; i < 4; i++)
        {
            playerScoreText[i].text = scoreboard[i];
            playerScoreText[i].color = playerInfo.playerColours[i + 1];
        }
    }
}
