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
        if(canRotate)
            CheckInput();
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

    IEnumerator Rotate(List<GameObject> pieces, Vector3 rotationVec)
    {
        canRotate = false;
        int angle = 0;

        while(angle < 90)
        {
            foreach (GameObject go in pieces)
                go.transform.RotateAround(CubeCenterPiece.transform.position, rotationVec, 5);
            angle += 5;
            yield return null;
        }
        canRotate = true;
    }

    public void RotUp()
    {
        if(canRotate)
            StartCoroutine(Rotate(UpPieces, new Vector3(0, 1, 0)));
    }
    public void RotDown()
    {
        if(canRotate)
            StartCoroutine(Rotate(DownPieces, new Vector3(0, -1, 0)));
    }
    public void RotLeft()
    {
        if(canRotate)
            StartCoroutine(Rotate(LeftPieces, new Vector3(0, 0, -1)));
    }
    public void RotRight()
    {
        if(canRotate)
            StartCoroutine(Rotate(RightPieces, new Vector3(0, 0, 1)));
    }
    public void RotFront()
    {
        if(canRotate)
            StartCoroutine(Rotate(FrontPieces, new Vector3(1, 0, 0)));
    }
    public void RotBack()
    {
        if(canRotate)
            StartCoroutine(Rotate(BackPieces, new Vector3(-1, 0, 0)));
    }
}
