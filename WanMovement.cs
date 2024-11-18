using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WanMovement : MonoBehaviour
{

    private Rigidbody2D MyRigidBody;
    private Vector2 _movementInput;
    [SerializeField]
    private float _rotationSpeed;
    [SerializeField]
    private float _speed;
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;
    private Camera _camera;
    Vector2 mousePos;

    private void Awake()
    {
        MyRigidBody = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
    }

    void Update()
    {
        mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        SetPlayerVelocity();   
        RotateInDirectionOfInput();

        Vector2 lookDir = mousePos - MyRigidBody.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        MyRigidBody.rotation = angle;
    }

    private void SetPlayerVelocity()
    {
        _smoothedMovementInput = Vector2.SmoothDamp(
            _smoothedMovementInput,
            _movementInput, ref _movementInputSmoothVelocity,
            0.1f);
        MyRigidBody.velocity = _smoothedMovementInput * _speed;

        PreventPlayerGoingOffScreen();
    }

    private void PreventPlayerGoingOffScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

        if ((screenPosition.x < 0 && MyRigidBody.velocity.x < 0) || (screenPosition.x > _camera.pixelWidth && MyRigidBody.velocity.x > 0)){
            MyRigidBody.velocity = new Vector2(0, MyRigidBody.velocity.y);
        }
        
        if ((screenPosition.y < 0 && MyRigidBody.velocity.y < 0) || (screenPosition.y > _camera.pixelHeight && MyRigidBody.velocity.y > 0)){
            MyRigidBody.velocity = new Vector2(MyRigidBody.velocity.x ,0);
        }
    }

    private void RotateInDirectionOfInput()
    {
        if (_movementInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _smoothedMovementInput);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            MyRigidBody.MoveRotation(rotation);
        }
    }

    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();
    }
}
