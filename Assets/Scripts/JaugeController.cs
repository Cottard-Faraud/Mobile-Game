using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JaugeController : MonoBehaviour
{
    public static JaugeController Instance;

    private Slider slider;

    public float value = 1;
    public float time = 0.5f;

    public float penalty = 5.0f;

    public GameObject glow;

    private bool gameStart = false;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        slider = this.transform.GetChild(0).GetComponent<Slider>();
        slider.value = 0;

        glow.SetActive(false);

        StartCoroutine(FillJauge());
    }

    IEnumerator FillJauge()
    {
        while (true)
        {
            if (gameStart)
            {
                if (slider.value != slider.maxValue)
                {
                    yield return new WaitForSeconds(0.5f);
                    if (gameStart) //Pour éviter de remplir la jauge si on quitte et qu'on relance très rapidement
                        slider.value += value;
                }
                else if (glow.activeSelf == false)
                    glow.SetActive(true);
            }
            yield return null;
        }
    }

    public void CollisionObstacle()
    {
        if (slider.value == slider.maxValue)
            return;

        if (slider.value - penalty >= 0)
            slider.value -= penalty;
        else
            slider.value = 0;
    }

    public void ResetValue()
    {
        slider.value = 0;
    }

    public bool UsePower()
    {
        if (slider.value != slider.maxValue)
            return false;
        else
        {
            ResetValue();
            glow.SetActive(false);
            return true;
        }

    }

    public void StartGame()
    {
        gameStart = true;
    }

    public void StopGame()
    {
        gameStart = false;
        slider.value = 0;
        glow.SetActive(false);
    }
}
