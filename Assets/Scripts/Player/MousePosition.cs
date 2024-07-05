using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    [SerializeField]
    private MyPlayerInputManager _input;

    [SerializeField]
    private LayerMask groundMask;

    [SerializeField]
    private float _maxDistanceAwayFromPlayer = 5f;

    [SerializeField]
    private Transform _anchorPosition;

    private Vector3 _worldPosition;
    private Vector3 _mouseInputPosition;

    private Plane _plane = new Plane(Vector3.up, 0);

    private Camera _cam;
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
    }

    void OnEnable()
    {
        _input.onMousePosition += GetInputMousePosition;
    }

    void OnDisable()
    {
        _input.onMousePosition -= GetInputMousePosition;
    }



    // Update is called once per frame
    void Update()
    {
        float distance;

        Ray ray = _cam.ScreenPointToRay(_mouseInputPosition);
        if (_plane.Raycast(ray, out distance))
        {
            Vector3 worldPosition = ray.GetPoint(distance);

            // Calculate direction vector and clamp magnitude
            Vector3 direction = worldPosition - _anchorPosition.position;
            float clampedMagnitude = Mathf.Clamp(direction.magnitude, 0, _maxDistanceAwayFromPlayer);

            // Apply limited direction to anchor position
            Vector3 limitedPosition = _anchorPosition.position + direction.normalized * clampedMagnitude;

            transform.position = limitedPosition;
        } 
    }

    private void GetInputMousePosition(Vector2 coordinate)
    {
        _mouseInputPosition = new Vector3(coordinate.x, coordinate.y, 0);
    }

}
