using Unity.Netcode;
using UnityEngine;

public class RpcTest : NetworkBehaviour
{
    //[SerializeField] Texture2D drawSurface;

    public override void OnNetworkSpawn()
    {
        if (!IsServer && IsOwner) //Only send an RPC to the server on the client that owns the NetworkObject that owns this NetworkBehaviour instance
        {
            TestServerRpc(0, NetworkObjectId/*,drawSurface*/);
        }
    }

    [ClientRpc]
    void TestClientRpc(int value, ulong sourceNetworkObjectId/*, Texture2D texture*/)
    {
        Debug.Log($"Client Received the RPC #{value} on NetworkObject #{sourceNetworkObjectId}");
        if (IsOwner) //Only send an RPC to the server on the client that owns the NetworkObject that owns this NetworkBehaviour instance
        {
            TestServerRpc(value + 1, sourceNetworkObjectId/*, texture*/);
        }
    }

    [ServerRpc]
    void TestServerRpc(int value, ulong sourceNetworkObjectId/*, Texture2D texture*/)
    {
        Debug.Log($"Server Received the RPC #{value} on NetworkObject #{sourceNetworkObjectId}");
        TestClientRpc(value, sourceNetworkObjectId/*, texture*/);
    }
}