using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] MyPlayerInputManager _input;
    [SerializeField] LayerMask _groundMask;
    [SerializeField] LayerMask _collisions;
    Camera _camera;
    Vector2 _mousePos;

    void Start()
    {
        _camera = Camera.main;
    }

    void OnEnable() 
    {
        _input.onMousePosition += GetInputLook;
    }

    void OnDisable()
    {
        _input.onMousePosition -= GetInputLook;
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.red);
        Aim();
    }

    void Aim()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            var direction = position - transform.position;
            direction.y = 0;
            transform.forward = direction;
        }
    }

    (bool success, Vector3 position) GetMousePosition()
    {
        var ray = _camera.ScreenPointToRay(_mousePos);
        var forwardRay = new Ray(transform.position, transform.forward);

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

    void GetInputLook(Vector2 axis)
    {
        if (_input == null) return;
        _mousePos = axis;
    }

}
