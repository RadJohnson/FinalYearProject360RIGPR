using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class Draw : NetworkBehaviour
{
    [SerializeField] Camera rayOrigin;

    [SerializeField] int penSize;//can likely be changed later to a Vector2 just an int now to draw to pixels in a square shaped brush

    [SerializeField] Color[] colors = new Color[1];

    [SerializeField] private DrawSurface drawSurface;// make more efficient as lots of get component calls
    private bool _touchedLastFrame;
    private DrawSurface lastUsedDrawSurface;

    private void Start()
    {
        Reset();
    }

    private void Update()
    {
        DrawPixels();
    }

    private void DrawPixels()
    {

        Ray ray = rayOrigin.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;//this line could be more efficient but only by not remaking the var every update overall impact is negligable

        if (Input.GetMouseButton(1) && IsHost)
            if (Physics.Raycast(ray, out hit))
            {
                drawSurface = hit.collider.gameObject.GetComponent<DrawSurface>();

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
        drawSurface = GameObject.Find("DrawSurface").GetComponent<DrawSurface>();
    }
}
