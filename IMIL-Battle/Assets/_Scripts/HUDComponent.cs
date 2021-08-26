using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Invector.vCharacterController.AI;

public class HUDComponent : MonoBehaviour
{
    public enum HUDType {HealthBarSlider, PopUpDamageText}
    public HUDType type;
    public v_AIController _enemyController;
    public void AssignEnemyController(v_AIController enemyController) {_enemyController = enemyController;}
    public virtual void Init(){}
    public virtual void UpdateData(){}
    public virtual void Enable(){}
    public virtual void Disable(){}
}
