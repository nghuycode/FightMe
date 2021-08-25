using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Invector.vCharacterController;

public class PlayerHUD : MonoBehaviour
{
    private static PlayerHUD _instance;
    public static PlayerHUD Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PlayerHUD>();
                //Tell unity not to destroy this object when loading a new scene
                //DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }
    [SerializeField] private Slider _healthBarSlider, _staminaBarSlider;
    [SerializeField] private Image _healthBarFill;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private vThirdPersonController _cc;
    private void Awake() 
    {
        _cc = vThirdPersonController.instance;
    }
    private void Update() 
    {
        UpdateHUD();
    }
    public void UpdateHUD() 
    {
        if (!_cc) _cc = vThirdPersonController.instance;
        UpdateHealthBar();
        UpdateStaminaBar();
    }
    private void UpdateHealthBar() 
    {
        if (_cc.maxHealth != _healthBarSlider.maxValue)
        {
            _healthBarSlider.maxValue = Mathf.Lerp(_healthBarSlider.maxValue, _cc.maxHealth, 2f * Time.fixedDeltaTime);
            _healthBarSlider.onValueChanged.Invoke(_healthBarSlider.value);
        }
        _healthBarSlider.value = Mathf.Lerp(_healthBarSlider.value, _cc.currentHealth, 2f * Time.fixedDeltaTime);
        _healthBarFill.color =  _gradient.Evaluate(_healthBarSlider.normalizedValue);
    }
    private void UpdateStaminaBar()
    {
        if (_cc.maxStamina != _staminaBarSlider.maxValue)
        {
            _staminaBarSlider.maxValue = Mathf.Lerp(_staminaBarSlider.maxValue, _cc.maxStamina, 2f * Time.fixedDeltaTime);
            _staminaBarSlider.onValueChanged.Invoke(_staminaBarSlider.value);
        }
        _staminaBarSlider.value = _cc.currentStamina;
    }
}
