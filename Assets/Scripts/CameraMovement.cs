using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;

    [Header("Zoom")]
    [SerializeField] private float zoomMax;
    [SerializeField] private float zoomMin;

    [Header("Movement")]
    [SerializeField] private float moveLeft;
    [SerializeField] private float moveRight;
    [SerializeField] private float moveUp;
    [SerializeField] private float moveDown;

    private bool moveAllowed;
    private Vector3 touchPos;
    private void Update()
    {
        if(Input.touchCount > 0)
        {
            if(Input.touchCount == 2)
            {
                CameraZoom();
            }
            else
            {
                CameraMove();
            }
        }
    }

    private void CameraMove()
    {
        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case (TouchPhase.Began):
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    moveAllowed = false;
                }
                else
                {
                    moveAllowed = true;
                }

                touchPos = cam.ScreenToWorldPoint(touch.position);
                break;

            case (TouchPhase.Moved):

                if (moveAllowed)
                {
                    Vector3 direction = touchPos - cam.ScreenToWorldPoint(touch.position);
                    cam.transform.position += direction;

                    cam.transform.position = new Vector3
                        (
                        Mathf.Clamp(cam.transform.position.x, moveLeft, moveRight),
                        Mathf.Clamp(cam.transform.position.y, moveDown, moveUp),
                        transform.position.z

                        ) ;
                }
                break;

        }
    }

    private void CameraZoom()
    {
        Touch touchZero = Input.GetTouch(0);
        Touch touchOne = Input.GetTouch(1);

        if(EventSystem.current.IsPointerOverGameObject(touchOne.fingerId) || EventSystem.current.IsPointerOverGameObject(touchZero.fingerId))
        {
            return;
        }

        Vector2 touchZeroLastPos = touchZero.position - touchZero.deltaPosition;
        Vector2 touchOneLastPos = touchOne.position - touchOne.deltaPosition;

        float distTouch = (touchZeroLastPos - touchOneLastPos).magnitude;
        float currentDistTouch = (touchZero.position - touchOne.position).magnitude;

        float diff = (currentDistTouch - distTouch) * 0.01f;

        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - diff, zoomMin, zoomMax);
    }

}
