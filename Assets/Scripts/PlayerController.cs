using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerController : NetworkBehaviour
{
    private Rigidbody2D rb;

    private int playerID;

    private NetworkController nc;

    public float speed = 5;
    private float speedTemp = 1;
    public float jumpHeight = 2;
    private bool isGrounded = true;

    public float penaltySpeed = 0.5f;
    public float penaltyTime = 1f;

    public GameObject groundPrefab;

    public bool isInvincible = true;

    public GameObject missilePrefab;
    public GameObject bombePrefab;

    private bool gameStart = false;

    private void Start()
    {
        GameObject ground = Instantiate(groundPrefab);
        ground.GetComponent<GroundController>().player = this.gameObject;

        gameStart = false;
        JaugeController.Instance.StopGame();
        ButtonStartGame.Instance.ShowButton();

        if (isLocalPlayer)
        {
            nc = this.GetComponent<NetworkController>();

            CameraController.Instance.player = this.gameObject;

            if (isServer)
            {
                playerID = 1;
                Variables.Instance.player1 = this.gameObject;

                GetComponent<SpriteRenderer>().material.color = Color.blue;
            }
            else
            {
                playerID = 2;
                Variables.Instance.player2 = this.gameObject;

                GetComponent<SpriteRenderer>().material.color = Color.yellow;
            }
        }
        else
        {
            if (isServer)
            {
                Variables.Instance.player2 = this.gameObject;
                GetComponent<SpriteRenderer>().material.color = Color.yellow;
            }
            else
            {
                Variables.Instance.player1 = this.gameObject;
                GetComponent<SpriteRenderer>().material.color = Color.blue;
            }
        }

        rb = this.GetComponent<Rigidbody2D>();

        //Reset de la position des obstacles
        foreach (ObstacleController oc in Object.FindObjectsOfType<ObstacleController>())
        {
            oc.ResetObstacle();
        }

        JaugeController.Instance.ResetValue();
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

        if (!gameStart)
            return;

        transform.position += new Vector3(1 * Time.deltaTime * speed * speedTemp, 0, 0);
        nc.MovePlayer(this.transform.position, playerID);
    }

    public void MovePlayer(Vector3 position, int ID)
    {

        if (ID != playerID)
            transform.position = position;
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        if (!gameStart)
            return;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            rb.velocity = Vector2.up * jumpHeight + Vector2.right * jumpHeight / 10;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (First())
                StartCoroutine(Invincible(2.0f));
            else
                StartCoroutine(ChangeSpeed(5, 2.0f));
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Variables.Instance.player1 == null || Variables.Instance.player2 == null)
                return;
            
            if (First())
            {
                nc.Bomb(playerID);              
            }
            else
            {
                nc.Missile(playerID);
            }
        }
    }

    public void Bomb()
    {
        GameObject bomb = Instantiate(bombePrefab);
        bomb.transform.position = this.transform.position;
        if (playerID == 1)
            bomb.GetComponent<BombController>().targetPlayer = Variables.Instance.player2;
        else
            bomb.GetComponent<BombController>().targetPlayer = Variables.Instance.player1;
    }

    public void Missile()
    {
        GameObject missile = Instantiate(missilePrefab);
        missile.transform.position = this.transform.position;
        if (playerID == 1)
            missile.GetComponent<MissileController>().targetPlayer = Variables.Instance.player2;
        else
            missile.GetComponent<MissileController>().targetPlayer = Variables.Instance.player1;
    }

    private bool First()
    {
        if (Variables.Instance.player1 == null || Variables.Instance.player2 == null)
            return false;

        if (playerID == 1)
        {
            if (Variables.Instance.player1.transform.position.x >= Variables.Instance.player2.transform.position.x)
                return true;
            else
                return false;
        }
        else
        {
            if (Variables.Instance.player2.transform.position.x >= Variables.Instance.player1.transform.position.x)
                return true;
            else
                return false;
        }
    }

    IEnumerator ChangeSpeed(float speedMultiplier, float time)
    {
        speedTemp *= speedMultiplier;
        yield return new WaitForSeconds(time);
        speedTemp /= speedMultiplier;
    }

    IEnumerator Invincible(float time)
    {
        isInvincible = true;
        yield return new WaitForSeconds(time);
        isInvincible = false;
    }

    public void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")
            || collision.gameObject.CompareTag("Player"))
        {
            isGrounded = true;
        }
    }

    public void CollisionObstacle()
    {
        if (isInvincible)
            return;

        StartCoroutine(ChangeSpeed(penaltySpeed, penaltyTime));
        JaugeController.Instance.CollisionObstacle();
    }

    public void CollisionObstacle(float penalty, float time)
    {
        if (isInvincible)
            return;

        StartCoroutine(ChangeSpeed(penalty, time));
        JaugeController.Instance.CollisionObstacle();
    }

    public void ButtonStart()
    {
        if (!isLocalPlayer)
            return;

        nc.StartPlayer();
    }

    public void StartGame()
    {
        gameStart = true;
        JaugeController.Instance.StartGame();
        ButtonStartGame.Instance.HideButton();
    }
}
