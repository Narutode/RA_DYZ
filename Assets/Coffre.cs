using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class Coffre : NetworkBehaviour
{
    public Transform target; // Référence au transform du coffre
    public String targetString;
    public float moveSpeed = 5f; // Vitesse de déplacement du personnage
    public float rotationSpeed = 5f; // Vitesse de rotation du personnage
    string coffrename;
    public GameManager gameManager;
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                coffrename = hit.transform.name;
                Debug.Log(coffrename);
                GameObject character = null;
                switch (coffrename)
                {
                    case  "CoffreP1":
                    
                        Debug.Log(LocalConnection.ClientId);
                        if (LocalConnection.ClientId == 0 && targetString == "CoffreP1")
                        {
                            character = GameObject.FindGameObjectWithTag("Player1");
                            MoveCharacterToCoffre(character.transform);
                        }

                    
                        break;
                    case  "CoffreP2":
                    
                        Debug.Log(LocalConnection.ClientId);
                        if (LocalConnection.ClientId == 1 && targetString == "CoffreP2")
                        {
                            character = GameObject.FindGameObjectWithTag("Player2");
                            MoveCharacterToCoffre(character.transform);

                        }
                        break;
                    case  "Code1":
                        if (LocalConnection.ClientId == 0 && targetString == "Code1")
                        {
                            character = GameObject.FindGameObjectWithTag("Player1");
                            MoveCharacterToCoffre(character.transform);
                        }
                        if (LocalConnection.ClientId == 1 && targetString == "Code1")
                        {
                            character = GameObject.FindGameObjectWithTag("Player2");
                            MoveCharacterToCoffre(character.transform);
                        }

                        break;
                }
            }
            
           
        }
       
    }
    //coliision avec le coffre
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player1") && targetString == "CoffreP1" && !gameManager.gainpistolP1)
        {
            gameManager.gainpistolP1 = true;
            gameManager.GivePistolP1();
            Debug.Log("P1 a gagné le pistolet");
        }
        if (other.gameObject.CompareTag("Player2") && targetString == "CoffreP2" && !gameManager.gainpistolP2)
        {
            gameManager.gainpistolP2 = true;
            gameManager.GivePistolP2();
            Debug.Log("P2 a gagné le pistolet");
        }

        if ((other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2")) && targetString == "Code1" && !gameManager.gainCode1)
        {
            gameManager.gainCode1 = true;
            gameManager.GiveCode1();
            Debug.Log("P1 et P2 a gagné le code");
        }

    }

    private void MoveCharacterToCoffre(Transform character)
    {
        Vector3 targetPosition = target.position;
        
        StartCoroutine(MoveCharacterCoroutine(character, targetPosition));
        
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - character.position);
        StartCoroutine(RotateCharacterCoroutine(character, targetRotation));
    }

    private IEnumerator MoveCharacterCoroutine(Transform character, Vector3 targetPosition)
    {
        while (Vector3.Distance(character.position, targetPosition) > 0.0001f)
        {
            character.position = Vector3.MoveTowards(character.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator RotateCharacterCoroutine(Transform character, Quaternion targetRotation)
    {
        while (Quaternion.Angle(character.rotation, targetRotation) > 0.0001f)
        {
            character.rotation = Quaternion.Slerp(character.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
