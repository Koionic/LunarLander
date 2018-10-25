using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AveragePlayerPosition : MonoBehaviour
{
    [SerializeField] GameObject[] players;

    [SerializeField] Transform defaultCameraTransform;

    Vector3 lastPosition;

    bool onePlayer;

	void Start ()
    {
        PlayerInfo playerInfo = FindObjectOfType<PlayerInfo>();
        if (playerInfo != null)
        {
            if (playerInfo.GetNumberOfPlayers() == 1)
            {
                onePlayer = true;
            }
        }
	}
	
	void Update ()
    {
        List<Vector3> positions = new List<Vector3>();

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].activeInHierarchy)
                positions.Add(players[i].transform.position);
        }

        transform.position = GetAverageVector(positions);
    }

    private Vector3 GetAverageVector(List<Vector3> positions)
    {
        if (positions.Count == 0)
        {
            if (onePlayer)
            {
                return lastPosition;
            }
            return defaultCameraTransform.position;
        }

        Vector3 averageVector = Vector3.zero;

        foreach (Vector3 pos in positions)
        {
            averageVector += pos;
        }

        lastPosition = averageVector / positions.Count;

        return lastPosition;
    }
}
