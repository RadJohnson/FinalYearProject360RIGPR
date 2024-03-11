using System.Linq;
using UnityEngine;
using Unity.Netcode;

public class Draw : NetworkBehaviour
{
    [SerializeField] Camera camera;

    [SerializeField] int penSize;//can be changed later to a Vector2 just an int now to draw to pixels in a square shaped brush

    //[SerializeField] Renderer rend;
    [SerializeField] Color[] colors = new Color[1];

    [SerializeField] private DrawSurface drawSurface;// make more efficient as lots of get component calls
    private bool _touchedLastFrame;
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
        
        if (Input.GetMouseButton(1)/* && IsServer*/)
            if (Physics.Raycast(ray, out hit))
            //if (Physics.Raycast(camera.transform.position,Vector3.forward,out hit,10))
            {

                //Debug.Log(hit.distance + hit.transform.name);
                
                drawSurface = hit.collider.gameObject.GetComponent<DrawSurface>();

                Vector2 pixelUV = hit.textureCoord;

                pixelUV.x = pixelUV.x * drawSurface.texture.width - penSize / 2;
                pixelUV.y = pixelUV.y * drawSurface.texture.height - penSize / 2;

                if (pixelUV.y < 0 || pixelUV.y > drawSurface.textureSize.y || pixelUV.x < 0
                    || pixelUV.x > drawSurface.textureSize.x)
                    return;

                if (!_touchedLastFrame)
                    _lastTouchPos = new Vector2(pixelUV.x, pixelUV.y);

                if (_touchedLastFrame)// this bit doesnt work properly
                {

                    //drawSurface.UpdateTextureServerRpc((int)pixelUV.x, (int)pixelUV.y, penSize, colors);
                    drawSurface.texture.SetPixels((int)pixelUV.x, (int)pixelUV.y, penSize, penSize, colors);

                    for (float f = 0.01f; f < 1.00f; f += 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, pixelUV.x, f);
                        var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, pixelUV.y, f);
                        //drawSurface.UpdateTextureServerRpc(lerpX, lerpY, penSize, colors);
                        drawSurface.texture.SetPixels(lerpX, lerpY, penSize, penSize, colors);
                    }
                    drawSurface.texture.Apply();
                }

                _lastTouchPos = new Vector2(pixelUV.x, pixelUV.y);
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
