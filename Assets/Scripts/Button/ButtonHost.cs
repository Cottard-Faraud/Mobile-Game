using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHost : MonoBehaviour
{
    public void ButtonPressed()
    {
        NetworkManagerHUD.Instance.Host();
    }
}
