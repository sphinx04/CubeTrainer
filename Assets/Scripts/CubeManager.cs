using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public GameObject CubePiecePref;
    public int defaultRotationSpeed;
    public ReadCubeState RCS;
    public CheckSolved checkSolved;
    public bool sandboxMode;

    public AnimationCurve easing;

    private List<GameObject> AllCubePieces = new List<GameObject>();
    private GameObject CubeCenterPiece;
    private bool canRotate = true;
    private Quaternion defaultRotation;
    private bool turnToDefault;
    private bool isSolved = true;
    private float EPSILON = 0.001f;
    private CameraMovement _cameraMovement;
    private int totalMoves;


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
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.CompareTag("Piece"))
            {
                var piece = transform.GetChild(i).gameObject;
                AllCubePieces.Add(piece);
            }
        }
        CubeCenterPiece = AllCubePieces[13];
        defaultRotation = transform.rotation;

        SetDefaultRotationSpeed(PlayerPrefs.GetInt("Speed"));

        _cameraMovement = GetComponent<CameraMovement>();

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
            _cameraMovement.SetDragging(false);
        }
        else
        {
            turnToDefault = false;
        }

    }

    public int CurrentRotationSpeed { get; set; }
    public void SetDefaultRotationSpeed(float speed) => CurrentRotationSpeed = (int) speed;
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

    public int TotalMoves { get => totalMoves; set => totalMoves = value; }

    public void SetCanRotate(bool val) => canRotate = val;

    public bool GetCanRotate() => canRotate;

    private void SetParent(List<GameObject> objects, Transform parent)
    {
        foreach (GameObject go in objects)
        {
            if (go != CubeCenterPiece)
            {
                go.transform.SetParent(parent);
            }
        }
    }
    public void ToDefaultKeyRotation()
    {
        SetParent(AllCubePieces, CubeCenterPiece.transform);
        StartCoroutine(RestoreDefaultKeyRotation());
        print(CubeCenterPiece.transform.localRotation.normalized);
    }

    IEnumerator RestoreDefaultKeyRotation()
    {
        CanRotate = false;
        while (Vector3.Distance(CubeCenterPiece.transform.localEulerAngles, new Vector3(0, 0, 0)) > 1f)
        {
            CubeCenterPiece.transform.localRotation = Quaternion.Slerp(CubeCenterPiece.transform.localRotation,
                new Quaternion(0, 0, 0, 1), 0.1f);
            yield return null;
        }

        CubeCenterPiece.transform.localEulerAngles = new Vector3(0, 0, 0);
        SetParent(AllCubePieces, CubeCenterPiece.transform.parent);
        CanRotate = true;
    }

    IEnumerator RotateSide(List<GameObject> pieces, Vector3 rotationVec, int count = 1)
    {
        CanRotate = false;
        float angle = 0;
        Vector3 point;
        float deltaAngle = 0;
        
        while (angle < 90f * count - deltaAngle)
        {
            point = CubeCenterPiece.transform.position;
            Vector3 axis = transform.rotation * rotationVec;
            deltaAngle = defaultRotationSpeed * Time.deltaTime;
            foreach (GameObject go in pieces)
            {
                go.transform.RotateAround(point, axis, deltaAngle);
            }
            angle += deltaAngle;
            yield return null;
        }

        foreach (GameObject go in pieces)
        {
            go.transform.RotateAround(CubeCenterPiece.transform.position, transform.rotation * rotationVec,
                90f * count - angle);
        }

        totalMoves += 1;
        CanRotate = true;
        
        bool currSolved = RCS.IsSolved();
        if (currSolved && !isSolved && !sandboxMode)
        {
            CubeSolved();
        }
        isSolved = RCS.IsSolved();

    }
    

    public void CubeSolved()
    {
        checkSolved.Emit(1);
        checkSolved.StopTimer();
        StartCoroutine(checkSolved.ShowWinPanel(totalMoves));
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

/*
    public void TurnFromDefaultScramble(string[] sides)
    {
        foreach (string side in sides)
        {
            TurnSide(side);
        }
    }
*/

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

private void TurnSide(string side)
    {
        switch (side)
        {
            case "U": 
                RotUp(1);
                break;
            case "U\'": 
                RotUp(-1);
                break;
            case "U2": 
                RotUp(2);
                break;

            case "D": 
                RotDown(1);
                break;
            case "D\'": 
                RotDown(-1);
                break;
            case "D2": 
                RotDown(2);
                break;

            case "L": 
                RotLeft(1);
                break;
            case "L\'": 
                RotLeft(-1);
                break;
            case "L2": 
                RotLeft(2);
                break;

            case "R": 
                RotRight(1);
                break;
            case "R\'": 
                RotRight(-1);
                break;
            case "R2": 
                RotRight(2);
                break;

            case "F": 
                RotFront(1);
                break;
            case "F\'": 
                RotFront(-1);
                break;
            case "F2": 
                RotFront(2);
                break;

            case "B": 
                RotBack(1);
                break;
            case "B\'": 
                RotBack(-1);
                break;
            case "B2": 
                RotBack(2);
                break;

            case "E": 
                RotMidE(1);
                break;
            case "E\'": 
                RotMidE(-1);
                break;
            case "E2": 
                RotMidE(2);
                break;

            case "M": 
                RotMidM();
                break;
            case "M\'": 
                RotMidM(-1);
                break;
            case "M2": 
                RotMidM(2);
                break;

            case "S": 
                RotMidS(1);
                break;
            case "S\'": 
                RotMidS(-1);
                break;
            case "S2": 
                RotMidS(2);
                break;

            case "X": 
                RotX(1);
                break;
            case "X\'": 
                RotX(-1);
                break;
            case "X2": 
                RotX(2);
                break;

            case "Y": 
                RotY(1);
                break;
            case "Y\'": 
                RotY(-1);
                break;
            case "Y2": 
                RotY(2);
                break;

            case "Z": 
                RotZ(1);
                break;
            case "Z\'": 
                RotZ(-1);
                break;
            case "Z2": 
                RotZ(2);
                break;
        }
    }
}
