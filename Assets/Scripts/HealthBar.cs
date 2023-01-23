using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Damageable playerDamageable;
    public TMP_Text healthBarText;
    public Slider healthSlider;


    private void Awake()
    {

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerDamageable = player.GetComponent<Damageable>();
    }


    void Start()
    {
        healthSlider.value = CaculateHealthSliderPertange(playerDamageable.Health, playerDamageable.MaxHealth);
        healthBarText.text = "HP" + playerDamageable.Health + "/" + playerDamageable.MaxHealth;
    }

    private void OnEnable()
    {
        playerDamageable.HealthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        playerDamageable.HealthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        healthSlider.value = CaculateHealthSliderPertange(newHealth, maxHealth);
        healthBarText.text = "HP" + newHealth + "/" + maxHealth;
    }

    private float CaculateHealthSliderPertange(float health, float maxHealth)
    {
        return health / maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
