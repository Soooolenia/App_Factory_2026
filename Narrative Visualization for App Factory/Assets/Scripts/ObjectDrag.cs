using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectDrag : MonoBehaviour
{
    [Header("Drag Constraints")]
    [SerializeField] private Vector2 dragAxis = Vector2.right; // direction the object is allowed to move along
    [SerializeField] private float maxDragDistance = 2f;       // how far it can be pulled from start

    [Header("Bounce Back")]
    [SerializeField] private float bounceSpeed = 10f;
    [SerializeField] private float bounceDamping = 0.5f; // lower = more overshoot/bounciness

    private Camera cam;
    private Vector3 startPosition;
    private Vector3 offset;
    private bool dragging;

    // Used for the spring-back animation
    private Vector3 velocity;
    private bool bouncingBack;

    void Start()
    {
        cam = Camera.main;
        startPosition = transform.position;
        dragAxis = dragAxis.normalized;
    }

    void Update()
    {
        if (Touchscreen.current != null)
        {
            var touch = Touchscreen.current.primaryTouch;
            Vector2 screenPos = touch.position.ReadValue();

            if (touch.press.wasPressedThisFrame) TryStartDrag(screenPos);
            else if (touch.press.isPressed && dragging) DoDrag(screenPos);
            else if (touch.press.wasReleasedThisFrame) EndDrag();
        }
        else if (Mouse.current != null)
        {
            Vector2 screenPos = Mouse.current.position.ReadValue();

            if (Mouse.current.leftButton.wasPressedThisFrame) TryStartDrag(screenPos);
            else if (Mouse.current.leftButton.isPressed && dragging) DoDrag(screenPos);
            else if (Mouse.current.leftButton.wasReleasedThisFrame) EndDrag();
        }

        if (bouncingBack)
        {
            BounceTowardsStart();
        }
    }

    private void TryStartDrag(Vector2 screenPos)
    {
        Vector2 worldPos = cam.ScreenToWorldPoint(screenPos);
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            dragging = true;
            bouncingBack = false;
            offset = transform.position - GetWorldPosition(screenPos);
        }
    }

    private void DoDrag(Vector2 screenPos)
    {
        Vector3 targetPos = GetWorldPosition(screenPos) + offset;

        // Project the movement onto the allowed axis only
        Vector3 displacement = targetPos - startPosition;
        float distanceAlongAxis = Vector3.Dot(displacement, dragAxis);

        // Clamp how far along that axis it can go
        distanceAlongAxis = Mathf.Clamp(distanceAlongAxis, 0f, maxDragDistance);

        transform.position = startPosition + (Vector3)(dragAxis * distanceAlongAxis);
    }

    private void EndDrag()
    {
        dragging = false;
        bouncingBack = true;
        velocity = Vector3.zero;
    }

    private void BounceTowardsStart()
    {
        // Simple spring-damper simulation (overshoots then settles = "bounce")
        Vector3 toStart = startPosition - transform.position;
        Vector3 springForce = toStart * bounceSpeed;
        velocity += springForce * Time.deltaTime;
        velocity *= (1f - bounceDamping * Time.deltaTime);

        transform.position += velocity * Time.deltaTime;

        // Stop once it's settled close enough to start
        if (toStart.sqrMagnitude < 0.0001f && velocity.sqrMagnitude < 0.0001f)
        {
            transform.position = startPosition;
            bouncingBack = false;
        }
    }

    private Vector3 GetWorldPosition(Vector2 screenPos)
    {
        Vector3 screenPoint = screenPos;
        screenPoint.z = cam.WorldToScreenPoint(transform.position).z;
        return cam.ScreenToWorldPoint(screenPoint);
    }
}
