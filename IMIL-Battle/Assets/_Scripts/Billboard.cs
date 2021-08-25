using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private Transform _cam;
    private void LateUpdate() 
    {
        this.transform.LookAt(this.transform.position + _cam.forward);
    }
}
