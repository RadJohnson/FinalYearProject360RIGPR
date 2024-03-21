using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodiumManager : MonoBehaviour
{
    [SerializeField] GameObject[] spawnLocations;
    private List<GameObject> playerList = new List<GameObject>();

    private void Start()
    {
        Reset();
    }

    private void Update()
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        if (allPlayers != null)
            foreach (GameObject player in allPlayers)
            {
                player.transform.position = spawnLocations[0].transform.position;
            }
    }


    private void Reset()
    {
        spawnLocations = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }
}
