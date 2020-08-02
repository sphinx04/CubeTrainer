using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;

public class SideControlCube : MonoBehaviour
{
    private bool canRotate = true;

    List<GameObject> pieces = new List<GameObject>();
    List<GameObject> planes = new List<GameObject>();
    List<string> pieceNames = new List<string>();
    List<string> planeNames = new List<string>();

    void Update()
    {
        int layerMask = LayerMask.GetMask("SideControlCube");
        int UIlayerMask = LayerMask.GetMask("UI");

        if (Input.GetMouseButton(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, UIlayerMask))
            {
                //Debug.Log("pauseMenu");
            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                if (pieces.Count < 2 &&
                    !(pieces.Exists(x => x == hit.collider.transform.parent.gameObject) &&
                      planes.Exists(x => x == hit.collider.transform.gameObject)))
                {
                    pieces.Add(hit.collider.transform.parent.gameObject);
                    planes.Add(hit.collider.gameObject);
                    pieceNames.Add(hit.collider.transform.parent.gameObject.name);
                    planeNames.Add(hit.collider.gameObject.name);
                }
                else if (pieces.Count == 2 && CameraMovement.instance.onCube && canRotate)
                {
                    DetectRotate();
                    pieces.Clear();
                    planes.Clear();
                    pieceNames.Clear();
                    planeNames.Clear();
                    canRotate = false;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            pieces.Clear();
            planes.Clear();
            pieceNames.Clear();
            planeNames.Clear();
            canRotate = true;
        }
    }

    private void DetectRotate()
    {
        if (planeNames[0] == planeNames[1])
        {
            DetectInnerRotate();
        }

        if (pieceNames[0] == pieceNames[0])
        {
            DetectEdgeRotate();
        }
    }

    private void DetectInnerRotate()
    {
        switch (planeNames[0])
        {
            case "F":
                if (DetectSwipe("UFL", "UFR")) CubeManager.instance.RotUp(-1);
                else if (DetectSwipe("UFR", "UFL")) CubeManager.instance.RotUp(1);
                else if (DetectSwipe("DFL", "DFR")) CubeManager.instance.RotDown(1);
                else if (DetectSwipe("DFR", "DFL")) CubeManager.instance.RotDown(-1);
                else if (DetectSwipe("UFR", "DFR")) CubeManager.instance.RotRight(-1);
                else if (DetectSwipe("DFR", "UFR")) CubeManager.instance.RotRight(1);
                else if (DetectSwipe("UFL", "DFL")) CubeManager.instance.RotLeft(1);
                else if (DetectSwipe("DFL", "UFL")) CubeManager.instance.RotLeft(-1);
                break;
            case "B":
                if (DetectSwipe("UBL", "UBR")) CubeManager.instance.RotUp(1);
                else if (DetectSwipe("UBR", "UBL")) CubeManager.instance.RotUp(-1);
                else if (DetectSwipe("DBL", "DBR")) CubeManager.instance.RotDown(-1);
                else if (DetectSwipe("DBR", "DBL")) CubeManager.instance.RotDown(1);
                else if (DetectSwipe("UBR", "DBR")) CubeManager.instance.RotRight(1);
                else if (DetectSwipe("DBR", "UBR")) CubeManager.instance.RotRight(-1);
                else if (DetectSwipe("UBL", "DBL")) CubeManager.instance.RotLeft(-1);
                else if (DetectSwipe("DBL", "UBL")) CubeManager.instance.RotLeft(1);
                break;
            case "U":
                if (DetectSwipe("UFL", "UFR")) CubeManager.instance.RotFront(1);
                else if (DetectSwipe("UFR", "UFL")) CubeManager.instance.RotFront(-1);
                else if (DetectSwipe("UBL", "UBR")) CubeManager.instance.RotBack(-1);
                else if (DetectSwipe("UBR", "UBL")) CubeManager.instance.RotBack(1);
                else if (DetectSwipe("UFR", "UBR")) CubeManager.instance.RotRight(1);
                else if (DetectSwipe("UBR", "UFR")) CubeManager.instance.RotRight(-1);
                else if (DetectSwipe("UFL", "UBL")) CubeManager.instance.RotLeft(-1);
                else if (DetectSwipe("UBL", "UFL")) CubeManager.instance.RotLeft(1);
                break;
            case "D":
                if (DetectSwipe("DFL", "DFR")) CubeManager.instance.RotFront(-1);
                else if (DetectSwipe("DFR", "DFL")) CubeManager.instance.RotFront(1);
                else if (DetectSwipe("DBL", "DBR")) CubeManager.instance.RotBack(1);
                else if (DetectSwipe("DBR", "DBL")) CubeManager.instance.RotBack(-1);
                else if (DetectSwipe("DFR", "DBR")) CubeManager.instance.RotRight(-1);
                else if (DetectSwipe("DBR", "DFR")) CubeManager.instance.RotRight(1);
                else if (DetectSwipe("DFL", "DBL")) CubeManager.instance.RotLeft(1);
                else if (DetectSwipe("DBL", "DFL")) CubeManager.instance.RotLeft(-1);
                break;
            case "L":
                if (DetectSwipe("UFL", "UBL")) CubeManager.instance.RotUp(1);
                else if (DetectSwipe("UBL", "UFL")) CubeManager.instance.RotUp(-1);
                else if (DetectSwipe("DFL", "DBL")) CubeManager.instance.RotDown(-1);
                else if (DetectSwipe("DBL", "DFL")) CubeManager.instance.RotDown(1);
                else if (DetectSwipe("UFL", "DFL")) CubeManager.instance.RotFront(-1);
                else if (DetectSwipe("DFL", "UFL")) CubeManager.instance.RotFront(1);
                else if (DetectSwipe("UBL", "DBL")) CubeManager.instance.RotBack(1);
                else if (DetectSwipe("DBL", "UBL")) CubeManager.instance.RotBack(-1);
                break;
            case "R":
                if (DetectSwipe("UFR", "UBR")) CubeManager.instance.RotUp(-1);
                else if (DetectSwipe("UBR", "UFR")) CubeManager.instance.RotUp(1);
                else if (DetectSwipe("DFR", "DBR")) CubeManager.instance.RotDown(1);
                else if (DetectSwipe("DBR", "DFR")) CubeManager.instance.RotDown(-1);
                else if (DetectSwipe("UFR", "DFR")) CubeManager.instance.RotFront(1);
                else if (DetectSwipe("DFR", "UFR")) CubeManager.instance.RotFront(-1);
                else if (DetectSwipe("UBR", "DBR")) CubeManager.instance.RotBack(-1);
                else if (DetectSwipe("DBR", "UBR")) CubeManager.instance.RotBack(1);
                break;
            default:
                break;
        }

        if (DetectSwipe("UL", "UR") || DetectSwipe("DR", "DL") || DetectSwipe("UR", "DR") || DetectSwipe("DL", "UL")) CubeManager.instance.RotMidS(1);
        else if (DetectSwipe("UR", "UL") || DetectSwipe("DL", "DR") || DetectSwipe("DR", "UR") || DetectSwipe("UL", "DL")) CubeManager.instance.RotMidS(-1);
        else if (DetectSwipe("FL", "FR") || DetectSwipe("BR", "BL") || DetectSwipe("FR", "BR") || DetectSwipe("BL", "FL")) CubeManager.instance.RotMidE(1);
        else if (DetectSwipe("FR", "FL") || DetectSwipe("BL", "BR") || DetectSwipe("BR", "FR") || DetectSwipe("FL", "BL")) CubeManager.instance.RotMidE(-1);
        else if (DetectSwipe("UF", "DF") || DetectSwipe("DB", "UB") || DetectSwipe("UB", "UF") || DetectSwipe("DF", "DB")) CubeManager.instance.RotMidM(1);
        else if (DetectSwipe("DB", "DF") || DetectSwipe("DF", "UF") || DetectSwipe("UB", "DB") || DetectSwipe("UF", "UB")) CubeManager.instance.RotMidM(-1);
    }
    private void DetectEdgeRotate()
    {
        pieceNames[1] = pieceNames[0];
        pieceNames[0] = pieceNames[0].Remove(pieceNames[0].IndexOf(planes[1].name.ToCharArray()[0]), 1);
        //planeNames[0] = planeNames[0];
        planeNames[1] = planeNames[0];
        DetectInnerRotate();
    }

    private bool DetectSwipe(string from, string to)
    {
        string middle = string.Concat(from.Where(c => string.Concat(from.Intersect(to)).Contains(c))); //get insection between strings

        return pieceNames[0] == from && pieceNames[1] == middle ||
                pieceNames[0] == from && pieceNames[1] == to ||
                pieceNames[0] == middle && pieceNames[1] == to;
    }
}
