using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using Unity.Netcode.Transports.UTP;
using TMPro;
using UnityEngine.SceneManagement;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] Button[] NetworkButtons;
    [SerializeField] TMP_InputField ipInput;
    [SerializeField] TMP_InputField portInput;

    [SerializeField] TextMeshProUGUI ipAdress;

    [SerializeField] private string myAddressLocal;

    //NetworkManager.Singleton.SceneManager.LoadScene
    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        NetworkButtons = GetComponentsInChildren<Button>();

        NetworkButtons[0].onClick.AddListener(() =>
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in hostEntry.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    myAddressLocal = ip.ToString();
                    break;
                }
            }
            NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>().ConnectionData.Address = myAddressLocal;

            ipAdress.text = myAddressLocal;


            NetworkManager.Singleton.StartHost();
            //NetworkManager.Singleton.SceneManager.LoadScene("MainMenuNetworking", LoadSceneMode.Single);
            //SceneManager.LoadScene("MainMenuNetworking", LoadSceneMode.Additive);
            //SceneManager.LoadSceneAsync("DesktopWaitingRoomNetworking", LoadSceneMode.Additive);

            //StartCoroutine(SceneSwitch(1, 0, NetworkManager.Singleton.LocalClient.PlayerObject.gameObject));

            //NetworkManager.Singleton.SceneManager.UnloadScene(SceneManager.GetSceneByBuildIndex(0));
            //NetworkManager.Singleton.NetworkConfig.EnableSceneManagement = true;
            //this may be very helpful

        });
        NetworkButtons[1].onClick.AddListener(() =>
        {
            /*
            if(ipInput.text == "")
            {
                ipInput.text = "127.0.0.1";
            }
            */
            NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>().ConnectionData.Address = ipInput.text;

            NetworkManager.Singleton.StartClient();

            SceneManager.LoadScene(2, LoadSceneMode.Single);

            //StartCoroutine(networkSceneManagement.SceneSwitch(2, 0, NetworkManager.Singleton.LocalClient.PlayerObject.gameObject));


            //NetworkManager.Singleton.SceneManager.LoadScene("DesktopWaitingRoomNetworking", LoadSceneMode.Additive);

            //NetworkManager.Singleton.SceneManager.ActiveSceneSynchronizationEnabled = false;
            //SceneManager.LoadSceneAsync("DesktopWaitingRoomNetworking", LoadSceneMode.Additive);
            
            //SceneManager.MoveGameObjectToScene(NetworkManager.Singleton.LocalClient.PlayerObject.gameObject, SceneManager.GetSceneByName("DesktopWaitingRoomNetworking"));
            //var instance = Instantiate(client);
            //var instanceNetworkObject = instance.GetComponent<NetworkObject>();
            //instanceNetworkObject.Spawn();
        });
    }

    NetworkSceneManagement networkSceneManagement = new NetworkSceneManagement();
}

