using System.Collections;
using System.Collections.Generic;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using TMPro;
using UnityEngine;

public delegate void TimeUpdate(float newTime);
public delegate void UpdateMessage(string message);
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
    public TextMeshProUGUI Enim3WrongTimeLeft;
    public TextMeshProUGUI Enim3WrongMessage;
    public GameObject code3;
    public GameObject code4;
    public TextMeshProUGUI EndGameMessage;
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
    public override  void OnStartClient()
    {
        base.OnStartClient();  
        GameManager.OnTimeUpdate += UpdateTimeText;
        GameManager.OnMessageUpdate += UpdateMessageText;
        gameManager = FindObjectOfType<GameManager>();
        NetworkObject networkObject = GetComponent<NetworkObject>();
        if (networkObject.Owner.ClientId == -1)
        {
            id = 0;
        }
        else
        {
            id = networkObject.Owner.ClientId;
        }
        Debug.Log("Login Player id : " + LocalConnection.ClientId);
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
        
        GameManager.OnTimeUpdate -= UpdateTimeText;
        GameManager.OnMessageUpdate -= UpdateMessageText;
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
    public void GiveCode3()
    {
        code3.SetActive(true);
    }
    public void GiveCode4()
    {
        code4.SetActive(true);
    }
    
    [ObserversRpc(RunLocally = true)]
    // Méthode pour mettre à jour le texte du temps
    private void UpdateTimeText(float newTime)
    { 
        if(newTime < 0)
        {
            Enim3WrongTimeLeft.text="";
            return;
        }
        // Convertir le temps en minutes, secondes 
        int minutes = Mathf.FloorToInt(newTime / 60);
        int seconds = Mathf.FloorToInt(newTime % 60);
        // Formater la chaîne de temps
        string formattedTime = string.Format("{0:D2}:{1:D2}", minutes, seconds);
        // Mettre à jour le texte
        Enim3WrongTimeLeft.text = formattedTime;
    }
    [ObserversRpc(RunLocally = true)]
    private void UpdateMessageText(string message)
    {
        Enim3WrongMessage.text = message;
    }
    

    

}
