using System.Linq;
using System.Net.Sockets;
using System.Net;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NDraw : NetworkBehaviour
{
    [SerializeField] Camera rayOrigin;

    [SerializeField] int penSize;//can likely be changed later to a Vector2 just an int now to draw to pixels in a square shaped brush

    [SerializeField] Color[] colors = new Color[1];

    [SerializeField] private NDrawSurface drawSurface;// make more efficient as lots of get component calls
    private bool _touchedLastFrame;
    private NDrawSurface lastUsedDrawSurface;

    [Space(5), Header("HostPrefabUI")]
    [SerializeField] private GameObject MainMenuScenePrefab;
    [SerializeField] private GameObject VideoUIPrefab;

    private bool prefabSpawned = false;
    private string myAddressLocal;

    private void Start()
    {
        Reset();
    }

    private void Update()
    {
        DrawPixels();

        if (IsHost)
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
            {
                var existingObject = GameObject.FindWithTag("HostUI");

                if (existingObject == null)
                {
                    Instantiate(MainMenuScenePrefab);

                }
                GameObject IpContainer = GameObject.FindWithTag("IP");

                IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in hostEntry.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        myAddressLocal = ip.ToString();
                        break;
                    }
                }
                IpContainer.GetComponent<TMP_Text>().text = myAddressLocal;
            }
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(2))
            {
                var existingObject = GameObject.FindWithTag("HostUI");
                if (existingObject == null)
                {
                    GameObject UIInstance = Instantiate(VideoUIPrefab);
                    UIInstance.GetComponent<VideoPlayingUIManager>().drawScript = this;

                    GameObject IpContainer = GameObject.FindWithTag("IP");

                    IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
                    foreach (IPAddress ip in hostEntry.AddressList)
                    {
                        if (ip.AddressFamily == AddressFamily.InterNetwork)
                        {
                            myAddressLocal = ip.ToString();
                            break;
                        }
                    }
                    IpContainer.GetComponent<TMP_Text>().text = myAddressLocal;
                }
            }
        }
    }

    private void DrawPixels()
    {

        Ray ray = rayOrigin.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;//this line could be more efficient but only by not remaking the var every update overall impact is negligable
        //Debug.DrawRay(ray.origin, ray.direction, Color.red);
        if (Input.GetMouseButton(1) && IsHost)
            if (Physics.Raycast(ray, out hit))
            {

                //Debug.Log(hit.distance + hit.transform.name);

                drawSurface = hit.collider.gameObject.GetComponent<NDrawSurface>();

                Vector2 pixelUV = hit.textureCoord;

                pixelUV.x = pixelUV.x * drawSurface.texture.width - penSize / 2;
                pixelUV.y = pixelUV.y * drawSurface.texture.height - penSize / 2;

                if (pixelUV.y < 0 || pixelUV.y > drawSurface.textureSize.y || pixelUV.x < 0
                    || pixelUV.x > drawSurface.textureSize.x)
                    return;

                if (!_touchedLastFrame)
                    drawSurface.lastTouchPos = new Vector2(pixelUV.x, pixelUV.y);

                if (_touchedLastFrame && lastUsedDrawSurface == drawSurface)
                {

                    drawSurface.texture.SetPixels((int)pixelUV.x, (int)pixelUV.y, penSize, penSize, colors);

                    for (float f = 0.01f; f < 1.00f; f += 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(drawSurface.lastTouchPos.x, pixelUV.x, f);
                        var lerpY = (int)Mathf.Lerp(drawSurface.lastTouchPos.y, pixelUV.y, f);
                        drawSurface.texture.SetPixels(lerpX, lerpY, penSize, penSize, colors);
                    }
                }

                drawSurface.lastTouchPos = new Vector2(pixelUV.x, pixelUV.y);
                lastUsedDrawSurface = drawSurface;
                _touchedLastFrame = true;
                return;
            }
        _touchedLastFrame = false;

    }

    internal void ChangeToEraser()
    {
        colors = Enumerable.Repeat(new Color(0, 0, 0, 0), (penSize + 10) * (penSize + 10)).ToArray();
    }

    internal void ChangeBrushColour(Color newColor)
    {
        colors = Enumerable.Repeat(newColor, penSize * penSize).ToArray();
    }

    internal void Reset()
    {
        penSize = 3;//change this later
        rayOrigin = GetComponent<Camera>();
        colors = Enumerable.Repeat(new Color(0, 0, 1, 1), penSize * penSize).ToArray();
    }
}
