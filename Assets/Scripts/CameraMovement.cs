using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement instance;

    public Rigidbody rb;
    public float rotationSpeed = 100f;
    public bool onCube = false;
    private bool dragging;
    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;

    public bool GetDragging() => Dragging;

    public void SetDragging(bool value) => Dragging = value;

    private bool swipeControl;

    public bool GetSwipeControl() => swipeControl;

    public void SetSwipeControl(bool value) => swipeControl = value;

    bool LeftSwipe => currentSwipe.x < 0 && currentSwipe.y > -.5f && currentSwipe.y < .5f;
    bool RightSwipe => currentSwipe.x > 0 && currentSwipe.y > -.5f && currentSwipe.y < .5f;
    bool UpLeftSwipe => currentSwipe.x < 0 && currentSwipe.y > 0;
    bool UpRightSwipe => currentSwipe.x > 0 && currentSwipe.y > 0;
    bool DownLeftSwipe => currentSwipe.x < 0 && currentSwipe.y < 0;
    bool DownRightSwipe => currentSwipe.x > 0 && currentSwipe.y < 0;

    public bool Dragging { get => dragging; set => dragging = value; }

    float distance;

    private void Awake()
    {
        instance = this;
    }
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
                Dragging = false;
            }

            else
            {
                Dragging = true;
                firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (Dragging && GetSwipeControl())
            {
                secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                //Debug.Log(secondPressPos);
                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                currentSwipe.Normalize();

                if (LeftSwipe)
                    CubeManager.instance.RotY(1);

                else if (RightSwipe)
                    CubeManager.instance.RotY(-1);

                else if (UpLeftSwipe)
                    CubeManager.instance.RotZ(-1);

                else if (UpRightSwipe)
                    CubeManager.instance.RotX(1);

                else if (DownLeftSwipe)
                    CubeManager.instance.RotX(-1);

                else if (DownRightSwipe)
                    CubeManager.instance.RotZ(1);
            }
            Dragging = false;
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
        if (Dragging && !onCube && !GetSwipeControl())
        {
            float x = Input.GetAxis("Mouse X") * rotationSpeed * Time.fixedDeltaTime;
            float y = Input.GetAxis("Mouse Y") * rotationSpeed * Time.fixedDeltaTime;

            rb.AddTorque(Vector3.down * x);
            rb.AddTorque(Vector3.right * y);
        }
    }
}