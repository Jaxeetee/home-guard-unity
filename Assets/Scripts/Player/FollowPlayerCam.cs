using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCam : MonoBehaviour
{
    [SerializeField]
    private Transform _pivot;

    [SerializeField]
    private float smoothSpeed = 0.125f;

    [SerializeField]
    private MyPlayerInputManager _input;

    [SerializeField]
    [Range(0.1f, 5f)]
    private float _influence = 0.5f;

    public Vector3 locationOffset;

    private Vector3 _desiredPosition;
    private Vector3 _smoothedPosition;
    private Vector3 _mouseDirection;
    private float _inputCamRotate;
    private bool _isPressed;


    private void OnEnable() 
    {
        _input.onMousePosition += GetInputLook;
        _input.onCamRotate += GetInputCamRotate;
    }

    private void OnDisable()
    {
        _input.onMousePosition -= GetInputLook;
        _input.onCamRotate -= GetInputCamRotate;
    }
    
    void Start()
    {

    }

    void FixedUpdate()
    {
        //! Fix issue where camera is barely adjusted on the upper right side of the screen
        _desiredPosition = (_pivot.position + (_mouseDirection * _influence)) + locationOffset;
        _smoothedPosition = Vector3.Slerp(transform.position, _desiredPosition, smoothSpeed);
        transform.position = _smoothedPosition;

        //* This feature is not in high priorities. Do this after all main features are built
        //TODO: Create a feature where whenever player presses E or Q, camera rotates around the player by 90 degrees
    }

    private void GetInputLook(Vector2 axis)
    {
        if (_input == null) return;
        Vector2 mousePos = axis;
        _mouseDirection = new Vector3(mousePos.x, 0, mousePos.y) - _pivot.position;
        _mouseDirection.Normalize();
    }

    private void GetInputCamRotate(float value)
    {
        if(_input == null) return;
        _inputCamRotate = value;
        _isPressed = _inputCamRotate == 0 ? false : true;
        Debug.Log($"inpute Cam Rotate value: {_inputCamRotate}");
    }

}
