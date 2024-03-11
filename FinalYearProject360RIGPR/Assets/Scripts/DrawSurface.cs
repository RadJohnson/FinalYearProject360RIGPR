using UnityEngine;
using System.Linq;
using Unity.Netcode;
using Unity.VisualScripting;
using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using System.Collections;

public class DrawSurface : NetworkBehaviour
{
    [SerializeField] public Texture2D texture;
    [SerializeField] internal Vector2Int textureSize = new Vector2Int(2048, 2048);
    [SerializeField] internal Color[] initialColour = new Color[1];

    //NetworkVariable<Texture2D> nVarTexture2d = new();

    public Texture2D MyTexture;
    public byte[] receivedBytes;
    public byte[] N;
    public Texture2D texi;

    IEnumerator GetRenderTexturePixel(Texture2D tex)
    {
        Texture2D tempTex = new Texture2D(tex.width, tex.height);
        yield return new WaitForEndOfFrame();
        tempTex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
        tempTex.Apply();
        N = tempTex.EncodeToPNG();
    }
    private void Update()
    {
        //nVarTexture2d.Value = texture;

        //nVarTexture2d.Value.Apply();

        texi = texture;

        if (IsServer)
        {
            StartCoroutine(GetRenderTexturePixel(texi));
        }   
        if (IsClient)
        {
            SendTexturesServerRpc(N);
        }

    }

    public void Start()
    {
        Reset();
        texi = texture;
    }

    void Reset()
    {
        var r = gameObject.GetComponent<Renderer>();
        texture = new Texture2D(textureSize.x, textureSize.y);
        r.material.mainTexture = texture;

        initialColour = Enumerable.Repeat(new Color(1, 1, 1, 1), textureSize.x * textureSize.y).ToArray();
        texture.SetPixels(initialColour);
        initialColour = new Color[1];
        texture.Apply();

        //initialColour[0] = Color.clear;

        //var r = gameObject.GetComponent<Renderer>();

        //texture = new Texture2D(textureSize.x, textureSize.y);
        ////texture.SetPixels(initialColour);

        //r.material.mainTexture = texture;
    }

    [ServerRpc]
    public void UpdateTextureServerRpc(int _lerpX,int _lerpY,int _penSize, Color[] _colors)
    {
        texture.SetPixels(_lerpX, _lerpY, _penSize, _penSize, _colors);

        Debug.Log(texture);
    }

    [ServerRpc]
    void SendTexturesServerRpc(byte[] receivedByte)
    {
        texi = null;
        texi = new Texture2D(1, 1);
        texi.LoadImage(receivedByte);
        texi.Apply();
    }
}
