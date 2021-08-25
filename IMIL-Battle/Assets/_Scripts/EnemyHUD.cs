using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Invector.vCharacterController.AI;

public class EnemyHUD : MonoBehaviour
{
    private static EnemyHUD _instance;
    public static EnemyHUD Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<EnemyHUD>();
                //Tell unity not to destroy this object when loading a new scene
                //DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }
    [SerializeField] private Slider _healthBarSlider;
    [SerializeField] private Image _healthBarFill;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private v_AIController _aiController;
    private void Awake() 
    {
        _aiController.onDead.AddListener(DisableHealthBar);
        InitHUD();
    }
    private void Update() 
    {
        UpdateHUD();
    }
    private void InitHUD() 
    {
        _healthBarSlider.enabled = true;
    }
    public void UpdateHUD() 
    {
        UpdateHealthBar();
    }
    private void UpdateHealthBar() 
    {
        if (_aiController.maxHealth != _healthBarSlider.maxValue)
        {
            _healthBarSlider.maxValue = Mathf.Lerp(_healthBarSlider.maxValue, _aiController.maxHealth, 2f * Time.fixedDeltaTime);
            _healthBarSlider.onValueChanged.Invoke(_healthBarSlider.value);
        }
        _healthBarSlider.value = Mathf.Lerp(_healthBarSlider.value, _aiController.currentHealth, 2f * Time.fixedDeltaTime);
        _healthBarFill.color =  _gradient.Evaluate(_healthBarSlider.normalizedValue);
    }
    private void DisableHealthBar(GameObject GO) 
    {
        _healthBarSlider.enabled = false;
    }
}
