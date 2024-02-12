using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    [Header("Player Settings")]
    [SerializeField] public List<PlayerNetwork> playerList;
    public static event TimeUpdate OnTimeUpdate;
    public static event UpdateMessage OnMessageUpdate;
    public List<Color> playerColors = new List<Color>
    {
        Color.red,
        Color.blue,
    };
    public int playerScanNeed=2;
    [SyncVar] public int playerScanEnim1 = 0;
    [SyncVar] public int playerScanEnim2 = 0;
    [SyncVar] public int playerScanEnim3 = 0;
    [SyncVar] public int playerScanEnim4 = 0;
    [SyncVar] public int playerScanEnim5 = 0;
    public GameObject Enim1;
    public GameObject Enim2;
    public GameObject Enim3;
    public GameObject Enim4;
    public GameObject Enim5;
    [Header("Enim 1")]
    [SyncVar] public bool gainpistolP1 = false;
    [SyncVar] public bool gainpistolP2 = false;
    [SyncVar] public bool gainCode1 = false;
    [Header("Enim 2")]
    public GameObject playerHand1;
    public GameObject playerHand2;
    public List<GameObject> NPCList;

    public GameObject scissors;
    public GameObject code2;
    [SyncVar] public bool gainscissors= false;
    [Header("Enim 3")]
    public GameObject paperplayer2;
    public GameObject bombeplayer1;
    public GameObject scissorsplayer1;
    public bool cutWrong = false;
    public int TimeToWaitBeforeStart = 30 ;
    public GameObject code3;
    
    [Header("Enim 4")]
    public GameObject bureau;
    public GameObject paperplayer1;
    public GameObject code4;
    /*[Header("Enim 5")]*/
    
    public void GivePistolP1()
    {
        PlayerNetwork playerP1 = playerList[0];
        playerP1.GivePistol();
    }
    public void GivePistolP2()
    {
        PlayerNetwork playerP2 = playerList[1];
        playerP2.GivePistol();
    }
    [ObserversRpc(RunLocally = true)]
    public void GiveCode1()
    {
        if(LocalConnection.ClientId == 0)
        {
            PlayerNetwork playerP1 = playerList[0];
            playerP1.GiveCode1();
        }
        else if(LocalConnection.ClientId == 1)
        {
            PlayerNetwork playerP2 = playerList[1];
            playerP2.GiveCode1();
        }   
    }

    public void ScanEnim1()
    {
        if(LocalConnection.ClientId == 0)
        {
            playerScanEnim1++;
        }
        else if(LocalConnection.ClientId == 1)
        {
            playerScanEnim1++;
        }
        if(playerScanEnim1==playerScanNeed)
        {
            //SetActiveEnim(Enim1, true);
        }
    }



    public void ScanEnim2()
    {
        if(LocalConnection.ClientId == 0)
        {
            playerScanEnim2++;
        }
        else if(LocalConnection.ClientId == 1)
        {
            playerScanEnim2++;
        }
        if(playerScanEnim2==playerScanNeed)
        {
            //SetActiveEnim(Enim2, true);
            
            if(playerHand1.activeSelf == false)
            {
                if(LocalConnection.ClientId == 0 && gainpistolP1)
                {
                    playerHand1.SetActive(true);
                    playerList[0].Enim2Interface.SetActive(true);
                }
            }
            if(playerHand2.activeSelf == false)
            {
                if(LocalConnection.ClientId == 1 && gainpistolP2)
                {
                    playerHand2.SetActive(true);
                    playerList[1].Enim2Interface.SetActive(true);
                }
            }
            
        }

    }
    public void ScanEnim3()
    {
        if(LocalConnection.ClientId == 0)
        {
            playerScanEnim3++;
        }
        else if(LocalConnection.ClientId == 1)
        {
            playerScanEnim3++;
        }
        if(playerScanEnim3==playerScanNeed)
        {

            //SetActiveEnim(Enim3, true);
            if(scissorsplayer1.activeSelf == false)
            {
                if(LocalConnection.ClientId == 0 && gainscissors)
                {
                    scissorsplayer1.SetActive(true);
                }
            }
            if(paperplayer2.activeSelf == false)
            {
                if(LocalConnection.ClientId == 1)
                {
                    paperplayer2.SetActive(true);
                    PlayerNetwork playerP2 = playerList[1];
                    if(!playerP2.Enim3WrongTimeLeft.gameObject.activeSelf)
                    {
                        playerP2.Enim3WrongTimeLeft.gameObject.SetActive(true);
                    }
                    if(!playerP2.Enim3WrongMessage.gameObject.activeSelf)
                    {
                        playerP2.Enim3WrongMessage.gameObject.SetActive(true);
                    }
                }
            }
        
            if(bombeplayer1.activeSelf == false)
            {
                if(LocalConnection.ClientId == 0)
                {
                    bombeplayer1.SetActive(true);
                }
            }
            if(LocalConnection.ClientId == 0)
            {
                PlayerNetwork playerP1 = playerList[0];
                if(!playerP1.Enim3WrongTimeLeft.gameObject.activeSelf)
                {
                    playerP1.Enim3WrongTimeLeft.gameObject.SetActive(true);
                }
                if(!playerP1.Enim3WrongMessage.gameObject.activeSelf)
                {
                    playerP1.Enim3WrongMessage.gameObject.SetActive(true);
                }
            }
            if(LocalConnection.ClientId == 1)
            {
                PlayerNetwork playerP2 = playerList[1];
                if(!playerP2.Enim3WrongTimeLeft.gameObject.activeSelf)
                {
                    playerP2.Enim3WrongTimeLeft.gameObject.SetActive(true);
                }
                if(!playerP2.Enim3WrongMessage.gameObject.activeSelf)
                {
                    playerP2.Enim3WrongMessage.gameObject.SetActive(true);
                }
            }
              
            
            
        }
    }
    public void ScanEnim4()
    {
        if(LocalConnection.ClientId == 0)
        {
            playerScanEnim4++;
        }
        else if(LocalConnection.ClientId == 1)
        {
            playerScanEnim4++;
        }
        if(playerScanEnim4==playerScanNeed)
        {
            //SetActiveEnim(Enim4, true);
        }
        if(bureau.activeSelf == false)
        {
            if(LocalConnection.ClientId == 1)
            {
                bureau.SetActive(true);
            }
        }
        if(paperplayer1.activeSelf == false)
        {
            if(LocalConnection.ClientId == 0)
            {
                paperplayer1.SetActive(true);
            }
        }
    }
    public void ScanEnim5()
    {
        if(LocalConnection.ClientId == 0)
        {
            playerScanEnim5++;
        }
        else if(LocalConnection.ClientId == 1)
        {
            playerScanEnim5++;
        }
        if(playerScanEnim5==playerScanNeed)
        {
            //SetActiveEnim(Enim5, true);
        }
    }
    public void DesScanEnim1()
    {
        if(LocalConnection.ClientId == 0)
        {
            playerScanEnim1--;
        }
        else if(LocalConnection.ClientId == 1)
        {
            playerScanEnim1--;
        }
        if(playerScanEnim1!=playerScanNeed)
        {
            //SetActiveEnim(Enim1, false);
        }
    }
    public void DesScanEnim2()
    {
        if(LocalConnection.ClientId == 0)
        {
            playerScanEnim2--;
        }
        else if(LocalConnection.ClientId == 1)
        {
            playerScanEnim2--;
        }
        if(playerScanEnim2!=playerScanNeed)
        {
            //SetActiveEnim(Enim2, false);
            if (playerHand1.activeSelf)
            {
                if(LocalConnection.ClientId == 0)
                {
                    playerHand1.SetActive(false);
                    playerList[0].Enim2Interface.SetActive(false);
                }
            }
            if (playerHand2.activeSelf)
            {
                if(LocalConnection.ClientId == 1)
                {
                    playerHand2.SetActive(false);
                    playerList[1].Enim2Interface.SetActive(false);
                }
            }
        }
    }
    public void DesScanEnim3()
    {
        if(LocalConnection.ClientId == 0)
        {
            playerScanEnim3--;
        }
        else if(LocalConnection.ClientId == 1)
        {
            playerScanEnim3--;
        }
        if(playerScanEnim3!=playerScanNeed)
        {
            //(Enim3, false);
            if(scissorsplayer1.activeSelf && Enim3.activeSelf)
            {
                if(LocalConnection.ClientId == 0 && gainscissors)
                {
                    scissorsplayer1.SetActive(false);
                   
                }
            }
            if(paperplayer2.activeSelf && Enim3.activeSelf)
            {
                if(LocalConnection.ClientId == 1)
                {
                    paperplayer2.SetActive(false);
                    
                    PlayerNetwork playerP2 = playerList[1];
                    if(playerP2.Enim3WrongTimeLeft.gameObject.activeSelf)
                    {
                        playerP2.Enim3WrongTimeLeft.gameObject.SetActive(false);
                    }
                    if(playerP2.Enim3WrongMessage.gameObject.activeSelf)
                    {
                        playerP2.Enim3WrongMessage.gameObject.SetActive(false);
                    }
                }
            }
            
            if(bombeplayer1.activeSelf && Enim3.activeSelf)
            {
                if(LocalConnection.ClientId == 0)
                {
                    bombeplayer1.SetActive(false);
                    PlayerNetwork playerP1 = playerList[0];
                    if(playerP1.Enim3WrongTimeLeft.gameObject.activeSelf)
                    {
                        playerP1.Enim3WrongTimeLeft.gameObject.SetActive(false);
                    }
                    if(playerP1.Enim3WrongMessage.gameObject.activeSelf)
                    {
                        playerP1.Enim3WrongMessage.gameObject.SetActive(false);
                    }
                }
            }
            if(LocalConnection.ClientId == 0)
            {
                PlayerNetwork playerP1 = playerList[0];
                if(playerP1.Enim3WrongTimeLeft.gameObject.activeSelf)
                {
                    playerP1.Enim3WrongTimeLeft.gameObject.SetActive(false);
                }
                if(playerP1.Enim3WrongMessage.gameObject.activeSelf)
                {
                    playerP1.Enim3WrongMessage.gameObject.SetActive(false);
                }
            }
            if(LocalConnection.ClientId == 1)
            {
                PlayerNetwork playerP2 = playerList[1];
                if(playerP2.Enim3WrongTimeLeft.gameObject.activeSelf)
                {
                    playerP2.Enim3WrongTimeLeft.gameObject.SetActive(false);
                }
                if(playerP2.Enim3WrongMessage.gameObject.activeSelf)
                {
                    playerP2.Enim3WrongMessage.gameObject.SetActive(false);
                }
            }
           


        }
        

    }
    public void DesScanEnim4()
    {
        if(LocalConnection.ClientId == 0)
        {
            playerScanEnim4--;
        }
        else if(LocalConnection.ClientId == 1)
        {
            playerScanEnim4--;
        }
        if(playerScanEnim4!=playerScanNeed)
        {
            //SetActiveEnim(Enim4, false);
            if(bureau.activeSelf == true)
            {
                if(LocalConnection.ClientId == 1)
                {
                    bureau.SetActive(false);
                    
                }
            }
            if(paperplayer1.activeSelf == true)
            {
                if(LocalConnection.ClientId == 0)
                {
                    paperplayer1.SetActive(false);
                }
            }
        }
    }
    public void DesScanEnim5()
    {
        if(LocalConnection.ClientId == 0)
        {
            playerScanEnim5--;
        }
        else if(LocalConnection.ClientId == 1)
        {
            playerScanEnim5--;
        }
        if(playerScanEnim5!=playerScanNeed)
        {
           // SetActiveEnim(Enim5, false);
        }
    }
    public void wrongCut()
    {
        Debug.Log("wrong cut");
        cutWrong=true;
        if(LocalConnection.ClientId == 0)
        {
            PlayerNetwork playerP1 = playerList[0];
            playerP1.Enim3WrongTimeLeft.gameObject.SetActive(true);
        }
        else if(LocalConnection.ClientId == 1)
        {
            PlayerNetwork playerP2 = playerList[1];
            playerP2.Enim3WrongTimeLeft.gameObject.SetActive(true);
        }
        StartCoroutine(cutWrongFalse());
    }
    public IEnumerator cutWrongFalse()
    {
        
        float remainingTime = TimeToWaitBeforeStart;
        OnMessageUpdate?.Invoke("WRONG CUT");
        while (remainingTime > 0)
        {
           
            OnTimeUpdate?.Invoke(remainingTime);
    
            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
        }
        OnTimeUpdate?.Invoke(0);
        cutWrong=false;
        if(LocalConnection.ClientId == 0)
        {
            PlayerNetwork playerP1 = playerList[0];
            playerP1.Enim3WrongTimeLeft.gameObject.SetActive(false);
            playerP1.Enim3WrongMessage.gameObject.SetActive(false);
        }
        else if(LocalConnection.ClientId == 1)
        {
            PlayerNetwork playerP2 = playerList[1];
            playerP2.Enim3WrongTimeLeft.gameObject.SetActive(false);
            playerP2.Enim3WrongMessage.gameObject.SetActive(false);
        }
        
    }
    public void rightCut()
    {
        bombeplayer1.SetActive(false);
        paperplayer2.SetActive(false);
        code3.SetActive(true);
    }
    public void rightEnim4()
    {
        bureau.SetActive(false);
        paperplayer1.SetActive(false);
        code4.SetActive(true);
    }
}
