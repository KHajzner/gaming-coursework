using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    public Slider slider;
    
    public void UpdateHealthBar(float health, float maxHealth)
    {
        slider.value = health/maxHealth;
    }
}
