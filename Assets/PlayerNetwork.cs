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
    [SerializeField] private GameManager gameManager;
    public TextMeshProUGUI pseudoText;
    public int Id => id;
    [SerializeField] private List<NetworkBehaviour> ObjectsToChangeOwnerShip;
    
    
    public GameObject pistol;
    public GameObject code1;
    public GameObject scissors;
    public GameObject code2;
    public GameObject Enim2Interface;
    
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
        pseudoText.color = gameManager.playerColors[id];
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
    public void GivePistol()
    {
        pistol.SetActive(true);
    }
    
    public void GiveCode1()
    {
        code1.SetActive(true);
    }
    public void GiveScissors()
    {
        scissors.SetActive(true);
    }
    public void GiveCode2()
    {
        code2.SetActive(true);
    }
    
    

}
