using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LabelInputView : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField labelNameInput;
    [SerializeField]
    private Slider colorSlider;
    [SerializeField]
    private Image colorIndicator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colorSlider.onValueChanged.AddListener(colorSlider_ValueChanged);
        colorSlider.value = 0;
        
    }

    private void colorSlider_ValueChanged(float arg0)
    {
        colorIndicator.color = Color.HSVToRGB(arg0/255f, 1f, 1f);
    }

    public void CreateNewLabel(){
        Debug.Log(colorSlider.value);
        LabelViewModel newLabel = new LabelViewModel(labelNameInput.text, Color.HSVToRGB(colorSlider.value/255f, 1f, 1f));
        newLabel.Store();

    }
}
