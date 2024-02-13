using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class filCut : NetworkBehaviour
{

    public GameManager gameManager;
    
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if(gameManager.gainscissors && gameManager.cutWrong==false && LocalConnection.ClientId == 0)
                        {
                            if (hit.transform.name == "fil1")
                            {
                                gameManager.wrongCut();
                                hit.transform.gameObject.SetActive(false);
                            }
                            else if (hit.transform.name == "fil2")
                            {
                                gameManager.wrongCut();
                                hit.transform.gameObject.SetActive(false);
                            }
                            else if (hit.transform.name == "fil3")
                            {
                                gameManager.rightCut();
                                hit.transform.gameObject.SetActive(false);
                            }
                            else if (hit.transform.name == "fil4")
                            {
                                gameManager.wrongCut();
                                hit.transform.gameObject.SetActive(false);
                            }
                        }
                    

               
            }
        }
    }

    
}
