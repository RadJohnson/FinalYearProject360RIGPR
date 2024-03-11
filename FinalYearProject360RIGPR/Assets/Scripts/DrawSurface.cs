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


public class DrawSurface : NetworkBehaviour
{
    [SerializeField] public Texture2D texture;
    [SerializeField] internal Vector2Int textureSize = new Vector2Int(2048, 2048);
    [SerializeField] internal Color[] initialColour = new Color[1];

    //[SerializeField] NetworkVariable<int> nVarInt = new();
    //[SerializeField] NetworkVariable<Color32> nVarCol32 = new();

    NetworkList<MyColor32> nVar_MyColor32 = new NetworkList<MyColor32>(/*,NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner*/);


    //NetworkVariable<Texture2D> nTexture = new NetworkVariable<Texture2D>();

    struct MyTexture2D
    {
        int xAxisSize;
        int yAxisSize;
    }

    //
    // Summary:
    //     Representation of RGBA colors in 32 bit format.
    public struct MyColor32 : INetworkSerializable, IEquatable<MyColor32>
    {
        private int rgba;

        //
        // Summary:
        //     Red component of the color.
        public byte r;

        //
        // Summary:
        //     Green component of the color.
        public byte g;

        //
        // Summary:
        //     Blue component of the color.
        public byte b;

        //
        // Summary:
        //     Alpha component of the color.
        public byte a;

        public byte this[int index]
        {
            get
            {
                return index switch
                {
                    0 => r,
                    1 => g,
                    2 => b,
                    3 => a,
                    _ => throw new IndexOutOfRangeException("Invalid Color32 index(" + index + ")!"),
                };
            }
            set
            {
                switch (index)
                {
                    case 0:
                        r = value;
                        break;
                    case 1:
                        g = value;
                        break;
                    case 2:
                        b = value;
                        break;
                    case 3:
                        a = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Color32 index(" + index + ")!");
                }
            }
        }

        //
        // Summary:
        //     Constructs a new Color32 with given r, g, b, a components.
        //
        // Parameters:
        //   r:
        //
        //   g:
        //
        //   b:
        //
        //   a:
        public MyColor32(byte r, byte g, byte b, byte a)
        {
            rgba = 0;
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref r);
            serializer.SerializeValue(ref g);
            serializer.SerializeValue(ref b);
            serializer.SerializeValue(ref a);
        }

        public bool Equals(MyColor32 other)
        {
            throw new NotImplementedException();
        }
    }



    public override void OnNetworkSpawn()
    {
        for (int i = 0; i < textureSize.x*textureSize.y; i++)
        {
            nVar_MyColor32.Add(new MyColor32());
        }

        base.OnNetworkSpawn();
    }

    private void Update()
    {
        for (int x = 0, i = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                var tmp = texture.GetPixel(x, y);
                nVar_MyColor32[i] = new MyColor32((byte)tmp.r, (byte)tmp.g, (byte)tmp.b, (byte)tmp.a);
                i++;
            }

        }

        Debug.Log(nVar_MyColor32[0]);

        //nVarCol32.Value = initialColour[0];
        //nVarInt.Value = textureSize.x;
        //Debug.Log(OwnerClientId + $"NetworkInt {nVarInt.Value} NetworkCol32 {nVarCol32.Value}");
        //nTexture.Value = texture;
    }

    public void Start()
    {
        Reset();
        for (int i = 0; i < textureSize.x * textureSize.y; i++)
        {
            nVar_MyColor32.Add(new MyColor32());
        }
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

}
