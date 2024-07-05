using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private MyPlayerInputManager _input;

    [SerializeField]
    [Range(2f, 14f)]
    private float _speed = 2;

    private Vector3 _inputDirection;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _input.onMovement += GetInputMovement;
    }

    private void OnDisable()
    {
        _input.onMovement -= GetInputMovement;
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(transform.position + _inputDirection * Time.deltaTime * _speed);

    }

    private void GetInputMovement(Vector2 axis)
    {
        if (_input == null) return;
        //* This is tied to the Camera rotation feature 
        //TODO: Adjust the input movements whenever the camera has been rotated 
        _inputDirection = new Vector3(axis.x, 0, axis.y);
    }
}
