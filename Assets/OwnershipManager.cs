using System;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Collections.Generic;
using System.Linq;
using FishNet.Managing;
using UnityEngine;

public class OwnershipManager : NetworkBehaviour
{
    public delegate void OwnershipManagerEventHandler();

    [SerializeField] private bool requestOwnershipOnStartClient = true;
    [SerializeField] private List<PlayerObjects> playerObjectsList;
    private Dictionary<int , PlayerObjects> playerObjectIndex = new Dictionary<int, PlayerObjects>();
    private Stack<PlayerObjects> playerCounter;

    public int NumberOfPlayers => 6-playerCounter.Count;

    //public static event OwnershipManagerEventHandler OnOwnshipSetupFinished;
    private void Start()
    {
        playerObjectsList.Reverse();
        playerCounter = new Stack<PlayerObjects>(playerObjectsList);
        if (requestOwnershipOnStartClient)
        {
            //Debug.Log($"Requesting ownership for client. Local connection is: {LocalConnection.ClientId}.");

            RequestOwnershipOnClientStarted();
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (requestOwnershipOnStartClient)
        {
            //Debug.Log($"Requesting ownership for client. Local connection is: {LocalConnection.ClientId}.");

            RequestOwnershipOnClientStarted();
        }
    }
    public override void OnStopClient()
    {
        base.OnStopClient();

        if (requestOwnershipOnStartClient)
        {
            Debug.Log($"Requesting desubscribe ownership for client. Local connection is: {LocalConnection.ClientId}.");

            RequestOwnershipOnClientStopped();
        }
    }

    public override void OnStopNetwork()
    {
        base.OnStopNetwork();
        if (requestOwnershipOnStartClient)
        {
            Debug.Log($"Requesting desubscribe ownership for client. Local connection is: {LocalConnection.ClientId}.");

            RequestOwnershipOnClientStopped();
        }
    }
    





    [ServerRpc(RequireOwnership = false)]
    private void RequestOwnershipOnClientStarted(NetworkConnection conn = null)
    {
        //Debug.Log($"Requesting ownership for client. NetworkConnection is: {conn.ClientId}.");

        GiveOwnershipToClient(conn);
    }
    [ServerRpc(RequireOwnership = false)]
    private void RequestOwnershipOnClientStopped(NetworkConnection conn = null)
    {
        Debug.Log($"Requesting desbuscribe ownership for client. NetworkConnection is: {conn.ClientId}.");
        RemoveOwnershipToClient(conn);
    }
    [ServerRpc(RequireOwnership = false)]
    public void RequestObjectListOwnership(NetworkConnection clientConnection, int index)
    {
        foreach (NetworkBehaviour playerObject in playerObjectsList[index].ObjectsToChangeOwnerShip)
        {
            playerObject.GiveOwnership(clientConnection);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void RequestObjectOwnership(NetworkBehaviour nob, NetworkConnection clientConnection)
    {
        nob.GiveOwnership(clientConnection);
    }

    [Server]
    private void GiveOwnershipToClient(NetworkConnection clientConnection)
    {
        //Debug.Log($"Requesting ownership for client: {clientConnection.ClientId}, and playerCounter: {playerCounter}");
        PlayerObjects Index = playerCounter.Pop();
        playerObjectIndex.Add(clientConnection.ClientId, Index);
        foreach (NetworkBehaviour playerObject in Index.ObjectsToChangeOwnerShip)
        {
            //print(playerObject.name);
            playerObject.GiveOwnership(clientConnection);

            //string ownershipMessage = $"{gameObject.name} - OwnershipManager: Owner of object {playerObject.name} is now client {playerObject.OwnerId}";

            //Debug.Log(ownershipMessage);
            //LogMessageOnClientTargetRpc(clientConnection, ownershipMessage);
        }
        //Debug.Log($"Player counter new value: {playerCounter}");
    }
    [Server]
    private void RemoveOwnershipToClient(NetworkConnection clientConnection)
    {
        PlayerObjects index = playerObjectIndex[clientConnection.ClientId];
        foreach (NetworkBehaviour playerObject in index.ObjectsToChangeOwnerShip)
        {
            //print(playerObject.name);
            playerObject.RemoveOwnership();
        }
        playerCounter.Push(index);
        playerObjectIndex.Remove(clientConnection.ClientId);
        Debug.Log($"Player disconnected, player counter new value: {playerCounter}");
    }
    [TargetRpc]
    private void LogMessageOnClientTargetRpc(NetworkConnection conn, string message)
    {
        //Debug.Log(message);
    }
}
