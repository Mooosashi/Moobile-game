using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private TouchControls touchControls;

    public delegate void StartTouchEvent(Vector2 position);
    public event StartTouchEvent OnStartTouch;
    public delegate void EndTouchEvent(Vector2 position);
    public event EndTouchEvent OnEndTouch;

    [HideInInspector] public Vector2 swipeAxis;

    [Header("References")]
    [SerializeField] public CameraRotationAndZoom cameraRotationAndZoom;


    private void Awake()
    {
        touchControls = new TouchControls();
    }


    private void OnEnable()
    {
        touchControls.Enable();
    }

    private void OnDisable()
    {
        touchControls.Disable();
    }

    private void Start()
    {
        touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
        touchControls.Touch.TouchPress.canceled += ctx => EndTouch(ctx);

        touchControls.Touch.TouchAxis.performed += ctx => swipeAxis = ctx.ReadValue<Vector2>();
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        if (OnStartTouch != null)
            OnStartTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>()); // If no script is subscribed then does not trigger event
    }

    private void EndTouch(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null)
            OnEndTouch(touchControls.Touch.TouchPosition.ReadValue<Vector2>());
    }


}
