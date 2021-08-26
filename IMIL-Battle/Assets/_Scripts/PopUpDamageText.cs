using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Invector;
using TMPro;

public class PopUpDamageText : HUDComponent
{
    [SerializeField] private GameObject _popUpDamageTextPrefab;
    [SerializeField] private int _currentDamage = 0;
    public override void Init()
    {
        _enemyController.onReceiveDamage.AddListener(ReceiveDamage);
    }
    public override void Enable()
    {

    }
    public override void Disable()
    {

    }
    public void ReceiveDamage(vDamage vDamage)
    {
        _currentDamage = vDamage.damageValue;
        StartCoroutine(PopUpTextAnimation()); 
    }
    private IEnumerator PopUpTextAnimation() 
    {
        Debug.Log("POP UP");
        //Instantiate Text Mesh Pro
        GameObject popUpDamageTextGO = Instantiate(_popUpDamageTextPrefab, this.transform.position, Quaternion.identity) as GameObject;
        popUpDamageTextGO.transform.SetParent(this.transform);
        popUpDamageTextGO.transform.localPosition = Vector3.zero;
        popUpDamageTextGO.transform.localEulerAngles = Vector3.zero;
        popUpDamageTextGO.transform.localScale = Vector3.one;


        TextMeshProUGUI popUpDamageText = popUpDamageTextGO.GetComponent<TextMeshProUGUI>();
        popUpDamageText.text = _currentDamage.ToString();
        float disappearTime = 5f;
        Color textColor = popUpDamageText.color;

        // Pop up and disappear
        while (true)
        {
            popUpDamageText.transform.position += new Vector3(0, 2.5f, 0) * Time.deltaTime;
            disappearTime -= Time.deltaTime;    
            if (disappearTime < 0)
            {
                textColor.a -= disappearTime * Time.deltaTime;
                popUpDamageText.color = textColor;
                if (textColor.a < 0)
                {
                    GameObject.Destroy(popUpDamageText.gameObject);
                    break;
                }
            }
            yield return null;
        }
        yield return null;
    }
}
 