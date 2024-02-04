using System;
using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class DisableIfIsNotOwner : NetworkBehaviour
{
    [SerializeField] protected Behaviour component;


   /* public override void OnStartClient()
    {
        base.OnStartClient();

        if (!OwnerMatches(LocalConnection))
        {
            component.enabled = false;
        }
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        component.enabled = false;
    }*/

    public void Start()
    {
        if (OwnerMatches(LocalConnection))
        {
            component.enabled = true;
        }
        else
        {
            component.enabled = false;
        }
    }
}
