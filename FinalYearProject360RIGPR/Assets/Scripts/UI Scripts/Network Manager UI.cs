using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] Button[] NetworkButtons;

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        NetworkButtons = GetComponentsInChildren<Button>();

        NetworkButtons[0].onClick.AddListener(() => { NetworkManager.Singleton.StartHost();});
        //NetworkButtons[0].onClick.AddListener(() => { SceneManager.LoadScene(1); });
        NetworkButtons[1].onClick.AddListener(() => { NetworkManager.Singleton.StartClient(); });
        //NetworkButtons[1].onClick.AddListener(() => { SceneManager.LoadScene(1); });
    }
}
