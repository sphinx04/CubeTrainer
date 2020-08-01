using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public CubeManager manager;
    public Rigidbody rb;
    public float rotationSpeed = 100f;
    public bool onCube = false;
    private bool dragging;
    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;

    public bool GetDragging() => dragging;

    public void SetDragging(bool value) => dragging = value;

    private bool swipeControl;

    public bool GetSwipeControl() => swipeControl;

    public void SetSwipeControl(bool value) => swipeControl = value;

    bool LeftSwipe => currentSwipe.x < 0 && currentSwipe.y > -.5f && currentSwipe.y < .5f;
    bool RightSwipe => currentSwipe.x > 0 && currentSwipe.y > -.5f && currentSwipe.y < .5f;
    bool UpLeftSwipe => currentSwipe.x < 0 && currentSwipe.y > 0;
    bool UpRightSwipe => currentSwipe.x > 0 && currentSwipe.y > 0;
    bool DownLeftSwipe => currentSwipe.x < 0 && currentSwipe.y < 0;
    bool DownRightSwipe => currentSwipe.x > 0 && currentSwipe.y < 0;


    float distance;

    private void Start()
    {
        SetSwipeControl(PlayerPrefs.GetInt("Free Rotation", 1) == 1);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.touchCount < 2)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                onCube = true;
                dragging = false;
            }

            else
            {
                dragging = true;
                firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (dragging && GetSwipeControl())
            {
                secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                //Debug.Log(secondPressPos);
                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                currentSwipe.Normalize();

                if (LeftSwipe)
                    manager.RotY(1);

                else if (RightSwipe)
                    manager.RotY(-1);

                else if (UpLeftSwipe)
                    manager.RotZ(-1);

                else if (UpRightSwipe)
                    manager.RotX(1);

                else if (DownLeftSwipe)
                    manager.RotX(-1);

                else if (DownRightSwipe)
                    manager.RotZ(1);
            }
            dragging = false;
            onCube = false;
        }

        if (Input.touchCount > 1)
        {
            Vector2 touch0, touch1;
            touch0 = Input.GetTouch(0).position;
            touch1 = Input.GetTouch(1).position;
            distance = Vector2.Distance(touch0, touch1);

        }
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(110, 110, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 100;
        style.normal.textColor = new Color(0.7f, 0.7f, 0.0f, 1.0f);
        string text = distance.ToString();
        GUI.Label(rect, text, style);
    }

    private void FixedUpdate()
    {
        if (dragging && !onCube && !GetSwipeControl())
        {
            float x = Input.GetAxis("Mouse X") * rotationSpeed * Time.fixedDeltaTime;
            float y = Input.GetAxis("Mouse Y") * rotationSpeed * Time.fixedDeltaTime;

            rb.AddTorque(Vector3.down * x);
            rb.AddTorque(Vector3.right * y);
        }
    }
}