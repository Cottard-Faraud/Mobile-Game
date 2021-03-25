using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStartGame : MonoBehaviour
{
    public static ButtonStartGame Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ClickButton()
    {
        if (Variables.Instance.player1 != null)
            Variables.Instance.player1.GetComponent<PlayerController>().ButtonStart();
        if (Variables.Instance.player2 != null)
            Variables.Instance.player2.GetComponent<PlayerController>().ButtonStart();
    }

    public void HideButton()
    {
        this.gameObject.SetActive(false);
    }

    public void ShowButton()
    {
        this.gameObject.SetActive(true);
    }
}
