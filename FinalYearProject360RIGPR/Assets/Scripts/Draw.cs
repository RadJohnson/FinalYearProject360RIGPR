using System;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class Draw : MonoBehaviour
{
    [SerializeField] Camera desktopCamera;

    [SerializeField] int penSize;//can be changed later to a Vector2 just an int now to draw to pixels in a square shaped brush

    //[SerializeField] Renderer rend;
    [SerializeField] Color[] colors = new Color[1];

    [SerializeField] private GameObject drawSurface;
    private bool _touchedLastFrame;
    private Vector2 _lastTouchPos;

    private void Start()
    {
        colors = Enumerable.Repeat(colors[0], penSize * penSize).ToArray();//quick way to just do a for loop I think and can double check this by doing a for loop
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
            DrawPixel();
    }

    void DrawPixel()
    {

        Ray ray = desktopCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction, Color.red);

        if (Physics.Raycast(ray, out hit))
        {
            Vector2 pixelUV = hit.textureCoord;

            pixelUV.x = pixelUV.x * drawSurface.GetComponent<DrawSurface>().texture.width - penSize / 2;
            pixelUV.y = pixelUV.y * drawSurface.GetComponent<DrawSurface>().texture.height - penSize / 2;

            if (pixelUV.y < 0 || pixelUV.y > drawSurface.GetComponent<DrawSurface>().textureSize.y || pixelUV.x < 0 || pixelUV.x > drawSurface.GetComponent<DrawSurface>().textureSize.x) return;

            if (_touchedLastFrame)// this bit doesnt work properly
            {
                drawSurface.GetComponent<DrawSurface>().texture.SetPixels((int)pixelUV.x, (int)pixelUV.y, penSize, penSize, colors);

                for (float f = 0.01f; f < 1.00f; f += 0.01f)
                {
                    var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, pixelUV.x, f);
                    var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, pixelUV.y, f);
                    drawSurface.GetComponent<DrawSurface>().texture.SetPixels(lerpX, lerpY, penSize, penSize, colors);
                }
                drawSurface.GetComponent<DrawSurface>().texture.Apply();
            }

            _lastTouchPos = new Vector2(pixelUV.x, pixelUV.y);
            _touchedLastFrame = true;
            return;
        }

        _touchedLastFrame = false;
    }


    void Reset()
    {
        desktopCamera = GetComponent<Camera>();
        colors = Enumerable.Repeat(colors[0], penSize * penSize).ToArray();
        drawSurface = GameObject.Find("DrawSurface");
    }
}
