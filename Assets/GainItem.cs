using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class GainItem : NetworkBehaviour
{
    public GameManager gameManager;
    public String itemName;
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if(itemName == "Scissors" )
                {
                    gameManager.playerList[0].GiveScissors();
                    gameManager.gainscissors = true;
                }
                if(itemName == "Code2")
                {
                    gameManager.playerList[0].GiveCode2();
                    gameManager.playerList[1].GiveCode2();
                    
                }
                if(itemName == "Code3")
                {
                    gameManager.playerList[0].GiveCode3();
                    gameManager.playerList[1].GiveCode3();
                }
                if(itemName == "Code4" )
                {
                    gameManager.playerList[0].GiveCode4();
                    gameManager.playerList[1].GiveCode4();
                }
                
               
            }
        }
    }
}
