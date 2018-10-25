using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{

    /*
        "To add more spawn points, simply duplicate a waypoint from the parent Spawner in the hierarchy and attach it to the inspector." 
        "The same can be done with items, but instead of duplicating from the hierarchy, just drag an item prefab from the prefabs foler." 
        "The float variable Seconds Before Spawning Items is the time before the spawner spawn items for the entirerity of the game." 
        "The float variable Spawn Rate In Seconds determins how often items spawn."
    */

    [SerializeField]
    GameObject[] spawnLocations;

    [SerializeField]
    GameObject[] itemsToSpawn;

    [SerializeField]
    float secondsBeforeSpawningItems;

    [SerializeField]
    float spawnRateInSeconds;

	void Start ()
    {
        PlayerInfo playerInfo = FindObjectOfType<PlayerInfo>();
        if (playerInfo != null && playerInfo.GetNumberOfPlayers() > 1)
            InvokeRepeating("SpawnStuff", secondsBeforeSpawningItems, spawnRateInSeconds);

    }

    void SpawnStuff()
    {

        GameObject spawn = spawnLocations[Random.Range(0, spawnLocations.Length)];

        GameObject item = itemsToSpawn[Random.Range(0, itemsToSpawn.Length)];

        if (spawn.GetComponentInChildren<PowerupAbility>() != null)
        {
            Destroy(spawn.GetComponentInChildren<PowerupAbility>().gameObject);
        }

        Instantiate(item, spawn.transform.position, transform.rotation * Quaternion.identity, spawn.transform);
        
    }

}
