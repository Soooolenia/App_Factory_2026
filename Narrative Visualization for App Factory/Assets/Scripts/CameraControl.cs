using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private List<BoxCollider2D> boxColliders;

    private Vector3 dragOrigin;
    private void Awake()
    {
        EnhancedTouchSupport.Enable();
    }
    private void Update()
    {
        PanCamera();

        foreach (var touch in Touch.activeTouches)
        {
            //Debug.Log(touch.screenPosition + " " + touch.phase);    
        }
    }
    private void PanCamera()
    {
        //Early return, check if current active touches are zero
        if(Touch.activeTouches.Count == 0) return;

        //Save the first touch point and store it in a local variable named touch
        //touches in Input is an array of points currently touching the screen, so we access the first point with index 0
        Touch touch = Touch.activeTouches[0];
        
        if (touch.phase == TouchPhase.Began)
        {
            //Save position of point when drag starts
            dragOrigin = cam.ScreenToWorldPoint(touch.screenPosition);
        }

        if (touch.phase == TouchPhase.Moved)
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(touch.screenPosition);

            //print("origin " + dragOrigin + " newPosition " + cam.ScreenToWorldPoint(touch.screenPosition) + " =difference " + difference);

            //Move camera by drag distance
            cam.transform.position += difference;

            Vector3 closestPosition = cam.transform.position;
            float closestDistance = float.MaxValue;

            foreach (var boxColliders in boxColliders)
            {
                if (boxColliders.enabled == false) continue;
                Vector3 cameraPosition = cam.transform.position;
                Vector3 cameraClampedPosition = boxColliders.bounds.ClosestPoint(cameraPosition);
                cameraClampedPosition.z = cameraPosition.z;
                float distance = Vector3.Distance(cameraPosition, cameraClampedPosition);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPosition = cameraClampedPosition;
                }
            }

            cam.transform.position = closestPosition;
        }
    }
}
