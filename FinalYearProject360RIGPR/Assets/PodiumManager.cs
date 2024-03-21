using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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
        foreach (GameObject player in allPlayers)
        {
            if (!playerList.Contains(player))
            {
                playerList.Add(player);
                // Here, you can add logic to position the player at a podium.
                Debug.Log($"Added {player.name} to the player list.");
            }
        }

        // Optional: Remove any players from the list that are no longer in the game
        for (int i = playerList.Count - 1; i >= 0; i--)
        {
            if (playerList[i] == null || !playerList[i].activeInHierarchy)
            {
                Debug.Log($"Removed {playerList[i].name} from the player list.");
                playerList.RemoveAt(i);
            }
        }

        for (int i = 0; i < spawnLocations.Length; i++)
        {
            playerList[i].transform.position = spawnLocations[i].transform.position;
        }

    }


    private void Reset()
    {
        spawnLocations = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }
}
