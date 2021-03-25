using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public GameObject targetPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.gameObject == targetPlayer)
        {
            collision.GetComponent<PlayerController>().CollisionObstacle(1f, 1.5f);
            Destroy(this.gameObject);
        }
    }
}
