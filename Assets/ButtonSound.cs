using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip[] audioClip;
    public List<int> PushedButtons = new List<int>();
    string buttonName;
    public GameManager gameManager;
    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                buttonName = hit.transform.name;
                switch (buttonName)
                {
                    case "mybutton1":
                        audioSource.clip = audioClip[0];
                        audioSource.Play();
                        PushedButtons.Add(1);
                        CheckCode();
                        break;
                    case "mybutton2":
                        audioSource.clip = audioClip[1];
                        audioSource.Play();
                        PushedButtons.Add(2);
                        CheckCode();
                        break;
                    case "mybutton3":
                        audioSource.clip = audioClip[2];
                        audioSource.Play();
                        PushedButtons.Add(3);
                        CheckCode();
                        break;
                    case "mybutton4":
                        audioSource.clip = audioClip[3];
                        audioSource.Play();
                        PushedButtons.Add(4);
                        CheckCode();
                        break;
                    case "mybutton5":
                        audioSource.clip = audioClip[4];
                        audioSource.Play();
                        PushedButtons.Add(5);
                        CheckCode();
                        break;
                    case "mybutton6":
                        audioSource.clip = audioClip[5];
                        audioSource.Play();
                        PushedButtons.Add(6);
                        CheckCode();
                        break;
                    
                    
                }
            }
        }
        
    }
    public void CheckCode()
    {

        if (PushedButtons.Count > 6)
        {
            //remove first element
            PushedButtons.RemoveAt(0);
        }
        if (PushedButtons.Count == 6)
        {
            if (PushedButtons[0] == 1 && PushedButtons[1] == 2 && PushedButtons[2] == 3 && PushedButtons[3] == 4 && PushedButtons[4] == 5 && PushedButtons[5] == 6)
            {
                gameManager.rightEnim4();
            }

        }
    }
}
