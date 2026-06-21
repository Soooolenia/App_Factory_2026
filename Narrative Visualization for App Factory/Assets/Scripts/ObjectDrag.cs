using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectDrag : MonoBehaviour
{
    [Header("Drag Threshold")]
    [SerializeField] private float dragThreshold = 0.1f;

    [Header("Drag Constraints")]
    //Drag line
    [SerializeField] private Vector2 dragAxis = Vector2.right; 
    [SerializeField] private float maxDragDistance = 2f;       

    [Header("Bounce Back")]
    [SerializeField] private float bounceSpeed = 10f;
    [SerializeField] private float bounceDamping = 0.5f; 

    private Camera cam;
    private Vector3 startPosition;
    private Vector3 offset;
    private bool dragging;

    public static bool IsDragging = false;

    //Spring mechanic, not sure how it works
    private Vector3 velocity;
    private bool bouncingBack;

    private Vector2 pressScreenPos;
    private bool pendingDrag;

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
            else if (touch.press.isPressed && (pendingDrag || dragging)) DoDrag(screenPos);
            else if (touch.press.wasReleasedThisFrame) EndDrag();
        }
        else if (Mouse.current != null)
        {
            Vector2 screenPos = Mouse.current.position.ReadValue();

            if (Mouse.current.leftButton.wasPressedThisFrame) TryStartDrag(screenPos);
            else if (Mouse.current.leftButton.isPressed && (pendingDrag || dragging)) DoDrag(screenPos);
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
            pendingDrag = true;
            bouncingBack = false;
            pressScreenPos = screenPos;
            offset = transform.position - GetWorldPosition(screenPos);
        }
    }

    private void DoDrag(Vector2 screenPos)
    {
        if (pendingDrag)
        {
            //Check drag threshold
            float moveDist = Vector2.Distance(cam.ScreenToWorldPoint(screenPos), cam.ScreenToWorldPoint(pressScreenPos));
            if (moveDist < dragThreshold) return; 

            //Passed
            pendingDrag = false;
            dragging = true;
            IsDragging = true;
        }

        if (!dragging) return;

        Vector3 targetPos = GetWorldPosition(screenPos) + offset;
        Vector3 displacement = targetPos - startPosition;
        float distanceAlongAxis = Vector3.Dot(displacement, dragAxis);
        distanceAlongAxis = Mathf.Clamp(distanceAlongAxis, 0f, maxDragDistance);
        transform.position = startPosition + (Vector3)(dragAxis * distanceAlongAxis);
    }

    private void EndDrag()
    {
        pendingDrag = false;
        dragging = false;
        IsDragging = false;
        bouncingBack = true;
        velocity = Vector3.zero;
    }

    private void BounceTowardsStart()
    {
        //Spring-damper simulation (Not sure how this works :( )
        Vector3 toStart = startPosition - transform.position;
        Vector3 springForce = toStart * bounceSpeed;
        velocity += springForce * Time.deltaTime;
        velocity *= (1f - bounceDamping * Time.deltaTime);

        transform.position += velocity * Time.deltaTime;

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

    //In case object gets disabled
    private void OnDisable()
    {
        if (dragging)
        {
            dragging = false;
            IsDragging = false;
        }
    }
}
