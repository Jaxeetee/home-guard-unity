using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[DefaultExecutionOrder(-1)]
public class MyPlayerInputManager : MonoBehaviour
{
    
    private PlayerInputs _inputs;

    public event Action<Vector2> onMovement;
    public event Action<Vector2> onMousePosition;
    public event Action<float> onShoot;

    public event Action<float> onCamRotate;

    // Start is called before the first frame update
    private void Awake()
    {
    }

    private void Start()
    {
        _inputs = new PlayerInputs();

        _inputs.main.Enable();
        MainControls();
    }

    private void MainControls()
    {
        #region --== started ==--
        _inputs.main.rotate_cam.performed += ctx =>
        {
            var value = ctx.ReadValue<float>();
            onCamRotate?.Invoke(value);
        };
        #endregion

        #region --== performed ==--
        _inputs.main.movement.performed += ctx =>
        {
            Vector2 axis = ctx.ReadValue<Vector2>();
            onMovement?.Invoke(axis);
        };

        _inputs.main.look.performed += ctx =>
        {
            Vector2 axis = ctx.ReadValue<Vector2>();
            onMousePosition?.Invoke(axis);
        };

        _inputs.main.shoot.performed += ctx =>
        {
            float value = ctx.ReadValue<float>();
            onShoot?.Invoke(value);
        };

        #endregion

        #region --== canceled ==--
        _inputs.main.movement.canceled += ctx =>
        {
            Vector2 axis = ctx.ReadValue<Vector2>();
            onMovement?.Invoke(axis);
        };

        _inputs.main.look.canceled += ctx =>
        {
            Vector2 axis = ctx.ReadValue<Vector2>();
            onMousePosition?.Invoke(axis);
        };

        _inputs.main.shoot.canceled += ctx =>
        {
            float value = ctx.ReadValue<float>();
            onShoot?.Invoke(value);
        };

        _inputs.main.rotate_cam.canceled += ctx =>
        {
            var value = ctx.ReadValue<float>();
            onCamRotate?.Invoke(value);
        };
        #endregion
    }

}
