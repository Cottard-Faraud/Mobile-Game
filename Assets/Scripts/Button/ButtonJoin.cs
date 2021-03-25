using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonJoin : MonoBehaviour
{
    public void ButtonPressed()
    {
        NetworkManagerHUD.Instance.Join();
    }
}
