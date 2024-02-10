using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using TMPro;
using UnityEngine;

public class ButtonCodeFinal : NetworkBehaviour
{
    string buttonName;
    public List<int> PushedButtons = new List<int>();
    public GameManager gameManager;
    public TextMeshProUGUI CodeFinal;
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                buttonName = hit.transform.name;
                switch (buttonName)
                {
                    case "Number1":
                        PushedButtons.Add(1);
                        sendCode();
                        break;
                    case "Number2":
                        PushedButtons.Add(2);
                        sendCode();
                        break;
                    case "Number3":
                        PushedButtons.Add(3);
                        sendCode();
                        break;
                    case "Number4":
                        PushedButtons.Add(4);
                        sendCode();
                        break;
                    case "Number5":
                        PushedButtons.Add(5);
                        sendCode();
                        break;
                    case "Number6":
                        PushedButtons.Add(6);
                        sendCode();
                        break;
                    case "Number7":
                        PushedButtons.Add(7);
                        sendCode();
                        break;
                    case "Number8":
                        PushedButtons.Add(8);
                        sendCode();
                        break;
                    case "Number9":
                        PushedButtons.Add(9);
                        sendCode();
                        break;
                    case "Number0":
                        PushedButtons.Add(0);
                        sendCode();
                        break;
                    case "Number11":
                        PushedButtons.Add(11);
                        sendCode();
                        break;
                    case "Number12":
                        PushedButtons.Add(12);
                        sendCode();
                        break;
                }
            }
        }
    }
    public void sendCode()
    {
        if(PushedButtons.Count > 4)
        {
            //remove first element
            PushedButtons.RemoveAt(0);
        }
         if (PushedButtons[0] == 1 && PushedButtons[1] == 2 && PushedButtons[2] == 3 && PushedButtons[3] == 4)
        {
            if(LocalConnection.ClientId == 0)
            {
                gameManager.playerList[0].EndGameMessage.gameObject.SetActive(true);
            }
            else if (LocalConnection.ClientId == 1)
            {
                gameManager.playerList[1].EndGameMessage.gameObject.SetActive(true);
            }
        }
        string code = "";
        foreach (int i in PushedButtons)
        {
            if (i == 11)
            {
                code += "#";
            }
            else if(i== 12)
            {
                code += "*";
            }
            else
            {
                code += i.ToString();
            }
        }
        UpdateCodeFinalText(code);
        
    }
    [ObserversRpc(RunLocally = true)]
    public void UpdateCodeFinalText(string message)
    {
        CodeFinal.text = message;
    }
}
    
