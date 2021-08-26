using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHUD : MonoBehaviour
{
    [SerializeField] private List<HUDComponent> _hudComponents;
    private void Awake() 
    {
        InitHUD();
    }
    private void Update() 
    {
        UpdateHUD();
    }
    public void InitHUD() 
    {
        foreach(var hudComponent in _hudComponents)
        {
            hudComponent.Init();
        }
    }
    public void UpdateHUD() 
    {
        foreach(var hudComponent in _hudComponents)
        {
            hudComponent.UpdateData();
        }
    }
}
