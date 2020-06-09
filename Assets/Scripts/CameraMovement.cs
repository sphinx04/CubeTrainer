using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public CubeManager manager;
    public Rigidbody rb;
    public float rotationSpeed = 100f;
    public bool dragging;
    public bool onCube = false;
    public bool swipeControl;
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    //private void OnMouseDrag()
    //{
    //    dragging = true;
    //    onCube = true;
    //}

    private void Start()
    {
        swipeControl = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100))
            {
                onCube = true;
                dragging = false;
                //Debug.Log(hit.collider.gameObject.name + "ray");
            }

            else 
            {
                dragging = true;
                firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                //Debug.Log(firstPressPos);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (dragging && swipeControl)
            {
                secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                //Debug.Log(secondPressPos);
                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                currentSwipe.Normalize();
                if (LeftSwipe(currentSwipe))
                {
                    manager.RotY(1);
                }
                else if (RightSwipe(currentSwipe))
                {
                    manager.RotY(-1);
                }
                else if (UpLeftSwipe(currentSwipe))
                {
                    manager.RotZ(-1);
                }
                else if (UpRightSwipe(currentSwipe))
                {
                    manager.RotX(1);
                }
                else if (DownLeftSwipe(currentSwipe))
                {
                    manager.RotX(-1);
                }
                else if (DownRightSwipe(currentSwipe))
                {
                    manager.RotZ(1);
                }

            }
            dragging = false;
            onCube = false;
        }
    }

    public void SetDragging(bool dragging)
    {
        this.dragging = dragging;
    }

    bool LeftSwipe(Vector2 swipe)
    {
        return currentSwipe.x < 0 && currentSwipe.y > -.5f && currentSwipe.y < .5f;
    }

    bool RightSwipe(Vector2 swipe)
    {
        return currentSwipe.x > 0 && currentSwipe.y > -.5f && currentSwipe.y < .5f;
    }
    bool UpLeftSwipe(Vector2 swipe)
    {
        return currentSwipe.x < 0 && currentSwipe.y > 0;
    }
    bool UpRightSwipe(Vector2 swipe)
    {
        return currentSwipe.x > 0 && currentSwipe.y > 0;
    }

    bool DownLeftSwipe(Vector2 swipe)
    {
        return currentSwipe.x < 0 && currentSwipe.y < 0;
    }
    bool DownRightSwipe(Vector2 swipe)
    {
        return currentSwipe.x > 0 && currentSwipe.y < 0;
    }


    public void SetSwipeControl(bool swipeControl)
    {
        this.swipeControl = swipeControl;
    }

    private void FixedUpdate()
    {
        if (dragging && !onCube && !swipeControl)
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
