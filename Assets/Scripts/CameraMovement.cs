using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float rotationSpeed = 100f;
    public bool dragging = false;
    public bool onCube = false;

    private void OnMouseDrag()
    {
        dragging = false;
        onCube = true;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !onCube)
        {
            dragging = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
            onCube = false;
        }
    }

    private void FixedUpdate()
    {
        if (dragging)
        {
            float x = Input.GetAxis("Mouse X") * rotationSpeed * Time.fixedDeltaTime;
            float y = Input.GetAxis("Mouse Y") * rotationSpeed * Time.fixedDeltaTime;

            rb.AddTorque(Vector3.down * x);
            rb.AddTorque(Vector3.right * y);
        }
    }
}






















    //public float speed = 0.2f;
    //private Touch touch;
    //private Quaternion localRotation;

    //private void Start()
    //{
    //    localRotation.x = 225f / speed;
    //    localRotation.y = 30f / speed;
    //}

    //private void Update()
    //{
    //    if(Input.touchCount > 0)
    //    {
    //        touch = Input.GetTouch(0);

    //        if(touch.phase == TouchPhase.Moved)
    //        {
    //            localRotation.x += touch.deltaPosition.x;
    //            localRotation.y += -touch.deltaPosition.y;

    //            //localRotation.y = Mathf.Clamp(localRotation.y, -45 / speed, 45 / speed);
    //            //transform.rotation = Quaternion.Euler(localRotation.y * speed, localRotation.x * speed, 0f);
    //            transform.Rotate(new Vector3(localRotation.y, localRotation.x, -localRotation.y), .05f);
    //        }
    //    }
    //}
