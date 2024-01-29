using System.Collections;
using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using TMPro;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    // Start is called before the first frame update
    [SyncVar] public int id;
    [SyncVar] public string pseudo;
    [SerializeField] private GameManager gameManager;
    public TextMeshProUGUI pseudoText;
    public int Id => id;
    [SerializeField] private List<NetworkBehaviour> ObjectsToChangeOwnerShip;
    
    public void SetId(int id)
    {
        this.id = id;
    }
    public override void OnStartNetwork()
    {
        base.OnStartNetwork();
        
        
    }
    
    private void RequestOwnershipOnClientStarted(NetworkConnection conn = null)
    {
        //Debug.Log($"Requesting ownership for client. NetworkConnection is: {conn.ClientId}.");

        GiveOwnershipToClient(conn);
    }
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        NetworkObject networkObject = GetComponent<NetworkObject>();
        id = networkObject.Owner.ClientId;
        Debug.Log("Login Player id : " + id);
        gameManager.playerList.Add(this);
        
        pseudoText.text = "Joueur " + (id+1);
        RequestOwnershipOnClientStarted();
    }
    
    private void GiveOwnershipToClient(NetworkConnection clientConnection)
    {

        foreach (NetworkBehaviour playerObject in ObjectsToChangeOwnerShip)
        {
            playerObject.GiveOwnership(clientConnection);
            string ownershipMessage = $"{gameObject.name} - OwnershipManager: Owner of object {playerObject.name} is now client {playerObject.OwnerId}";

            Debug.Log(ownershipMessage);
        }
    }
    public override void OnStopClient()
    {
        base.OnStopClient();
        gameManager.playerList.Remove(this);
    }

}
