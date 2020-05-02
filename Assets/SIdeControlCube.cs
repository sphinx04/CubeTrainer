using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIdeControlCube : MonoBehaviour
{
    public CubeManager manager;
    public CameraMovement cm;
    List<GameObject> AllCubePieces = new List<GameObject>();
    List<GameObject> pieces = new List<GameObject>();
    List<GameObject> planes = new List<GameObject>();

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


    string startPieceColor = "";
    string endPieceColor = "";
    string startPieceName = "";
    string endPieceName = "";
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = LayerMask.GetMask("SideControlCube");

        // Does the ray intersect any objects excluding the player layer
        if (Input.GetMouseButton(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Debug.Log(".");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                if (pieces.Count < 2 && !pieces.Exists(x => x == hit.collider.transform.parent.gameObject))
                {

                    Debug.Log("<2");
                    pieces.Add(hit.collider.transform.parent.gameObject);
                    planes.Add(hit.collider.gameObject);
                }
                else if (pieces.Count == 2 && cm.onCube)
                {
                    Debug.Log("2");
                    DetectRotate();
                    pieces.Clear();
                    planes.Clear();
                }


                //startPieceColor = hit.collider.gameObject.name;
                //startPieceName = hit.collider.transform.parent.name;
                //Debug.Log(startPieceColor);
                //Debug.Log(startPieceName);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            pieces.Clear();
            planes.Clear();
        }
    }



    void DetectRotate()
    {
        if (pieces[0].name == "UFL" && pieces[1].name == "UF" ||
            pieces[0].name == "UFL" && pieces[1].name == "UFR" ||
            pieces[0].name == "UF" && pieces[1].name == "UFR")
        {
            if (planes[0].name == "Front" && planes[1].name == "Front")
            {
                manager.RotUp(-1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }

            if (planes[0].name == "Up" && planes[1].name == "Up")
            {
                manager.RotFront(1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }
        }
        if (pieces[1].name == "UFL" && pieces[0].name == "UF" ||
            pieces[1].name == "UFL" && pieces[0].name == "UFR" ||
            pieces[1].name == "UF" && pieces[0].name == "UFR")
        {
            if (planes[0].name == "Front" && planes[1].name == "Front")
            {
                manager.RotUp(1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }

            if (planes[0].name == "Up" && planes[1].name == "Up")
            {
                manager.RotFront(-1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }
        }


        if (pieces[0].name == "UBL" && pieces[1].name == "UB" ||
            pieces[0].name == "UBL" && pieces[1].name == "UBR" ||
            pieces[0].name == "UB" && pieces[1].name == "UBR")
        {
            if (planes[0].name == "Back" && planes[1].name == "Back")
            {
                manager.RotUp(1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }

            if (planes[0].name == "Up" && planes[1].name == "Up")
            {
                manager.RotBack(-1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }
        }
        if (pieces[1].name == "UBL" && pieces[0].name == "UB" ||
            pieces[1].name == "UBL" && pieces[0].name == "UBR" ||
            pieces[1].name == "UB" && pieces[0].name == "UBR")
        {
            if (planes[0].name == "Back" && planes[1].name == "Back")
            {
                manager.RotUp(-1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }

            if (planes[0].name == "Up" && planes[1].name == "Up")
            {
                manager.RotBack(1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }
        }



        if (pieces[0].name == "UFR" && pieces[1].name == "UR" ||
            pieces[0].name == "UFR" && pieces[1].name == "UBR" ||
            pieces[0].name == "UR" && pieces[1].name == "UBR")
        {
            if (planes[0].name == "Right" && planes[1].name == "Right")
            {
                manager.RotUp(-1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }

            if (planes[0].name == "Up" && planes[1].name == "Up")
            {
                manager.RotRight(1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }
        }
        if (pieces[1].name == "UFR" && pieces[0].name == "UR" ||
            pieces[1].name == "UFR" && pieces[0].name == "UBR" ||
            pieces[1].name == "UR" && pieces[0].name == "UBR")
        {
            if (planes[0].name == "Right" && planes[1].name == "Right")
            {
                manager.RotUp(1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }

            if (planes[0].name == "Up" && planes[1].name == "Up")
            {
                manager.RotRight(-1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }
        }

        if (pieces[0].name == "UFL" && pieces[1].name == "UL" ||
            pieces[0].name == "UFL" && pieces[1].name == "UBL" ||
            pieces[0].name == "UL" && pieces[1].name == "UBL")
        {
            if (planes[0].name == "Left" && planes[1].name == "Left")
            {
                manager.RotUp(1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }

            if (planes[0].name == "Up" && planes[1].name == "Up")
            {
                manager.RotLeft(-1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }
        }
        if (pieces[1].name == "UFL" && pieces[0].name == "UL" ||
            pieces[1].name == "UFL" && pieces[0].name == "UBL" ||
            pieces[1].name == "UL" && pieces[0].name == "UBL")
        {
            if (planes[0].name == "Left" && planes[1].name == "Left")
            {
                manager.RotUp(-1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }

            if (planes[0].name == "Up" && planes[1].name == "Up")
            {
                manager.RotLeft(1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }
        }





        if (pieces[0].name == "DFL" && pieces[1].name == "DF" ||
            pieces[0].name == "DFL" && pieces[1].name == "DFR" ||
            pieces[0].name == "DF" && pieces[1].name == "DFR")
        {
            if (planes[0].name == "Front" && planes[1].name == "Front")
            {
                manager.RotDown(1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }

            if (planes[0].name == "Down" && planes[1].name == "Down")
            {
                manager.RotFront(-1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }
        }
        if (pieces[1].name == "DFL" && pieces[0].name == "DF" ||
            pieces[1].name == "DFL" && pieces[0].name == "DFR" ||
            pieces[1].name == "DF" && pieces[0].name == "DFR")
        {
            if (planes[0].name == "Front" && planes[1].name == "Front")
            {
                manager.RotDown(-1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }

            if (planes[0].name == "Down" && planes[1].name == "Down")
            {
                manager.RotFront(1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }
        }


        if (pieces[0].name == "DBL" && pieces[1].name == "DB" ||
            pieces[0].name == "DBL" && pieces[1].name == "DBR" ||
            pieces[0].name == "DB" && pieces[1].name == "DBR")
        {
            if (planes[0].name == "Back" && planes[1].name == "Back")
            {
                manager.RotDown(-1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }

            if (planes[0].name == "Down" && planes[1].name == "Down")
            {
                manager.RotBack(1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }
        }
        if (pieces[1].name == "DBL" && pieces[0].name == "DB" ||
            pieces[1].name == "DBL" && pieces[0].name == "DBR" ||
            pieces[1].name == "DB" && pieces[0].name == "DBR")
        {
            if (planes[0].name == "Back" && planes[1].name == "Back")
            {
                manager.RotDown(1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }

            if (planes[0].name == "Down" && planes[1].name == "Down")
            {
                manager.RotBack(-1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }
        }






        if (pieces[0].name == "DFR" && pieces[1].name == "DR" ||
           pieces[0].name == "DFR" && pieces[1].name == "DBR" ||
           pieces[0].name == "DR" && pieces[1].name == "DBR")
        {
            if (planes[0].name == "Right" && planes[1].name == "Right")
            {
                manager.RotDown(1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }

            if (planes[0].name == "Down" && planes[1].name == "Down")
            {
                manager.RotRight(-1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }
        }
        if (pieces[1].name == "DFR" && pieces[0].name == "DR" ||
            pieces[1].name == "DFR" && pieces[0].name == "DBR" ||
            pieces[1].name == "DR" && pieces[0].name == "DBR")
        {
            if (planes[0].name == "Right" && planes[1].name == "Right")
            {
                manager.RotDown(-1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }

            if (planes[0].name == "Down" && planes[1].name == "Down")
            {
                manager.RotRight(1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }
        }

        if (pieces[0].name == "DFL" && pieces[1].name == "DL" ||
            pieces[0].name == "DFL" && pieces[1].name == "DBL" ||
            pieces[0].name == "DL" && pieces[1].name == "DBL")
        {
            if (planes[0].name == "Left" && planes[1].name == "Left")
            {
                manager.RotDown(-1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }

            if (planes[0].name == "Down" && planes[1].name == "Down")
            {
                manager.RotLeft(1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }
        }
        if (pieces[1].name == "DFL" && pieces[0].name == "DL" ||
            pieces[1].name == "DFL" && pieces[0].name == "DBL" ||
            pieces[1].name == "DL" && pieces[0].name == "DBL")
        {
            if (planes[0].name == "Left" && planes[1].name == "Left")
            {
                manager.RotDown(1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }

            if (planes[0].name == "Down" && planes[1].name == "Down")
            {
                manager.RotLeft(-1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }
        }




        if (pieces[0].name == "UFR" && pieces[1].name == "FR" ||
            pieces[0].name == "UFR" && pieces[1].name == "DFR" ||
            pieces[0].name == "FR" && pieces[1].name == "DFR")
        {
            if (planes[0].name == "Front" && planes[1].name == "Front")
            {
                manager.RotRight(-1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }

            if (planes[0].name == "Right" && planes[1].name == "Right")
            {
                manager.RotFront(1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }
        }
        if (pieces[1].name == "UFR" && pieces[0].name == "FR" ||
           pieces[1].name == "UFR" && pieces[0].name == "DFR" ||
           pieces[1].name == "FR" && pieces[0].name == "DFR")
        {
            if (planes[0].name == "Front" && planes[1].name == "Front")
            {
                manager.RotRight(1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }

            if (planes[0].name == "Right" && planes[1].name == "Right")
            {
                manager.RotFront(-1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }
        }



        if (pieces[0].name == "UBR" && pieces[1].name == "BR" ||
            pieces[0].name == "UBR" && pieces[1].name == "DBR" ||
            pieces[0].name == "BR" && pieces[1].name == "DBR")
        {
            if (planes[0].name == "Back" && planes[1].name == "Back")
            {
                manager.RotRight(1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }

            if (planes[0].name == "Right" && planes[1].name == "Right")
            {
                manager.RotBack(-1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }
        }
        if (pieces[1].name == "UBR" && pieces[0].name == "BR" ||
           pieces[1].name == "UBR" && pieces[0].name == "DBR" ||
           pieces[1].name == "BR" && pieces[0].name == "DBR")
        {
            if (planes[0].name == "Back" && planes[1].name == "Back")
            {
                manager.RotRight(-1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }

            if (planes[0].name == "Right" && planes[1].name == "Right")
            {
                manager.RotBack(1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }
        }


        if (pieces[0].name == "UFL" && pieces[1].name == "FL" ||
            pieces[0].name == "UFL" && pieces[1].name == "DFL" ||
            pieces[0].name == "FL" && pieces[1].name == "DFL")
        {
            if (planes[0].name == "Front" && planes[1].name == "Front")
            {
                manager.RotLeft(1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }

            if (planes[0].name == "Left" && planes[1].name == "Left")
            {
                manager.RotFront(-1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }
        }
        if (pieces[1].name == "UFL" && pieces[0].name == "FL" ||
           pieces[1].name == "UFL" && pieces[0].name == "DFL" ||
           pieces[1].name == "FL" && pieces[0].name == "DFL")
        {
            if (planes[0].name == "Front" && planes[1].name == "Front")
            {
                manager.RotLeft(-1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }

            if (planes[0].name == "Left" && planes[1].name == "Left")
            {
                manager.RotFront(1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }
        }



        if (pieces[0].name == "UBL" && pieces[1].name == "BL" ||
            pieces[0].name == "UBL" && pieces[1].name == "DBL" ||
            pieces[0].name == "BL" && pieces[1].name == "DBL")
        {
            if (planes[0].name == "Back" && planes[1].name == "Back")
            {
                manager.RotLeft(-1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }

            if (planes[0].name == "Left" && planes[1].name == "Left")
            {
                manager.RotBack(1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }
        }
        if (pieces[1].name == "UBL" && pieces[0].name == "BL" ||
           pieces[1].name == "UBL" && pieces[0].name == "DBL" ||
           pieces[1].name == "BL" && pieces[0].name == "DBL")
        {
            if (planes[0].name == "Back" && planes[1].name == "Back")
            {
                manager.RotLeft(1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }

            if (planes[0].name == "Left" && planes[1].name == "Left")
            {
                manager.RotBack(-1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
            }
        }





        if (pieces[0].name == "UL" && pieces[1].name == "U" ||
            pieces[0].name == "UL" && pieces[1].name == "UR" ||
            pieces[0].name == "U" && pieces[1].name == "UR")
        {
            manager.RotMidS(1);
            startPieceColor = endPieceColor = startPieceName = endPieceName = "";
        }
        if (pieces[1].name == "UL" && pieces[0].name == "U" ||
            pieces[1].name == "UL" && pieces[0].name == "UR" ||
            pieces[1].name == "U" && pieces[0].name == "UR")
        {
            manager.RotMidS(-1);
            startPieceColor = endPieceColor = startPieceName = endPieceName = "";
        }

        if (pieces[0].name == "DL" && pieces[1].name == "D" ||
            pieces[0].name == "DL" && pieces[1].name == "DR" ||
            pieces[0].name == "D" && pieces[1].name == "DR")
        {
            manager.RotMidS(-1);
            startPieceColor = endPieceColor = startPieceName = endPieceName = "";
        }
        if (pieces[1].name == "DL" && pieces[0].name == "D" ||
            pieces[1].name == "DL" && pieces[0].name == "DR" ||
            pieces[1].name == "D" && pieces[0].name == "DR")
        {
            manager.RotMidS(1);
            startPieceColor = endPieceColor = startPieceName = endPieceName = "";
        }
        if (pieces[0].name == "UR" && pieces[1].name == "R" ||
            pieces[0].name == "UR" && pieces[1].name == "DR" ||
            pieces[0].name == "R" && pieces[1].name == "DR")
        {
            manager.RotMidS(1);
            startPieceColor = endPieceColor = startPieceName = endPieceName = "";
        }
        if (pieces[1].name == "UR" && pieces[0].name == "R" ||
            pieces[1].name == "UR" && pieces[0].name == "DR" ||
            pieces[1].name == "R" && pieces[0].name == "DR")
        {
            manager.RotMidS(-1);
            startPieceColor = endPieceColor = startPieceName = endPieceName = "";
        }
        if (pieces[0].name == "UL" && pieces[1].name == "L" ||
            pieces[0].name == "UL" && pieces[1].name == "DL" ||
            pieces[0].name == "L" && pieces[1].name == "DL")
        {
            manager.RotMidS(-1);
            startPieceColor = endPieceColor = startPieceName = endPieceName = "";
        }
        if (pieces[1].name == "UL" && pieces[0].name == "L" ||
            pieces[1].name == "UL" && pieces[0].name == "DL" ||
            pieces[1].name == "L" && pieces[0].name == "DL")
        {
            manager.RotMidS(1);
            startPieceColor = endPieceColor = startPieceName = endPieceName = "";
        }





        if (pieces[0].name == "FL" && pieces[1].name == "F" ||
            pieces[0].name == "FL" && pieces[1].name == "FR" ||
            pieces[0].name == "F" && pieces[1].name == "FR")
        {
            manager.RotMidE(1);
            startPieceColor = endPieceColor = startPieceName = endPieceName = "";
        }
        if (pieces[1].name == "FL" && pieces[0].name == "F" ||
            pieces[1].name == "FL" && pieces[0].name == "FR" ||
            pieces[1].name == "F" && pieces[0].name == "FR")
        {
            manager.RotMidE(-1);
            startPieceColor = endPieceColor = startPieceName = endPieceName = "";
        }
        if (pieces[0].name == "BL" && pieces[1].name == "B" ||
            pieces[0].name == "BL" && pieces[1].name == "BR" ||
            pieces[0].name == "B" && pieces[1].name == "BR")
        {
            manager.RotMidE(-1);
            startPieceColor = endPieceColor = startPieceName = endPieceName = "";
        }
        if (pieces[1].name == "BL" && pieces[0].name == "B" ||
            pieces[1].name == "BL" && pieces[0].name == "BR" ||
            pieces[1].name == "B" && pieces[0].name == "BR")
        {
            manager.RotMidE(1);
            startPieceColor = endPieceColor = startPieceName = endPieceName = "";
        }
        if (pieces[0].name == "FR" && pieces[1].name == "R" ||
            pieces[0].name == "FR" && pieces[1].name == "BR" ||
            pieces[0].name == "R" && pieces[1].name == "BR")
        {
            manager.RotMidE(1);
            startPieceColor = endPieceColor = startPieceName = endPieceName = "";
        }
        if (pieces[1].name == "FR" && pieces[0].name == "R" ||
            pieces[1].name == "FR" && pieces[0].name == "BR" ||
            pieces[1].name == "R" && pieces[0].name == "BR")
        {
            manager.RotMidE(-1);
            startPieceColor = endPieceColor = startPieceName = endPieceName = "";
        }
        if (pieces[0].name == "FL" && pieces[1].name == "L" ||
            pieces[0].name == "FL" && pieces[1].name == "BL" ||
            pieces[0].name == "L" && pieces[1].name == "BL")
        {
            manager.RotMidE(-1);
            startPieceColor = endPieceColor = startPieceName = endPieceName = "";
        }
        if (pieces[1].name == "FL" && pieces[0].name == "L" ||
            pieces[1].name == "FL" && pieces[0].name == "BL" ||
            pieces[1].name == "L" && pieces[0].name == "BL")
        {
            manager.RotMidE(1);
            startPieceColor = endPieceColor = startPieceName = endPieceName = "";
        }







        if (pieces[0].name == "UF" && pieces[1].name == "F" ||
            pieces[0].name == "UF" && pieces[1].name == "DF" ||
            pieces[0].name == "F" && pieces[1].name == "DF")
        {
            manager.RotMidM(1);
            startPieceColor = endPieceColor = startPieceName = endPieceName = "";
        }
        if (pieces[1].name == "UF" && pieces[0].name == "F" ||
            pieces[1].name == "UF" && pieces[0].name == "DF" ||
            pieces[1].name == "F" && pieces[0].name == "DF")
        {
            manager.RotMidM(-1);
            startPieceColor = endPieceColor = startPieceName = endPieceName = "";
        }
        if (pieces[0].name == "UB" && pieces[1].name == "B" ||
            pieces[0].name == "UB" && pieces[1].name == "DB" ||
            pieces[0].name == "B" && pieces[1].name == "DB")
        {
            manager.RotMidM(-1);
            startPieceColor = endPieceColor = startPieceName = endPieceName = "";
        }
        if (pieces[1].name == "UB" && pieces[0].name == "B" ||
            pieces[1].name == "UB" && pieces[0].name == "DB" ||
            pieces[1].name == "B" && pieces[0].name == "DB")
        {
            manager.RotMidM(1);
            startPieceColor = endPieceColor = startPieceName = endPieceName = "";
        }
        if (pieces[0].name == "UF" && pieces[1].name == "U" ||
            pieces[0].name == "UF" && pieces[1].name == "UB" ||
            pieces[0].name == "U" && pieces[1].name == "UB")
        {
                manager.RotMidM(-1);
                startPieceColor = endPieceColor = startPieceName = endPieceName = "";
        }
        if (pieces[1].name == "UF" && pieces[0].name == "U" ||
            pieces[1].name == "UF" && pieces[0].name == "UB" ||
            pieces[1].name == "U" && pieces[0].name == "UB")
        {
            manager.RotMidM(1);
            startPieceColor = endPieceColor = startPieceName = endPieceName = "";
        }
        if (pieces[0].name == "DF" && pieces[1].name == "D" ||
            pieces[0].name == "DF" && pieces[1].name == "DB" ||
            pieces[0].name == "D" && pieces[1].name == "DB")
        {
            manager.RotMidM(1);
            startPieceColor = endPieceColor = startPieceName = endPieceName = "";
        }
        if (pieces[1].name == "DF" && pieces[0].name == "D" ||
            pieces[1].name == "DF" && pieces[0].name == "DB" ||
            pieces[1].name == "D" && pieces[0].name == "DB")
        {
            manager.RotMidM(-1);
            startPieceColor = endPieceColor = startPieceName = endPieceName = "";
        }

    }
}
