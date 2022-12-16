using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodScreen : MonoBehaviour
{
    [SerializeField] private Image BloodScreenOverlay;
    private HealthSystem _healthSystem;
    private Color _colorTemp;

    // Start is called before the first frame update
    void Start()
    {
        _colorTemp = BloodScreenOverlay.color;

        _colorTemp.a = 1 - _healthSystem.GetHealthPercent();
        BloodScreenOverlay.color = _colorTemp;
    }

    public void Setup(HealthSystem healthSystem)
    {
        this._healthSystem = healthSystem;
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }

    private void HealthSystem_OnHealthChanged(object sender, EventArgs e)
    {
        _colorTemp.a = 1 - _healthSystem.GetHealthPercent();
        BloodScreenOverlay.color = _colorTemp;
    }
}
