using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBarSlider : HUDComponent
{
    [SerializeField] private Slider _healthBarSlider;
    [SerializeField] private Image _healthBarFill;
    [SerializeField] private Gradient _gradient;
    public override void Init()
    {
        
    }
    public override void UpdateData() 
    {
        if (_enemyController.maxHealth != _healthBarSlider.maxValue)
        {
            _healthBarSlider.maxValue = Mathf.Lerp(_healthBarSlider.maxValue, _enemyController.maxHealth, 2f * Time.fixedDeltaTime);
            _healthBarSlider.onValueChanged.Invoke(_healthBarSlider.value);
        }
        _healthBarSlider.value = Mathf.Lerp(_healthBarSlider.value, _enemyController.currentHealth, 2f * Time.fixedDeltaTime);
        _healthBarFill.color =  _gradient.Evaluate(_healthBarSlider.normalizedValue);
    }
    public override void Enable() 
    {
        _healthBarSlider.enabled = false;
    }
    public override void Disable() 
    {
        _healthBarSlider.enabled = false;
    }
}
