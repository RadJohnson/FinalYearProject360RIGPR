using System.Linq;
using UnityEngine;

public class DrawSurface : MonoBehaviour
{
    [SerializeField] internal Texture2D texture;
    [SerializeField] internal Vector2Int textureSize = new Vector2Int(2048, 2048);
    [SerializeField] internal Color[] initialColour = new Color[1];
    internal Vector2 lastTouchPos;

    private void Update()
    {
        texture.Apply();
    }

    internal void Start()
    {
        Reset();
        gameObject.GetComponent<Renderer>().material.mainTexture = texture;
    }

    private void Reset()
    {
        var r = gameObject.GetComponent<Renderer>();
        texture = new Texture2D(textureSize.x, textureSize.y);

        initialColour = Enumerable.Repeat(initialColour[0], textureSize.x * textureSize.y).ToArray();
        texture.SetPixels(initialColour);
        initialColour = new Color[1];
        texture.Apply();
    }
}
