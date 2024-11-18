using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _rotationSpeed;

    private Rigidbody2D MyRigidBody;
    private PlayerAwareness _playerAwarenessController;
    private Vector2 _targetDirection;
    private Camera _camera;

    [SerializeField]
    private float _screenBorder;

    // Start is called before the first frame update
    private void Awake()
    {
        MyRigidBody = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwareness>();
        _camera = Camera.main;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardstarget();
        SetVelocity();
    }

    private void UpdateTargetDirection()
    {
        if (_playerAwarenessController.AwareOfPlayer)
        {
            _targetDirection = _playerAwarenessController.DirectionToPlayer;
        }
        else
        {
            _targetDirection = Vector2.zero;
        }

        HandleEnemyOffScreen();
    }

    private void HandleEnemyOffScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

        if ((screenPosition.x < 0 && _targetDirection.x < 0) || (screenPosition.x > _camera.pixelWidth && _targetDirection.x > 0))
        {
            _targetDirection = new Vector2(-_targetDirection.x , _targetDirection.y);
        }

        if ((screenPosition.y < 0 && _targetDirection.y < 0) || (screenPosition.y > _camera.pixelHeight && _targetDirection.y > 0))
        {
            _targetDirection = new Vector2(_targetDirection.x, -_targetDirection.y);
        }
    }

    private void RotateTowardstarget()
    {
        if (_targetDirection == Vector2.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        MyRigidBody.SetRotation(rotation);
    }

    private void SetVelocity()
    {
        if (_targetDirection == Vector2.zero)
        {
            MyRigidBody.velocity = Vector2.zero;
        }
        else
        {
            MyRigidBody.velocity = transform.up * _speed;
        }

    }
}
