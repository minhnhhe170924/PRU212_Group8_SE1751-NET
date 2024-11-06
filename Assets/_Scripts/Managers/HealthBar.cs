using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Slider healthSlider;
    public TMP_Text healthBarText;

    Damageable playerDamageable;

    private void Awake() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if(player == null ) {
            Debug.Log("Player not found in this scene");
        }
        playerDamageable = player.GetComponent<Damageable>();

    }

    // Start is called before the first frame update
    void Start() {
        Debug.Log(playerDamageable.Health);
        Debug.Log(playerDamageable.MaxHealth);
        healthSlider.value = CalculateSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
        healthBarText.text = "HP " + playerDamageable.Health + " / " + playerDamageable.MaxHealth;
    }

    private float CalculateSliderPercentage(float currentHealth, float maxHealth) {
        return currentHealth / maxHealth;
    }

    private void OnEnable() {
        Debug.Log("OnEnable");
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable() {
        Debug.Log("OnDisable");
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnPlayerHealthChanged(int newHealth, int maxHealth) {
        Debug.Log("Health change");
        healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
        healthBarText.text = "HP " + newHealth + " / " + maxHealth;
    }
}
