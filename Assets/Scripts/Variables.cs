using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
{
    public static Variables Instance;

    public GameObject player1;
    public GameObject player2;

    public GameObject localPlayer;

    public GameObject spawn1;
    public GameObject spawn2;

    public bool isServer;

    private void Awake()
    {
        Instance = this;
    }
}
