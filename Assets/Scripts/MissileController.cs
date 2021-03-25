using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{

    public float missileSpeed = 10f;
    public GameObject targetPlayer;
    
    private void Update()
    {
        float step = missileSpeed * Time.deltaTime;
        if (targetPlayer != null)
            transform.position = Vector3.MoveTowards(transform.position, targetPlayer.transform.position, step);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.gameObject == targetPlayer)
        {
            collision.GetComponent<PlayerController>().CollisionObstacle(1f, 1.5f);
            Destroy(this.gameObject);
        }
    }
}
