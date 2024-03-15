using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using Unity.Netcode.Transports.UTP;
using TMPro;
using System.Text.RegularExpressions;
using Unity.Networking.Transport;


public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] Button[] NetworkButtons;
    [SerializeField] TMP_InputField ipInput;

    [SerializeField] TextMeshProUGUI ipAdress;

    [SerializeField] private string myAddressLocal;

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
            //NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>().ConnectionData.Address = myAddressLocal;

            NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>().ConnectionData.ServerListenAddress = myAddressLocal;
            

            //ipAdress = Instantiate(ipAdress, gameObject.transform.parent);

            //ipAdress.text = myAddressLocal;
        });
        //NetworkButtons[0].onClick.AddListener(() => { SceneManager.LoadScene(1); });
        NetworkButtons[1].onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();

            //NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>();

            //NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>().SetConnectionData(ipInput.ToString(), NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>().ConnectionData.Port);
            
            NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>().SetConnectionData(NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>().ConnectionData.ServerEndPoint);
        });
        //NetworkButtons[1].onClick.AddListener(() => { SceneManager.LoadScene(1); });
    }

    /// <summary>
    /// Sanitize user IP address InputField box allowing only numbers and '.'. This also prevents undesirable
    /// invisible characters from being copy-pasted accidentally.
    /// </summary>
    /// <param name="dirtyString"> string to sanitize. </param>
    /// <returns> Sanitized text string. </returns>
    public static string SanitizeIP(string dirtyString)
    {
        return Regex.Replace(dirtyString, "[^0-9.]", "");
    }

    /// <summary>
    /// Sanitize user port InputField box allowing only numbers. This also prevents undesirable invisible characters
    /// from being copy-pasted accidentally.
    /// </summary>
    /// <param name="dirtyString"> string to sanitize. </param>
    /// <returns> Sanitized text string. </returns>
    public static string SanitizePort(string dirtyString)
    {

        return Regex.Replace(dirtyString, "[^0-9]", "");
    }


    public static bool AreIpAddressAndPortValid(string ipAddress, string port)
    {
        var portValid = ushort.TryParse(port, out var portNum);
        return portValid && true/*NetworkEndpoint.TryParse(ipAddress, portNum, out var networkEndPoint)*/;
    }


}
