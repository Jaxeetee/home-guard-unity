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
    public event Action<float> onCamRotate;
    public event Action onShoot;
    public event Action onReload;

    private void Start()
    {
        _inputs = new PlayerInputs();

        _inputs.main.Enable();
        MainControls();
    }

    private void MainControls()
    {
        #region --== started ==--
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
            onShoot?.Invoke();
        };

        _inputs.main.rotate_cam.performed += ctx =>
        {
            float value = ctx.ReadValue<float>();
            onCamRotate?.Invoke(value);
        };

        _inputs.main.reload.performed += ctx =>
        {
            onReload?.Invoke(); 
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

        #endregion
    }

}
