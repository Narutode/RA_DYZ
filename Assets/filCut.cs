using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class filCut : NetworkBehaviour
{
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
                        if (hit.transform.name == "filjaune")
                        {
                            wrongCut();
                        }
                        else if (hit.transform.name == "filbleu")
                        {
                            rightCut();
                        }
                        else if (hit.transform.name == "filrouge")
                        {
                            wrongCut();
                        }
                        else if (hit.transform.name == "filvert")
                        {
                            wrongCut();
                        }
                        
                        
                    }

               
            }
        }
    }
    public void wrongCut()
    {
        Debug.Log("wrong cut");
    }
    public void rightCut()
    {
        Debug.Log("right cut");
    }
    
}
