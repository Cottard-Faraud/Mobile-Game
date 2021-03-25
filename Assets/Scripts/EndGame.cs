using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public static EndGame Instance;

    public GameObject buttonRestart;

    public GameObject winText;
    public GameObject looseText;

    private void Awake()
    {
        Instance = this;
    }

    public void ButtonRestart()
    {
        if (Variables.Instance.player1 != null)
            Variables.Instance.player1.GetComponent<PlayerController>().ButtonRestart();
        if (Variables.Instance.player2 != null)
            Variables.Instance.player2.GetComponent<PlayerController>().ButtonRestart();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == Variables.Instance.localPlayer)
        {
            if (Variables.Instance.player1 != null)
                Variables.Instance.player1.GetComponent<PlayerController>().EndGameTrigger();
            if (Variables.Instance.player2 != null)
                Variables.Instance.player2.GetComponent<PlayerController>().EndGameTrigger();
        }
    }

    public void Win()
    {
        buttonRestart.SetActive(true);
        winText.SetActive(true);
        looseText.SetActive(false);
    }

    public void Loose()
    {
        buttonRestart.SetActive(true);
        winText.SetActive(false);
        looseText.SetActive(true);
    }

    public void HideAll()
    {
        buttonRestart.SetActive(false);
        winText.SetActive(false);
        looseText.SetActive(false);
    }
}
