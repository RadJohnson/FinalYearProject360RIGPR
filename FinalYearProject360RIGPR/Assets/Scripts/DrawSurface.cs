using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DrawSurface : MonoBehaviour
{
    [SerializeField] public Texture2D texture;
    [SerializeField] internal Vector2Int textureSize = new Vector2Int(2048, 2048);
    [SerializeField] internal Color[] initialColour = new Color[1];

    public void Start()
    {
        Reset();
    }

    void Reset()
    {
        var r = gameObject.GetComponent<Renderer>();
        texture = new Texture2D(textureSize.x, textureSize.y);
        r.material.mainTexture = texture;

        initialColour = Enumerable.Repeat(new Color(1, 1, 1, 0), textureSize.x * textureSize.y).ToArray();
        texture.SetPixels(initialColour);
        initialColour = new Color[1];
        texture.Apply();

        //initialColour[0] = Color.clear;

        //var r = gameObject.GetComponent<Renderer>();

        //texture = new Texture2D(textureSize.x, textureSize.y);
        ////texture.SetPixels(initialColour);

        //r.material.mainTexture = texture;
    }

}
