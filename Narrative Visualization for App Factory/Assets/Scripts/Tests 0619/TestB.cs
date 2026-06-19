using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class TestB : MonoBehaviour
{
    private Camera cam;

    private bool _boolB = false;

    public event Action OnPowerStatusChanged;

    public bool IsPowerOn
    {
        get => _boolB;
        set
        {
            if (_boolB != value)
            {
                _boolB = value;
                OnPowerStatusChanged?.Invoke(); //Notify listeners
            }
        }
    }
    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        //If there is a touchscreen and the button is currently being pressed
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            CheckTap(Touchscreen.current.primaryTouch.position.ReadValue());
        }
        //Else if, there is a mouse and left mouse button is being pressed
        else if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            CheckTap(Mouse.current.position.ReadValue());
        }
    }

    void CheckTap(Vector2 screenPos)
    {
        Vector2 worldPos = cam.ScreenToWorldPoint(screenPos);
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            IsPowerOn = true;
            Debug.Log("B is true");
        }
    }
}

