using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrawSurface : MonoBehaviour
{
    [SerializeField] internal Texture2D texture;
    [SerializeField] internal Vector2Int textureSize = new Vector2Int(2048*2, 2048*2);
    [SerializeField] internal Color[] initialColour = new Color[1];

    void Start()
    {
        var r = gameObject.GetComponent<Renderer>();
    
        texture = new Texture2D(textureSize.x, textureSize.y);

        initialColour = Enumerable.Repeat(new Color(1,1,1,0), textureSize.x * textureSize.y).ToArray();
        texture.SetPixels(initialColour);
        initialColour = new Color[1];
        r.material.mainTexture = texture;
    }

    void Reset()
    {
        initialColour[0] = Color.clear;

        var r = gameObject.GetComponent<Renderer>();

        texture = new Texture2D(textureSize.x, textureSize.y);
        //texture.SetPixels(initialColour);

        r.material.mainTexture = texture;
    }

}
