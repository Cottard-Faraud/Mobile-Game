using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
{
    public static Variables Instance;

    public GameObject player1;
    public GameObject player2;

    private void Awake()
    {
        Instance = this;
    }
}
