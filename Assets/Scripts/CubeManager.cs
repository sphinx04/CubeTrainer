﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public GameObject CubePievePref;
    List<GameObject> AllCubePieces = new List<GameObject>();
    GameObject CubeCenterPiece;
    bool canRotate = true;
    Quaternion defaultRotation;
    bool turnToDefault;

    public int defaultRotationSpeed = 10;
    int currentRotationSpeed;


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
        GameObject piece;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.tag == "Piece")
            {
                piece = transform.GetChild(i).gameObject;
                AllCubePieces.Add(piece);
                piece.GetComponent<CubePieceScr>().SetColor((int)piece.transform.localPosition.x, (int)piece.transform.localPosition.y, (int)piece.transform.localPosition.z);

            }
        }
        CubeCenterPiece = AllCubePieces[13];
        defaultRotation = transform.rotation;
        currentRotationSpeed = defaultRotationSpeed;
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.rotation.eulerAngles, defaultRotation.eulerAngles);

        if (canRotate)
        {
            //Debug.Log("defaultRotationSpeed = currentRotationSpeed");
            defaultRotationSpeed = currentRotationSpeed;
        }
        if (Mathf.Abs(dist) > 2f && turnToDefault && canRotate)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, defaultRotation, .1f);
            GetComponent<CameraMovement>().dragging = false;
        }
        else
        {
            turnToDefault = false;
        }
    }

    public bool CheckSolved()
    {
        GameObject item = AllCubePieces[0];
        bool solved = true;
        float EPSILON = 0.1f;
        for (int i = 1; i < 27; i++)
        {
            if (i == 4)
            {
                solved = solved && System.Math.Abs(item.transform.rotation.eulerAngles.x - AllCubePieces[i].transform.rotation.eulerAngles.x) < EPSILON &&
                                   System.Math.Abs(item.transform.rotation.eulerAngles.y - AllCubePieces[i].transform.rotation.eulerAngles.y) < EPSILON;
            }
            else if (i == 10)
            {
                solved = solved && System.Math.Abs(item.transform.rotation.eulerAngles.x - AllCubePieces[i].transform.rotation.eulerAngles.x) < EPSILON &&
                                   System.Math.Abs(item.transform.rotation.eulerAngles.z - AllCubePieces[i].transform.rotation.eulerAngles.z) < EPSILON;
            }
            else if (i == 12)
            {
                solved = solved && System.Math.Abs(item.transform.rotation.eulerAngles.z - AllCubePieces[i].transform.rotation.eulerAngles.z) < EPSILON &&
                                   System.Math.Abs(item.transform.rotation.eulerAngles.y - AllCubePieces[i].transform.rotation.eulerAngles.y) < EPSILON;
            }
            else if (i == 14)
            {
                solved = solved && System.Math.Abs(item.transform.rotation.eulerAngles.z - AllCubePieces[i].transform.rotation.eulerAngles.z) < EPSILON &&
                                   System.Math.Abs(item.transform.rotation.eulerAngles.y - AllCubePieces[i].transform.rotation.eulerAngles.y) < EPSILON;
            }
            else if (i == 16)
            {
                solved = solved && System.Math.Abs(item.transform.rotation.eulerAngles.x - AllCubePieces[i].transform.rotation.eulerAngles.x) < EPSILON &&
                                   System.Math.Abs(item.transform.rotation.eulerAngles.z - AllCubePieces[i].transform.rotation.eulerAngles.z) < EPSILON;
            }
            else if (i == 22)
                solved = solved && System.Math.Abs(item.transform.rotation.eulerAngles.x - AllCubePieces[i].transform.rotation.eulerAngles.x) < EPSILON &&
                                   System.Math.Abs(item.transform.rotation.eulerAngles.y - AllCubePieces[i].transform.rotation.eulerAngles.y) < EPSILON;
            else
            {
                solved = solved && System.Math.Abs(item.transform.rotation.eulerAngles.x - AllCubePieces[i].transform.rotation.eulerAngles.x) < EPSILON &&
                                   System.Math.Abs(item.transform.rotation.eulerAngles.y - AllCubePieces[i].transform.rotation.eulerAngles.y) < EPSILON &&
                                   System.Math.Abs(item.transform.rotation.eulerAngles.z - AllCubePieces[i].transform.rotation.eulerAngles.z) < EPSILON;
            }
        }
        //Debug.Log(item.transform.rotation);
        //Debug.Log(solved);
        //Debug.Log(AllCubePieces[4].transform.rotation.eulerAngles.x + ", " + AllCubePieces[4].transform.rotation.eulerAngles.y + ", " + AllCubePieces[4].transform.rotation.eulerAngles.z);
        return solved;
    }
    public void SetCurrentRotationSpeed(int speed)
    {
        currentRotationSpeed = speed;
    }
    public void SetDefaultRotationSpeed(float speed)
    {
        switch ((int)speed)
        {
            case 0:
                currentRotationSpeed = 2;
                break;
            case 1:
                currentRotationSpeed = 5;
                break;
            case 2:
                currentRotationSpeed = 10;
                break;
            case 3:
                currentRotationSpeed = 15;
                break;
            case 4:
                currentRotationSpeed = 30;
                break;
            case 90:
                currentRotationSpeed = 90;
                break;
        }

    }

    public void TurnToDefault()
    {
        StartCoroutine(WaitUntilCanRotate());
    }

    public void SetCanRotate(bool canRotate)
    {
        this.canRotate = canRotate;
    }

    public void Reset()
    {
        //UnityEditor.PrefabUtility.ResetToPrefabState(this.gameObject);
        //UnityEditor.PrefabUtility.RevertObjectOverride(this.gameObject, );
    }

    IEnumerator RotateSide(List<GameObject> pieces, Vector3 rotationVec, int count = 1)
    {
        SetCanRotate(false);
        int angle = 0;    

        while (angle < 90 * count)
        {
            foreach (GameObject go in pieces)
            {
                go.transform.RotateAround(CubeCenterPiece.transform.position, transform.rotation * rotationVec, defaultRotationSpeed);
            }
            angle += defaultRotationSpeed;
            yield return null;
        }
        SetCanRotate(true);

        if (CheckSolved())
        {
            Debug.Log("SOLVED");
        }
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

    public void TurnFromDefaultScramble(string[] sides)
    {
        foreach (string side in sides)
        {
            TurnSide(side);
        }
    }

    IEnumerator WaitUntilCanRotate()
    {
        yield return new WaitUntil(() => canRotate);
        turnToDefault = true;
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
                //Debug.Log(side);
                RotUp();
                break;
            case "U\'":
                //Debug.Log(side);
                RotUp(-1);
                break;
            case "U2":
                //Debug.Log(side);
                RotUp(2);
                break;

            case "D":
                //Debug.Log(side);
                RotDown();
                break;
            case "D\'":
                //Debug.Log(side);
                RotDown(-1);
                break;
            case "D2":
                //Debug.Log(side);
                RotDown(2);
                break;

            case "L":
                //Debug.Log(side);
                RotLeft();
                break;
            case "L\'":
                //Debug.Log(side);
                RotLeft(-1);
                break;
            case "L2":
                //Debug.Log(side);
                RotLeft(2);
                break;

            case "R":
                //Debug.Log(side);
                RotRight();
                break;
            case "R\'":
                //Debug.Log(side);
                RotRight(-1);
                break;
            case "R2":
                //Debug.Log(side);
                RotRight(2);
                break;

            case "F":
                //Debug.Log(side);
                RotFront();
                break;
            case "F\'":
                //Debug.Log(side);
                RotFront(-1);
                break;
            case "F2":
                //Debug.Log(side);
                RotFront(2);
                break;

            case "B":
                //Debug.Log(side);
                RotBack();
                break;
            case "B\'":
                //Debug.Log(side);
                RotBack(-1);
                break;
            case "B2":
                //Debug.Log(side);
                RotBack(2);
                break;

            case "E":
                //Debug.Log(side);
                RotMidE();
                break;
            case "E\'":
                //Debug.Log(side);
                RotMidE(-1);
                break;
            case "E2":
                //Debug.Log(side);
                RotMidE(2);
                break;

            case "M":
                //Debug.Log(side);
                RotMidM();
                break;
            case "M\'":
                //Debug.Log(side);
                RotMidM(-1);
                break;
            case "M2":
                //Debug.Log(side);
                RotMidM(2);
                break;

            case "S":
                //Debug.Log(side);
                RotMidS();
                break;
            case "S\'":
                //Debug.Log(side);
                RotMidS(-1);
                break;
            case "S2":
                //Debug.Log(side);
                RotMidS(2);
                break;

            case "X":
                //Debug.Log(side);
                RotX();
                break;
            case "X\'":
                //Debug.Log(side);
                RotX(-1);
                break;
            case "X2":
                //Debug.Log(side);
                RotX(2);
                break;

            case "Y":
                //Debug.Log(side);
                RotY();
                break;
            case "Y\'":
                //Debug.Log(side);
                RotY(-1);
                break;
            case "Y2":
                //Debug.Log(side);
                RotY(2);
                break;

            case "Z":
                //Debug.Log(side);
                RotZ();
                break;
            case "Z\'":
                //Debug.Log(side);
                RotZ(-1);
                break;
            case "Z2":
                //Debug.Log(side);
                RotZ(2);
                break;

            default:
                //Debug.Log("DEFAULT " + side);
                break;
        }
    }
}
