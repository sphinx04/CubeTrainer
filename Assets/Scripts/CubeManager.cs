using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public GameObject CubePievePref;
    List<GameObject> AllCubePieces = new List<GameObject>();
    GameObject CubeCenterPiece;
    bool canRotate = true;
    Quaternion to;
    bool turnToDefault = false;


    #region Side Definition

    List<GameObject> UpPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.y) == 1);
        }
    }
    List<GameObject> MidEPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.y) == 0);
        }
    }
    List<GameObject> DownPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.y) == -1);
        }
    }
    List<GameObject> FrontPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.x) == 1);
        }
    }
    List<GameObject> MidSPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.x) == 0);
        }
    }
    List<GameObject> BackPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.x) == -1);
        }
    }
    List<GameObject> LeftPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.z) == -1);
        }
    }
    List<GameObject> MidMPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.z) == 0);
        }
    }
    List<GameObject> RightPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.z) == 1);
        }
    }

    #endregion

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            AllCubePieces.Add(transform.GetChild(i).gameObject);
        }
        CubeCenterPiece = AllCubePieces[13];
        to = transform.rotation;
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.rotation.eulerAngles, to.eulerAngles);

        if (Mathf.Abs(dist) > 2f && turnToDefault)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, to, .1f);
            GetComponent<CameraMovement>().dragging = false;
        }
        else
        {
            turnToDefault = false;
        }
    }

    public void TurnToDefault()
    {
        turnToDefault = true;
    }

    IEnumerator RotateSide(List<GameObject> pieces, Vector3 rotationVec, int count = 1, int speed = 5)
    {
        canRotate = false;
        int angle = 0;

        while (angle < 90 * count)
        {
            foreach (GameObject go in pieces)
            {
                go.transform.RotateAround(CubeCenterPiece.transform.position + transform.parent.position, transform.rotation * rotationVec, speed);
            }
            angle += speed;
            yield return null;
        }
        canRotate = true;
    }

    public IEnumerator TurnFromScramble(string[] sides)
    {
        foreach (string side in sides)
        {
            TurnSide(side);
            yield return new WaitUntil(() => canRotate);

            //canRotate = true;
        }
    }

    #region Side Rotations

    public void RotUp(int dir = 1)
    {
        if (canRotate)
            StartCoroutine(RotateSide(UpPieces, new Vector3(0, 1 * dir, 0), Mathf.Abs(dir)));
    }
    public void RotMidE(int dir = 1)
    {
        if (canRotate)
            StartCoroutine(RotateSide(MidEPieces, new Vector3(0, -1 * dir, 0), Mathf.Abs(dir)));
    }
    public void RotDown(int dir = 1)
    {
        if (canRotate)
            StartCoroutine(RotateSide(DownPieces, new Vector3(0, -1 * dir, 0), Mathf.Abs(dir)));
    }
    public void RotLeft(int dir = 1)
    {
        if (canRotate)
            StartCoroutine(RotateSide(LeftPieces, new Vector3(0, 0, -1 * dir), Mathf.Abs(dir)));
    }
    public void RotMidM(int dir = 1)
    {
        if (canRotate)
            StartCoroutine(RotateSide(MidMPieces, new Vector3(0, 0, -1 * dir), Mathf.Abs(dir)));
    }
    public void RotRight(int dir = 1)
    {
        if (canRotate)
            StartCoroutine(RotateSide(RightPieces, new Vector3(0, 0, 1 * dir), Mathf.Abs(dir)));
    }
    public void RotFront(int dir = 1)
    {
        if (canRotate)
            StartCoroutine(RotateSide(FrontPieces, new Vector3(1 * dir, 0, 0), Mathf.Abs(dir)));
    }
    public void RotMidS(int dir = 1)
    {
        if (canRotate)
            StartCoroutine(RotateSide(MidSPieces, new Vector3(1 * dir, 0, 0), Mathf.Abs(dir)));
    }
    public void RotBack(int dir = 1)
    {
        if (canRotate)
            StartCoroutine(RotateSide(BackPieces, new Vector3(-1 * dir, 0, 0), Mathf.Abs(dir)));
    }

    public void RotX(int dir = 1)
    {
        if (canRotate)
            StartCoroutine(RotateSide(AllCubePieces, new Vector3(0, 0, 1 * dir), Mathf.Abs(dir)));
    }
    public void RotY(int dir = 1)
    {
        if (canRotate)
            StartCoroutine(RotateSide(AllCubePieces, new Vector3(0, 1 * dir, 0), Mathf.Abs(dir)));
    }
    public void RotZ(int dir = 1)
    {
        if (canRotate)
            StartCoroutine(RotateSide(AllCubePieces, new Vector3(1 * dir, 0, 0), Mathf.Abs(dir)));
    }

