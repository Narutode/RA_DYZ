using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class filCut : MonoBehaviour
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
                if(gameManager.gainscissors && gameManager.cutWrong==false)
                        {
                            if (hit.transform.name == "filJaune")
                            {
                                gameManager.rightCut();
                                hit.transform.gameObject.SetActive(false);
                            }
                            else if (hit.transform.name == "filBleu")
                            {
                                gameManager.wrongCut();
                                hit.transform.gameObject.SetActive(false);
                            }
                            else if (hit.transform.name == "filRouge")
                            {
                                gameManager.wrongCut();
                                hit.transform.gameObject.SetActive(false);
                            }
                            else if (hit.transform.name == "filVert")
                            {
                                gameManager.wrongCut();
                                hit.transform.gameObject.SetActive(false);
                            }
                        }
                    

               
            }
        }
    }

    
}
