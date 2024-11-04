using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour {
    public UnityEvent<int, int> healthChanged;
    Animator animator;

    [SerializeField]
    private int _maxHeath = 100;
    public int MaxHealth {
        get {
            return _maxHeath;
        }
        set {
            _maxHeath = value;

            healthChanged?.Invoke(_health, MaxHealth);
        }
    }

    [SerializeField]
    private int _health = 100;

    public int Health {
        get {
            return _health;
        }
        set {
            _health = value;
            if (_health <= 0) {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;
    [SerializeField]
    private bool isInvincible = false;
    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    public bool IsAlive {
        get {
            return _isAlive;
        }
        set {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive: " + value);
        }
    }

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (isInvincible) {
            if (timeSinceHit > invincibilityTime) {
                // Invincibility has worn off
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }

        Hit(10);
    }

    public void Hit(int damage) {
        if (IsAlive && !isInvincible) {
            Health -= damage;
            isInvincible = true;
        }
    }
}