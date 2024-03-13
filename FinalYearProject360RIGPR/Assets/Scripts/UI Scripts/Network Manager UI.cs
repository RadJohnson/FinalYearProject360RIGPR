using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net;
using System.Net.Sockets;
using Unity.Netcode.Transports.UTP;
using TMPro;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] Button[] NetworkButtons;
    [SerializeField] TMP_InputField ipInput;

    [SerializeField] TextMeshProUGUI ipAdress;

    [SerializeField]private string myAddressLocal;

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

            ipAdress = Instantiate(ipAdress,gameObject.transform.parent);
            
            ipAdress.text = myAddressLocal;
        });
        //NetworkButtons[0].onClick.AddListener(() => { SceneManager.LoadScene(1); });
        NetworkButtons[1].onClick.AddListener(() => 
        {
            NetworkManager.Singleton.StartClient();
            
            NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>().ConnectionData.Address = ipInput.ToString();
        });
        //NetworkButtons[1].onClick.AddListener(() => { SceneManager.LoadScene(1); });
    }
}
