using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float fallingSpeed = 2.0f;
    public float timeUntilDestruction = 2.0f;

    private Vector3 initPosition;

    private void Start()
    {
        initPosition = this.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().CollisionObstacle();

            StartCoroutine(Destruction());
        }
    }

    IEnumerator Destruction()
    {
        float time = 0;
        while (time < timeUntilDestruction)
        {
            transform.position += new Vector3(0, -1 * Time.deltaTime * fallingSpeed, 0);
            time += Time.deltaTime;
            yield return null;
        }
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void ResetObstacle()
    {
        this.transform.position = initPosition;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}
