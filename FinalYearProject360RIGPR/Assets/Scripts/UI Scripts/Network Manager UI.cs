using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using Unity.Netcode.Transports.UTP;
using TMPro;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] Button[] NetworkButtons;
    [SerializeField] TMP_InputField ipInput;
    [SerializeField] TMP_InputField portInput;

    [SerializeField] TextMeshProUGUI ipAdress;

    [SerializeField] private string myAddressLocal;

    //NetworkManager.Singleton.SceneManager.LoadScene();


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
            
            //NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>().ConnectionData.Port = portInput.text;

            //int _port;
            //int.TryParse(portInput.text,out _port);
            ////NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>().ConnectionData.Address = myAddressLocal;
            //NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>().SetConnectionData(myAddressLocal, (ushort)_port);
            //NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>().ConnectionData.ServerListenAddress = myAddressLocal;

            ipAdress.text = myAddressLocal;


            NetworkManager.Singleton.StartHost();
        });
        //NetworkButtons[0].onClick.AddListener(() => { SceneManager.LoadScene(1); });
        NetworkButtons[1].onClick.AddListener(() =>
        {

            //NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>();

            /*
            if(ipInput.text == "")
            {
                ipInput.text = "127.0.0.1";
            }
            */
            NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>().ConnectionData.Address = ipInput.text;
            //int _port;
            //int.TryParse(portInput.text,out _port);
            
            //NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>().SetConnectionData(myAddressLocal, (ushort)_port);

            //NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>().SetConnectionData(ipInput.ToString(), NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>().ConnectionData.Port);

            //NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>().SetConnectionData(NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>().ConnectionData.ServerEndPoint);

            //NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>().ConnectionData.ServerListenAddress = myAddressLocal;

            //ipAdress = null;

            NetworkManager.Singleton.StartClient();
        });
        //NetworkButtons[1].onClick.AddListener(() => { SceneManager.LoadScene(1); });

        //portInput.onValueChanged.AddListener();
    }
}
