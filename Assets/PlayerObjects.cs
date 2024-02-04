using FishNet.Object;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjects : MonoBehaviour
{
    [SerializeField] private List<NetworkBehaviour> objectsToChangeOwnerShip;

    public List<NetworkBehaviour> ObjectsToChangeOwnerShip => objectsToChangeOwnerShip;
}