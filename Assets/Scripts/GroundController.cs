using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public GameObject player;

    void FixedUpdate()
    {
        if (player != null)
            this.transform.position = new Vector3(player.transform.position.x, transform.position.y, this.transform.position.z);
    }
}
