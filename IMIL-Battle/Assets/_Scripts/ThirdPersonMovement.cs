using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _speed = 6f;
    [SerializeField] private float _turnSmoothTime = 0.1f, _turnSmoothVelocity;
    [SerializeField] private Transform _camera;

    private void Update() 
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(this.transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);   
            this.transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            _controller.Move(moveDir.normalized * _speed * Time.deltaTime);
        }
    }
}
