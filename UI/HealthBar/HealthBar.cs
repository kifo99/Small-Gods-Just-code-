using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Gradient gradiant;
    [SerializeField]
    private Image fill;

    public void SetMaxHealth(int health){
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradiant.Evaluate(1f);
    }
    
    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradiant.Evaluate(slider.normalizedValue);
    }

}
