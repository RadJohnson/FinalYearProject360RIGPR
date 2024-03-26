using System.Linq;
using Unity.Netcode;
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

    [Space(5),Header("HostPrefabUI")]
    [SerializeField] private GameObject MainMenuScenePrefab;
    [SerializeField] private GameObject VideoUIPrefab;
    
    private bool prefabSpawned = false;
    
    private void Start()
    {
        Reset();
    }

    private void Update()
    {
        DrawPixels();

        if (IsHost)
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1) && prefabSpawned == false)
            {
                Instantiate(MainMenuScenePrefab);
        
                prefabSpawned = true;
            }
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(2) && prefabSpawned == false)
            {
                Instantiate(VideoUIPrefab);
        
                VideoUIPrefab.GetComponent<VideoPlayingUIManager>().drawScript = this;
        
                prefabSpawned = true;
            }
        }
    }

    public void OnSceneChanged()
    {
        // Check if the player is the host
        if (IsHost)
        {
            // Check if the next scene is the first scene and the prefab hasn't been spawned yet
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1) && !prefabSpawned)
            {
                Instantiate(MainMenuScenePrefab);
                prefabSpawned = true;
            }
            // Check if the next scene is the second scene and the prefab hasn't been spawned yet
            else if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(2) && !prefabSpawned)
            {
                Instantiate(VideoUIPrefab);
                VideoUIPrefab.GetComponent<VideoPlayingUIManager>().drawScript = this;
                prefabSpawned = true;
            }
        }
    }


    private void DrawPixels()
    {

        Ray ray = rayOrigin.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;//this line could be more efficient but only by not remaking the var every update overall impact is negligable
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        if (Input.GetMouseButton(1) && IsHost)
            if (Physics.Raycast(ray, out hit))
            {

                Debug.Log(hit.distance + hit.transform.name);

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
        colors = Enumerable.Repeat(new Color(0, 0, 0, 0), (penSize + 8) * (penSize + 8)).ToArray();
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
