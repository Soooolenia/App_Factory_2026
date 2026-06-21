using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectDrag : MonoBehaviour
{
    private Vector3 offset;
    private bool dragging;

    private void Update()
    {
        if (Pointer.current == null) return;

        Vector2 screenPos = Pointer.current.position.ReadValue();
        bool pressed = Pointer.current.press.isPressed;

        if (pressed && !dragging)
        {
            //Check if pressed down
            Ray ray = Camera.main.ScreenPointToRay(screenPos);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                dragging = true;
                offset = transform.position - GetWorldPosition(screenPos);
            }
        }
        else if (!pressed)
        {
            dragging = false;
        }

        if (dragging)
        {
            transform.position = GetWorldPosition(screenPos) + offset;
        }
    }

    private Vector3 GetWorldPosition(Vector2 screenPos)
    {
        Vector3 screenPoint = screenPos;
        screenPoint.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(screenPoint);
    }
}
