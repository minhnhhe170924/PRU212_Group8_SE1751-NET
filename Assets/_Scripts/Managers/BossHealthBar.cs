using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text healthBarText;

    Damageable bossDamageable;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("GOD");

        if (player == null)
        {
            Debug.Log("Boss not found in this scene");
        }
        bossDamageable = player.GetComponent<Damageable>();

    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(bossDamageable.Health);
        Debug.Log(bossDamageable.MaxHealth);
        healthSlider.value = CalculateSliderPercentage(bossDamageable.Health, bossDamageable.MaxHealth);
        healthBarText.text = "HP " + bossDamageable.Health + " / " + bossDamageable.MaxHealth;
    }

    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable");
        bossDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable");
        bossDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        Debug.Log("Health change");
        healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
        healthBarText.text = "HP " + newHealth + " / " + maxHealth;
    }
}
