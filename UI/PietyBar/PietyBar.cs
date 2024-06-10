using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PietyBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Gradient gradiant;
    [SerializeField]
    private Image fill;

    public void SetMaxPiety(int piety){
        slider.maxValue = piety;
        slider.value = piety;

        fill.color = gradiant.Evaluate(1f);
    }
    
    public void SetPiety(int piety)
    {
        slider.value = piety;

        fill.color = gradiant.Evaluate(slider.normalizedValue);
    }
}