#endregion

    public void TurnSide(string side)
    {
        switch (side)
        {

            case "U":
                Debug.Log(side);
                RotUp();
                break;
            case "U\'":
                Debug.Log(side);
                RotUp(-1);
                break;
            case "U2":
                Debug.Log(side);
                RotUp(2);
                break;

            case "D":
                Debug.Log(side);
                RotDown();
                break;
            case "D\'":
                Debug.Log(side);
                RotDown(-1);
                break;
            case "D2":
                Debug.Log(side);
                RotDown(2);
                break;

            case "L":
                Debug.Log(side);
                RotLeft();
                break;
            case "L\'":
                Debug.Log(side);
                RotLeft(-1);
                break;
            case "L2":
                Debug.Log(side);
                RotLeft(2);
                break;

            case "R":
                Debug.Log(side);
                RotRight();
                break;
            case "R\'":
                Debug.Log(side);
                RotRight(-1);
                break;
            case "R2":
                Debug.Log(side);
                RotRight(2);
                break;

            case "F":
                Debug.Log(side);
                RotFront();
                break;
            case "F\'":
                Debug.Log(side);
                RotFront(-1);
                break;
            case "F2":
                Debug.Log(side);
                RotFront(2);
                break;

            case "B":
                Debug.Log(side);
                RotBack();
                break;
            case "B\'":
                Debug.Log(side);
                RotBack(-1);
                break;
            case "B2":
                Debug.Log(side);
                RotBack(2);
                break;

            case "E":
                Debug.Log(side);
                RotMidE();
                break;
            case "E\'":
                Debug.Log(side);
                RotMidE(-1);
                break;
            case "E2":
                Debug.Log(side);
                RotMidE(2);
                break;

            case "M":
                Debug.Log(side);
                RotMidM();
                break;
            case "M\'":
                Debug.Log(side);
                RotMidM(-1);
                break;
            case "M2":
                Debug.Log(side);
                RotMidM(2);
                break;

            case "S":
                Debug.Log(side);
                RotMidS();
                break;
            case "S\'":
                Debug.Log(side);
                RotMidS(-1);
                break;
            case "S2":
                Debug.Log(side);
                RotMidS(2);
                break;

            case "X":
                Debug.Log(side);
                RotX();
                break;
            case "X\'":
                Debug.Log(side);
                RotX(-1);
                break;
            case "X2":
                Debug.Log(side);
                RotX(2);
                break;

            case "Y":
                Debug.Log(side);
                RotY();
                break;
            case "Y\'":
                Debug.Log(side);
                RotY(-1);
                break;
            case "Y2":
                Debug.Log(side);
                RotY(2);
                break;

            case "Z":
                Debug.Log(side);
                RotZ();
                break;
            case "Z\'":
                Debug.Log(side);
                RotZ(-1);
                break;
            case "Z2":
                Debug.Log(side);
                RotZ(2);
                break;

            default:
                Debug.Log("DEFAULT " + side);
                break;
        }
    }
}
