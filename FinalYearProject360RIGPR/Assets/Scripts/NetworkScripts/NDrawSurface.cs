using System.Linq;
using UnityEngine;
using Unity.Netcode;

public class NDrawSurface : NetworkBehaviour
{
    [SerializeField] internal Texture2D texture;
    [SerializeField] internal Vector2Int textureSize = new Vector2Int(2048, 2048);
    [SerializeField] internal Color[] initialColour = new Color[1];
    internal Vector2 lastTouchPos;
    private byte[] N;


    private void Update()
    {
        if (IsClient)
        {
            if (IsOwner)
            {
                N = texture.EncodeToPNG();
                SendTexturesClientRpc(N);
            }
            texture.Apply();
        }
    }

    [ClientRpc]
    private void SendTexturesClientRpc(byte[] receivedByte)
    {
        texture.LoadImage(receivedByte);
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

        initialColour = Enumerable.Repeat(new Color(1, 1, 1, 0), textureSize.x * textureSize.y).ToArray();
        texture.SetPixels(initialColour);
        initialColour = new Color[1];
        texture.Apply();
    }
}
