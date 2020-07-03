using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public GameObject CubePievePref;
    public int defaultRotationSpeed;
    public ReadCubeState RCS;
    public CheckSolved checkSolved;
    public bool isSolvable;

    private List<GameObject> AllCubePieces = new List<GameObject>();
    private GameObject CubeCenterPiece;
    private bool canRotate = true;
    private Quaternion defaultRotation;
    private bool turnToDefault;
    private bool isSolved = true;
    private float EPSILON = 0.001f;


    #region Side Definition

    List<GameObject> UpPieces       => AllCubePieces.FindAll(x => System.Math.Abs(Mathf.Round(x.transform.localPosition.y) - 1)     < EPSILON);
    List<GameObject> MidEPieces     => AllCubePieces.FindAll(x => System.Math.Abs(Mathf.Round(x.transform.localPosition.y))         < EPSILON);
    List<GameObject> DownPieces     => AllCubePieces.FindAll(x => System.Math.Abs(Mathf.Round(x.transform.localPosition.y) - -1)    < EPSILON);
    List<GameObject> FrontPieces    => AllCubePieces.FindAll(x => System.Math.Abs(Mathf.Round(x.transform.localPosition.x) - 1)     < EPSILON);
    List<GameObject> MidSPieces     => AllCubePieces.FindAll(x => System.Math.Abs(Mathf.Round(x.transform.localPosition.x))         < EPSILON);
    List<GameObject> BackPieces     => AllCubePieces.FindAll(x => System.Math.Abs(Mathf.Round(x.transform.localPosition.x) - -1)    < EPSILON);
    List<GameObject> LeftPieces     => AllCubePieces.FindAll(x => System.Math.Abs(Mathf.Round(x.transform.localPosition.z) - -1)    < EPSILON);
    List<GameObject> MidMPieces     => AllCubePieces.FindAll(x => System.Math.Abs(Mathf.Round(x.transform.localPosition.z))         < EPSILON);
    List<GameObject> RightPieces    => AllCubePieces.FindAll(x => System.Math.Abs(Mathf.Round(x.transform.localPosition.z) - 1)     < EPSILON);

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
            }
        }
        CubeCenterPiece = AllCubePieces[13];
        defaultRotation = transform.rotation;

        SetDefaultRotationSpeed(PlayerPrefs.GetInt("Speed"));

    }

    void Update()
    {
        float dist = Vector3.Distance(transform.rotation.eulerAngles, defaultRotation.eulerAngles);

        if (canRotate)
        {
            defaultRotationSpeed = CurrentRotationSpeed;
        }
        if (Mathf.Abs(dist) > .1f && turnToDefault && canRotate)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, defaultRotation, .1f);
            GetComponent<CameraMovement>().SetDragging(false);
        }
        else
        {
            turnToDefault = false;
        }

    }

    public int CurrentRotationSpeed { get; set; }

    public void SetDefaultRotationSpeed(float speed)
    {
        switch ((int)speed)
        {
            case 0:
                CurrentRotationSpeed = 2;
                break;
            case 1:
                CurrentRotationSpeed = 5;
                break;
            case 2:
                CurrentRotationSpeed = 10;
                break;
            case 3:
                CurrentRotationSpeed = 15;
                break;
            case 4:
                CurrentRotationSpeed = 30;
                break;
            case 90:
                CurrentRotationSpeed = 90;
                break;
        }
    }

    public void TurnToDefault() => StartCoroutine(WaitUntilCanRotate());

    public bool CanRotate
    {
        get
        {
            return canRotate;
        }
        set
        {
            canRotate = value;
        }
    }

    public void SetCanRotate(bool val) => canRotate = val;

    public bool GetCanRotate() => canRotate;

    IEnumerator RotateCube(Vector3 rotationVec, int count = 1)
    {
        CanRotate = false;
        int angle = 0;

        while (angle < 90 * count)
        {
                transform.RotateAround(CubeCenterPiece.transform.position, transform.rotation * rotationVec, defaultRotationSpeed);

                angle += defaultRotationSpeed;
                yield return null;
        }
        CanRotate = true;
    }

    IEnumerator RotateSide(List<GameObject> pieces, Vector3 rotationVec, int count = 1, List<Transform> rays = null)
    {
        CanRotate = false;
        int angle = 0;    

        while (angle < 90 * count)
        {
            foreach (GameObject go in pieces)
            {
                go.transform.RotateAround(CubeCenterPiece.transform.position, transform.rotation * rotationVec, defaultRotationSpeed);
                
            }
            if (rays != null)
            {
                foreach (Transform ray in rays)
                {
                    //ray.RotateAround(CubeCenterPiece.transform.position, transform.rotation * rotationVec, defaultRotationSpeed);
                }
            }
            angle += defaultRotationSpeed;
            yield return null;
        }

        CanRotate = true;

        bool currSolved = RCS.IsSolved();
        if (currSolved && !isSolved)
        {
            CubeSolved();
        }
        isSolved = RCS.IsSolved();

    }

    public void CubeSolved()
    {
        checkSolved.Emit(1);
        checkSolved.StopTimer();
        StartCoroutine(checkSolved.ShowWinPanel());
        CanRotate = false;
    }

    public IEnumerator TurnFromScramble(string text)
    {
        string[] sides = text.ToUpper().Split(' ');

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
        {
            if (isSolvable)
            {
                RotDown(-dir);
                canRotate = true;
                RotUp(dir);
                StartCoroutine(RotateCube(new Vector3(0, -1 * dir, 0), Mathf.Abs(dir)));
            }
            else
            StartCoroutine(RotateSide(MidEPieces, new Vector3(0, -1 * dir, 0), Mathf.Abs(dir)));
        }
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
        {
            if (isSolvable)
            {
                RotLeft(-dir);
                canRotate = true;
                RotRight(dir);
                StartCoroutine(RotateCube(new Vector3(0, 0, -1 * dir), Mathf.Abs(dir)));
            }
            else
                StartCoroutine(RotateSide(MidMPieces, new Vector3(0, 0, -1 * dir), Mathf.Abs(dir)/*, rays*/));
        }
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
        {
            if (isSolvable)
            {
                RotBack(dir);
                canRotate = true;
                RotFront(-dir);
                StartCoroutine(RotateCube(new Vector3(1 * dir, 0, 0), Mathf.Abs(dir)));
            }
            else
                StartCoroutine(RotateSide(MidSPieces, new Vector3(1 * dir, 0, 0), Mathf.Abs(dir)/*, rays*/));
        }
    }
    public void RotBack(int dir = 1)
    {
        if (canRotate)
            StartCoroutine(RotateSide(BackPieces, new Vector3(-1 * dir, 0, 0), Mathf.Abs(dir)));
    }

    public void RotX(int dir = 1)
    {
        if (canRotate)
        {
            if (isSolvable)
                StartCoroutine(RotateCube(new Vector3(0, 0, 1 * dir), Mathf.Abs(dir)));
            else
                StartCoroutine(RotateSide(AllCubePieces, new Vector3(0, 0, 1 * dir), Mathf.Abs(dir)/*, rays*/));
        }
    }
    public void RotY(int dir = 1)
    {
        if (canRotate)
        {
            if (isSolvable)
                StartCoroutine(RotateCube(new Vector3(0, 1 * dir, 0), Mathf.Abs(dir)));
            else
                StartCoroutine(RotateSide(AllCubePieces, new Vector3(0, 1 * dir, 0), Mathf.Abs(dir)/*, rays*/));
        }
    }
    public void RotZ(int dir = 1)
    {
        if (canRotate)
        {
            if (isSolvable)
                StartCoroutine(RotateCube(new Vector3(1 * dir, 0, 0), Mathf.Abs(dir)));
            else
                StartCoroutine(RotateSide(AllCubePieces, new Vector3(1 * dir, 0, 0), Mathf.Abs(dir)/*, rays*/));
        }
    }

#endregion

    public void TurnSide(string side)
    {
        switch (side)
        {

            case "U":
                //Debug.Log(side);
                RotUp(1);
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
                RotDown(1);
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
                RotLeft(1);
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
                RotRight(1);
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
                RotFront(1);
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
                RotBack(1);
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
                RotMidE(1);
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
                RotMidS(1);
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
                RotX(1);
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
                RotY(1);
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
                RotZ(1);
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
