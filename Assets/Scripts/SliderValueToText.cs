using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class SliderValueToText : MonoBehaviour
{
    public Slider sliderUI;
    public TMP_Text textSliderValue;

    void Start()
    {
        ShowSliderValue();
    }

    public void ShowSliderValue()
    {
        string sliderMessage = "Bonus probability = " + System.Math.Round(sliderUI.value, 2); ;
        textSliderValue.text = sliderMessage;
    }
}