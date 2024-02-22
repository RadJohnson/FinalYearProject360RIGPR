using System;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Draw : MonoBehaviour
{
    [SerializeField] Camera camera;

    [SerializeField] int penSize;//can be changed later to a Vector2 just an int now to draw to pixels in a square shaped brush

    //[SerializeField] Renderer rend;
    [SerializeField] Color[] colors = new Color[1];

    [SerializeField] private DrawSurface drawSurface;// make more efficient as lots of get component calls
    private bool _touchedLastFrame;
    private DrawSurface lastUsedDrawSurface;
    private Vector2 _lastTouchPos;

    private void Start()
    {
        Reset();
        //colors = Enumerable.Repeat(colors[0], penSize * penSize).ToArray();//quick way to just do a for loop I think and can double check this by doing a for loop
    }

    void Update()
    {
        //if (Input.GetMouseButton(1))// this likely causes the issues with connected drawing
        DrawPixels();
    }

    void DrawPixels()
    {

        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //RAY needs to be mdae more effceint by just setting it once then simply re-using it

        //Debug.DrawRay(ray.origin, ray.direction, Color.red);
        
        if (Input.GetMouseButton(1))
            if (Physics.Raycast(ray, out hit))
            //if (Physics.Raycast(camera.transform.position,Vector3.forward,out hit,10))
            {

                Debug.Log(hit.distance + hit.transform.name);
                
                drawSurface = hit.collider.gameObject.GetComponent<DrawSurface>();

                Vector2 pixelUV = hit.textureCoord;

                pixelUV.x = pixelUV.x * drawSurface.texture.width - penSize / 2;
                pixelUV.y = pixelUV.y * drawSurface.texture.height - penSize / 2;

                if (pixelUV.y < 0 || pixelUV.y > drawSurface.textureSize.y || pixelUV.x < 0
                    || pixelUV.x > drawSurface.textureSize.x)
                    return;

                if (!_touchedLastFrame)
                    drawSurface.lastTouchPos = new Vector2(pixelUV.x, pixelUV.y);

                if (_touchedLastFrame && lastUsedDrawSurface == drawSurface)// this bit doesnt work properly as it still needs to try and do a small ammount more of interpolation between a coupld of surfaces
                {
                    drawSurface.texture.SetPixels((int)pixelUV.x, (int)pixelUV.y, penSize, penSize, colors);

                    for (float f = 0.01f; f < 1.00f; f += 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(drawSurface.lastTouchPos.x, pixelUV.x, f);
                        var lerpY = (int)Mathf.Lerp(drawSurface.lastTouchPos.y, pixelUV.y, f);
                        drawSurface.texture.SetPixels(lerpX, lerpY, penSize, penSize, colors);
                    }
                    drawSurface.texture.Apply();
                }

                drawSurface.lastTouchPos = new Vector2(pixelUV.x, pixelUV.y);
                lastUsedDrawSurface = drawSurface;
                _touchedLastFrame = true;
                return;
            }
        _touchedLastFrame = false;

    }

    public void ChangeToEraser()
    {
        colors = Enumerable.Repeat(new Color(0, 0, 0, 0), (penSize + 8) * (penSize + 8)).ToArray();
    }

    public void ChangeColour(Color newColor)
    {
        //Color brushColor = GetComponent<Button>().colors.normalColor;
        //colors = Enumerable.Repeat(brushColor, penSize * penSize).ToArray();
    
        colors = Enumerable.Repeat(newColor, penSize * penSize).ToArray();
    }

    public void Reset()
    {
        penSize = 10;//change this later
        camera = GetComponent<Camera>();
        colors = Enumerable.Repeat(new Color(0,0,1,1), penSize * penSize).ToArray();
        drawSurface = GameObject.Find("DrawSurface").GetComponent<DrawSurface>();
    }
}
