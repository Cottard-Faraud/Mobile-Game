using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    public GameObject player;

    private void Awake()
    {
        Instance = this;
    }

    void FixedUpdate()
    {
        if (player != null)
            this.transform.position = new Vector3(player.transform.position.x, transform.position.y, this.transform.position.z);
    }
}
