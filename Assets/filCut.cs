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
                   if (LocalConnection.ClientId == 0)
                    {
                        if(gameManager.gainscissors && gameManager.cutWrong==false)
                        {
                            if (hit.transform.name == "filJaune")
                            {
                                gameManager.rightCut();
                                Despawn(hit.transform.gameObject);
                            }
                            else if (hit.transform.name == "filBleu")
                            {
                                gameManager.wrongCut();
                                Despawn(hit.transform.gameObject);
                            }
                            else if (hit.transform.name == "filRouge")
                            {
                                gameManager.wrongCut();
                                Despawn(hit.transform.gameObject);
                            }
                            else if (hit.transform.name == "filVert")
                            {
                                gameManager.wrongCut();
                                Despawn(hit.transform.gameObject);
                            }
                        }
                    }

               
            }
        }
    }

    
}
