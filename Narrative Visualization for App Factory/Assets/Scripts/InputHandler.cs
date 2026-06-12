using UnityEngine;
using System;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.Events;
public class InputHandler : MonoBehaviour
{
    [SerializeField] private UnityEvent _clicked;

    private InputProvider _inputProvider;
    private Collider2D _collider;
    private void Awake()
    {
        _inputProvider = GetComponent<InputProvider>();

        _collider = GetComponent<Collider2D>();

        if(_inputProvider != null)
        {
            _inputProvider.Clicked += OnClicked;
        }
    }

    private void OnClicked()
    {
        _clicked?.Invoke();
    }
}
