using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AveragePlayerPosition : MonoBehaviour
{
    [SerializeField] GameObject[] players;

	void Start ()
    {
		
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
            return Vector3.zero;
        }

        Vector3 averageVector = Vector3.zero;

        foreach (Vector3 pos in positions)
        {
            averageVector += pos;
        }

        return (averageVector / positions.Count);
    }
}
