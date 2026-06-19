using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class TestA : MonoBehaviour
{
    private Camera cam;

    private bool _boolA = false;

    public event Action OnKeyStatusChanged;

    public bool HasKey
    {
        get => _boolA;
        set
        {
            if (_boolA != value)
            {
                _boolA = value;
                OnKeyStatusChanged?.Invoke(); 
            }
        }
    }
    void Start()
    {
        //Reference main camera upon start up (I think for the raycast thing)
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
            HasKey = true;
            Debug.Log("A is true");
        }
    }
}
