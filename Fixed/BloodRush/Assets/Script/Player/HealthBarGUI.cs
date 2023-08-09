using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarGUI : MonoBehaviour
{
    public Image ImgHealthBar;
    public float percentTest;

    public void Update()
    {
        SetBarPercent(percentTest);
    }

    public void SetBarPercent(float percent)
    {
        ImgHealthBar.fillAmount = percent / 100.0f;
    }
}
