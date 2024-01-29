using System.Collections;
using System.Collections.Generic;
using FishNet.Object;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    [Header("Player Settings")]
    [SerializeField] public List<PlayerNetwork> playerList;
}
