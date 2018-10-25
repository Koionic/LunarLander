using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneController : MonoBehaviour 
{
    [SerializeField] List<GameObject> inactiveZones;

    int numActiveZones;

    private void Start()
    {
        
    }

    public void AddZones (int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int random = Random.Range(0, inactiveZones.Count - 1);
            inactiveZones[random].SetActive(true);
            inactiveZones.RemoveAt(random);
            numActiveZones++;
        }
    }

    public void AddZonesToLimit(int limit)
    {
        for (int i = 0; numActiveZones < limit; i++)
        {
            int random = Random.Range(0, inactiveZones.Count - 1);
            inactiveZones[random].SetActive(true);
            inactiveZones.RemoveAt(random);
            numActiveZones++;
        }
    }

    public void DeactivateSpawn(GameObject zone)
    {
        inactiveZones.Add(zone);
        zone.SetActive(false);
        numActiveZones--;
    }

}
