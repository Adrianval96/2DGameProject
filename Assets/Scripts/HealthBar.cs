using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    
    
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        PlayerHealthController.onPlayerHealthChange += UpdateHealthBar;
    }

    public void UpdateHealthBar(float healthPercentage)
    {
        healthBar.value = healthPercentage;
    }
}
