using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    #region MOVEMENT FIELD
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _speed = 6f;
    [SerializeField] private float _turnSmoothTime = 0.1f, _turnSmoothVelocity;
    [SerializeField] private Transform _camera;
    #endregion
    #region GRAVITY FIELD
    [SerializeField] private float _gravity = -9.81f, _groundDistance = 0.4f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private bool _isGrounded;  
    [SerializeField] private Vector3 _velocity;
    #endregion
    #region JUMP FIELD
    [SerializeField] private float _jumpHeight = 1.5f;
    #endregion
    #region ANIMATION 
    [SerializeField] private Animator _anim;
    [SerializeField] private Vector2 _currentAnimationBlendVector, _animationVelocity;
    [SerializeField] private float _animationSmoothTime = 0.1f;
    #endregion
    private void Update() 
    {
        Move();
        ApplyGravityAndJump();
    }
    private void Move() 
    {
        //Get mouse input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        //Rotate by mouse input
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(this.transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);   
        this.transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

        if (direction.magnitude >= 0.1f)
        {
            //Move
            _controller.Move(moveDir.normalized * _speed * Time.deltaTime);   
        }

        //Animation
        _currentAnimationBlendVector = Vector2.SmoothDamp(_currentAnimationBlendVector, new Vector2(horizontal, vertical), ref _animationVelocity, _animationSmoothTime);
        _anim.SetFloat("MoveX", _currentAnimationBlendVector.x);
        _anim.SetFloat("MoveZ", _currentAnimationBlendVector.y);
    }
    private void ApplyGravityAndJump() 
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _whatIsGround);
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2;
        }
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }
        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
}
