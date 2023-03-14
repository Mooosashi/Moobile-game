using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject target;

    [Header("Parameters")]
    [SerializeField] private float rotationSpeed = 5000f;

    private Vector3 previousPosition;

    private void Update()
    {
        //if (Input.touchCount > 0 && Input.GetTouch(0).position.y >= Screen.height / 2)
        //{
        //    if (Input.GetTouch(0).phase == TouchPhase.Began)
        //    {
        //        
        //    }

        //    if (Input.GetTouch(0).phase == TouchPhase.Moved)
        //    {
        //        
        //    }
        //}

        if (Input.GetMouseButtonDown(0))
        {
            previousPosition = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 direction = previousPosition - mainCamera.ScreenToViewportPoint(Input.mousePosition);

            transform.RotateAround(target.transform.position, Vector3.up, direction.x * rotationSpeed * Time.deltaTime);

            previousPosition = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }
    }
}
