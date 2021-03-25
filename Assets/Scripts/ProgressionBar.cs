using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionBar : MonoBehaviour
{
    public static ProgressionBar Instance;

    public GameObject start;
    public GameObject end;

    public GameObject spritePlayer1;
    public GameObject spritePlayer2;

    public GameObject markerStart;
    public GameObject markerEnd;

    private float distancePlayer;
    private float distanceSpritePlayer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        distancePlayer = end.transform.position.x - start.transform.position.x;
        distanceSpritePlayer = markerEnd.transform.position.x - markerStart.transform.position.x;

        spritePlayer1.transform.position = new Vector2(markerStart.transform.position.x, spritePlayer1.transform.position.y); 
    }


    private void Update()
    {
        if (Variables.Instance.player1 != null)
            spritePlayer1.transform.position = new Vector2(markerStart.transform.position.x + (Variables.Instance.player1.transform.position.x / distancePlayer) * distanceSpritePlayer, spritePlayer1.transform.position.y);
        if (Variables.Instance.player2 != null)
            spritePlayer2.transform.position = new Vector2(markerStart.transform.position.x + (Variables.Instance.player2.transform.position.x / distancePlayer) * distanceSpritePlayer, spritePlayer1.transform.position.y);
    }
}
