using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{
    public event EventHandler OnDeathOccured;

    public HealthSystem _healthSystem;

    [Header("Basic")]
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private BloodScreen _bloodScreen;
    [SerializeField] private int _maxHP;
    [SerializeField] private int _currentHP;

    [Header("Regen")]
    [SerializeField] private bool _canRegen;
    [SerializeField] private int _regenAmount;
    [SerializeField] private float _regenRate;
    [SerializeField] private float _regenDelay;
    private float _regenDelayTimer;
    private float _regenRateTimer;
    private bool _isRegenerating;


    private void Awake()
    {
        _healthSystem = new HealthSystem(_maxHP);
        if (_healthBar != null) _healthBar.Setup(_healthSystem);
        if (_bloodScreen != null) _bloodScreen.Setup(_healthSystem);
        _healthSystem.OnDeath += _healthSystem_OnDeath;
        _healthSystem.OnDamaged += _healthSystem_OnDamaged;
    }

    // Update is called once per frame
    void Update()
    {
        HealthRegen();
        _currentHP = _healthSystem.CurrentHealth;
    }


    public void ToggleHealthBar(bool state)
    {
        if (_healthBar != null) _healthBar.gameObject.SetActive(state);
    }

    private void _healthSystem_OnDeath(object sender, EventArgs e)
    {
        if (OnDeathOccured != null) OnDeathOccured(this, EventArgs.Empty);
    }

    public void HealthRegen()
    {
        if (_canRegen && _isRegenerating)
        {
            if (_regenDelayTimer >= _regenDelay)
            {
                if (_regenRateTimer >= _regenRate)
                {
                    _healthSystem.Heal(_regenAmount);
                    _regenRateTimer = 0;
                    if (_currentHP >= _maxHP)
                    {
                        _isRegenerating = false;
                    }
                }
                else
                {
                    _regenRateTimer += Time.deltaTime;
                }
            }
            else
            {
                _regenDelayTimer += Time.deltaTime;
            }
        }
    }


    private void _healthSystem_OnDamaged(object sender, EventArgs e)
    {
        _isRegenerating = true;
        _regenDelayTimer = 0;
        _regenRateTimer = 0;
    }

    private void TakeDamage(GameObject damagerObj)
    {
        // Get Damage Info from Damager GameObject
        Damager tempDamager = damagerObj.GetComponent<Damager>();

        // Check if GameObject can be Affected by Damager
        if (gameObject.tag == "Player" && tempDamager.CanAffect == CanAffect.Player || gameObject.tag == "Enemy" && tempDamager.CanAffect == CanAffect.Enemy || tempDamager.CanAffect == CanAffect.Both)
        {
            // If Damager is One Hit
            if (tempDamager.DamagerType == DamagerType.OneHit)
            {
                _healthSystem.Damage(tempDamager.DamageAmount);
            }
            // If Damager is Over Time
            else if (tempDamager.DamagerType == DamagerType.OverTime)
            {
                // To Be Written
            }
            // If Damager is Insta Death
            else if (tempDamager.DamagerType == DamagerType.InstaDeath)
            {
                _healthSystem.Damage(_maxHP);
            }
        }
    }


    // Collision Handling
    // --------------------
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Damager"))
        {
            if (gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<Damager>().UsedBy.TryGetComponent<EnemyAI>(out EnemyAI enemyai);
                if (enemyai != null) enemyai.HitSuccess = true;
            }
            TakeDamage(other.gameObject);
        }
    }
}
