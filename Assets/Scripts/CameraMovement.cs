using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 0.2f;
    private Touch touch;
    private Quaternion localRotation;

    private void Start()
    {
        localRotation.x = 135f / speed;
        localRotation.y = 30f / speed;
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Moved)
            {
                localRotation.x += touch.deltaPosition.x;
                localRotation.y += -touch.deltaPosition.y;
                localRotation.y = Mathf.Clamp(localRotation.y, -45 / speed, 45 / speed);
                transform.parent.rotation = Quaternion.Euler
                    (localRotation.y * speed, localRotation.x * speed, 0f);
            }
        }
    }












    //Vector3 localRotation;
    //bool cameraDisabled = false;

    //private void LateUpdate()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        if (!cameraDisabled)
    //        {
    //            localRotation.x += Input.GetAxis("Mouse X") * 1;
    //            localRotation.y += Input.GetAxis("Mouse Y") * -1;
    //            localRotation.y = Mathf.Clamp(localRotation.y, -90, 90);
    //        }
    //        Quaternion qt = Quaternion.Euler(localRotation.y, localRotation.x, 0);
    //        transform.parent.rotation = qt;// Quaternion.Lerp(transform.parent.rotation, qt, Time.deltaTime * 15);
    //        }
    //}
}
