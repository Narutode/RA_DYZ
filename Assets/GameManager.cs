using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    [Header("Player Settings")]
    [SerializeField] public List<PlayerNetwork> playerList;

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
    /*
    [Header("Enim 4")]
    [Header("Enim 5")]*/
    
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
    [ObserversRpc(RunLocally = true)]
    public void SetActiveEnim(GameObject enim, bool active)
    {
        //enim.SetActive(active);
        if (active)
        {
            Spawn(enim);
        }
        else
        {
            Despawn(enim);
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
            SetActiveEnim(Enim1, true);
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
            SetActiveEnim(Enim2, true);
            
            if(playerHand1.activeSelf == false)
            {
                if(LocalConnection.ClientId == 0 && gainpistolP1)
                {
                    Spawn(playerHand1);
                    playerList[0].Enim2Interface.SetActive(true);
                }
            }
            if(playerHand2.activeSelf == false)
            {
                if(LocalConnection.ClientId == 1 && gainpistolP2)
                {
                    Spawn(playerHand2);
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
            SetActiveEnim(Enim3, true);
            if(scissorsplayer1.activeSelf == false)
            {
                if(LocalConnection.ClientId == 0 && gainscissors)
                {
                    Spawn(scissorsplayer1);
                }
            }
            if(paperplayer2.activeSelf == false)
            {
                if(LocalConnection.ClientId == 1)
                {
                    Spawn(paperplayer2);
                }
            }
            
            if(bombeplayer1.activeSelf == false)
            {
                if(LocalConnection.ClientId == 0)
                {
                    Spawn(bombeplayer1);
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
            SetActiveEnim(Enim4, true);
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
            SetActiveEnim(Enim5, true);
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
            SetActiveEnim(Enim1, false);
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
            SetActiveEnim(Enim2, false);
            if (playerHand1.activeSelf)
            {
                if(LocalConnection.ClientId == 0)
                {
                    Despawn(playerHand1);
                    playerList[0].Enim2Interface.SetActive(false);
                }
            }
            if (playerHand2.activeSelf)
            {
                if(LocalConnection.ClientId == 1)
                {
                    Despawn(playerHand2);
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
            SetActiveEnim(Enim3, false);
            if(scissorsplayer1.activeSelf && Enim3.activeSelf)
            {
                if(LocalConnection.ClientId == 0 && gainscissors)
                {
                    Despawn(scissorsplayer1);
                }
            }
            if(paperplayer2.activeSelf && Enim3.activeSelf)
            {
                if(LocalConnection.ClientId == 1)
                {
                    Despawn(paperplayer2);
                }
            }
            
            if(bombeplayer1.activeSelf && Enim3.activeSelf)
            {
                if(LocalConnection.ClientId == 0)
                {
                    Despawn(bombeplayer1);
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
            SetActiveEnim(Enim4, false);
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
            SetActiveEnim(Enim5, false);
        }
    }
    
}
