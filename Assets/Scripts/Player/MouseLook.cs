using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    
    [SerializeField]
    private MyPlayerInputManager _input;

    [SerializeField]
    private LayerMask _groundMask;
    
    private Camera _camera;

    private Vector2 _mousePos;

    private void Awake()
    {
    }

    void Start()
    {
        _camera = Camera.main;
    }

    private void OnEnable() 
    {
        _input.onMousePosition += GetInputLook;
    }

    private void OnDisable()
    {
        _input.onMousePosition -= GetInputLook;
    }

    private void Update()
    {
        Aim();
    }

    private void Aim()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            var direction = position - transform.position;
            direction.y = 0;
            transform.forward = direction;
        }
    }

    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = _camera.ScreenPointToRay(_mousePos);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, _groundMask))
        {
            // The Raycast hit something, return with the position.
            return (success: true, position: hitInfo.point);
        }
        else
        {
            // The Raycast did not hit anything.
            return (success: false, position: Vector3.zero);
        }
    }

    private void GetInputLook(Vector2 axis)
    {
        if (_input == null) return;
        _mousePos = axis;
    }

}
