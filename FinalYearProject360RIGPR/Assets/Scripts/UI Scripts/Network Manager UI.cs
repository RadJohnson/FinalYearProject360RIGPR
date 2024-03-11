using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] Button[] NetworkButtons;

    [SerializeField] Transform drawSurfaceToSpawn;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        NetworkButtons = GetComponentsInChildren<Button>();

        NetworkButtons[0].onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            //Transform spawnedObj = Instantiate(drawSurfaceToSpawn);
            //spawnedObj.GetComponent<NetworkObject>().Spawn(true);
            //Transform[] childList = new Transform[spawnedObj.childCount];
            //for (int i = 0; i < childList.Length; i++)
            //{
            //    childList[i] = spawnedObj.GetChild(i);
            //}
            //foreach (Transform child in childList)
            //{
            //    child.GetComponent<NetworkObject>().Spawn();
            //
            //}
        });
        //NetworkButtons[0].onClick.AddListener(() => { SceneManager.LoadScene(1); });
        NetworkButtons[1].onClick.AddListener(() => { NetworkManager.Singleton.StartClient(); });
        //NetworkButtons[1].onClick.AddListener(() => { SceneManager.LoadScene(1); });
    }
}
