using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public GameObject CubePievePref;
    Transform CubeTransf;
    List<GameObject> AllCubePieces = new List<GameObject>();
    GameObject CubeCenterPiece;
    bool canRotate = true;

    List<GameObject> UpPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.y) == 0);
        }
    }
    List<GameObject> MidEPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.y) == -1);
        }
    }
    List<GameObject> DownPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.y) == -2);
        }
    }
    List<GameObject> FrontPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.x) == 0);
        }
    }
    List<GameObject> MidSPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.x) == -1);
        }
    }
    List<GameObject> BackPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.x) == -2);
        }
    }
    List<GameObject> LeftPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.z) == 0);
        }
    }
    List<GameObject> MidMPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.z) == -1);
        }
    }
    List<GameObject> RightPieces
    {
        get
        {
            return AllCubePieces.FindAll(x => Mathf.Round(x.transform.localPosition.z) == 2);
        }
    }


    void Start()
    {
        CubeTransf = transform;
        CreateCube();
    }

    // Update is called once per frame
    void Update()
    {
        //if (canRotate)
        //    CheckInput();
        //Debug.Log(canRotate);
        Debug.Log(transform.rotation.eulerAngles);
    }

    void CreateCube()
    {
        foreach (GameObject go in AllCubePieces)
            DestroyImmediate(go);

        AllCubePieces.Clear();

        for (int x = 0; x < 3; x++)
            for (int y = 0; y < 3; y++)
                for (int z = 0; z < 3; z++)
                {
                    GameObject go = Instantiate(CubePievePref, CubeTransf, false);
                    go.transform.localPosition = new Vector3(-x, -y, z);
                    go.GetComponent<CubePieceScr>().SetColor(-x, -y, z);
                    AllCubePieces.Add(go);
                }
        CubeCenterPiece = AllCubePieces[13];
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
            RotUp();
        else if (Input.GetKeyDown(KeyCode.S))
            RotDown();
        else if (Input.GetKeyDown(KeyCode.A))
            RotLeft();
        else if (Input.GetKeyDown(KeyCode.D))
            RotRight();
        else if (Input.GetKeyDown(KeyCode.F))
            RotFront();
        else if (Input.GetKeyDown(KeyCode.B))
            RotBack();
    }

    IEnumerator Rotate(List<GameObject> pieces, Vector3 rotationVec, int count = 1, int speed = 5)
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

    public IEnumerator SolveCube(string[] sides)
    {
        foreach (string side in sides)
        {
            TurnSide(side);
            //canRotate = false;
            //yield return new WaitForSeconds(1f);
            yield return new WaitUntil(() => canRotate);

            //canRotate = true;
        }
    }

    public void RotUp(int dir = 1)
    {
        if (canRotate)
            StartCoroutine(Rotate(UpPieces, new Vector3(0, 1 * dir, 0), Mathf.Abs(dir)));
    }
    public void RotMidE(int dir = 1)
    {
        if (canRotate)
            StartCoroutine(Rotate(MidEPieces, new Vector3(0, -1 * dir, 0), Mathf.Abs(dir)));
    }
    public void RotDown(int dir = 1)
    {
        if (canRotate)
            StartCoroutine(Rotate(DownPieces, new Vector3(0, -1 * dir, 0), Mathf.Abs(dir)));
    }
    public void RotLeft(int dir = 1)
    {
        if (canRotate)
            StartCoroutine(Rotate(LeftPieces, new Vector3(0, 0, -1 * dir), Mathf.Abs(dir)));
    }
    public void RotMidM(int dir = 1)
    {
        if (canRotate)
            StartCoroutine(Rotate(MidMPieces, new Vector3(0, 0, -1 * dir), Mathf.Abs(dir)));
    }
    public void RotRight(int dir = 1)
    {
        if (canRotate)
            StartCoroutine(Rotate(RightPieces, new Vector3(0, 0, 1 * dir), Mathf.Abs(dir)));
    }
    public void RotFront(int dir = 1)
    {
        if (canRotate)
            StartCoroutine(Rotate(FrontPieces, new Vector3(1 * dir, 0, 0), Mathf.Abs(dir)));
    }
    public void RotMidS(int dir = 1)
    {
        if (canRotate)
            StartCoroutine(Rotate(MidSPieces, new Vector3(1 * dir, 0, 0), Mathf.Abs(dir)));
    }
    public void RotBack(int dir = 1)
    {
        if (canRotate)
            StartCoroutine(Rotate(BackPieces, new Vector3(-1 * dir, 0, 0), Mathf.Abs(dir)));
    }


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

            default:
                Debug.Log("DEFAULT " + side);
                break;
        }
    }
}
