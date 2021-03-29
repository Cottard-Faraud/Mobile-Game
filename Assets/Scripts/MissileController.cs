using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{

    public float missileSpeed = 20f;
    public GameObject targetPlayer;
    
    private void Update()
    {
        float step = missileSpeed * Time.deltaTime;
        if (targetPlayer != null)
            transform.position = Vector3.MoveTowards(transform.position, targetPlayer.transform.position, step);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == targetPlayer)
        {
            if(targetPlayer == Variables.Instance.localPlayer)
            {
                collision.GetComponent<PlayerController>().CollisionObstacle(.1f, 1.5f);
                Debug.Log("Missile hits");
            }  
            Destroy(this.gameObject);
        }
    }
}
