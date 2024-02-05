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
    [SyncVar] public bool gainpistolP1 = false;
    [SyncVar] public bool gainpistolP2 = false;
    [SyncVar] public bool gainCode1 = false;

    
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
        enim.SetActive(active);
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
        if(playerScanEnim1==2)
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
        if(playerScanEnim2==2)
        {
            SetActiveEnim(Enim2, true);
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
        if(playerScanEnim3==2)
        {
            SetActiveEnim(Enim3, true);
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
        if(playerScanEnim4==2)
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
        if(playerScanEnim5==2)
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
        if(playerScanEnim1!=2)
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
        if(playerScanEnim2!=2)
        {
            SetActiveEnim(Enim2, false);
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
        if(playerScanEnim3!=2)
        {
            SetActiveEnim(Enim3, false);
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
        if(playerScanEnim4!=2)
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
        if(playerScanEnim5!=2)
        {
            SetActiveEnim(Enim5, false);
        }
    }
    
}
