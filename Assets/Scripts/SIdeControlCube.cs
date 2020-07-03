using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;

public class SideControlCube : MonoBehaviour
{
    public CubeManager manager;
    public CameraMovement cm;
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
                else if (pieces.Count == 2 && cm.onCube && canRotate)
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
                if (DetectSwipe("UFL", "UFR")) manager.RotUp(-1);
                else if (DetectSwipe("UFR", "UFL")) manager.RotUp(1);
                else if (DetectSwipe("DFL", "DFR")) manager.RotDown(1);
                else if (DetectSwipe("DFR", "DFL")) manager.RotDown(-1);
                else if (DetectSwipe("UFR", "DFR")) manager.RotRight(-1);
                else if (DetectSwipe("DFR", "UFR")) manager.RotRight(1);
                else if (DetectSwipe("UFL", "DFL")) manager.RotLeft(1);
                else if (DetectSwipe("DFL", "UFL")) manager.RotLeft(-1);
                break;
            case "B":
                if (DetectSwipe("UBL", "UBR")) manager.RotUp(1);
                else if (DetectSwipe("UBR", "UBL")) manager.RotUp(-1);
                else if (DetectSwipe("DBL", "DBR")) manager.RotDown(-1);
                else if (DetectSwipe("DBR", "DBL")) manager.RotDown(1);
                else if (DetectSwipe("UBR", "DBR")) manager.RotRight(1);
                else if (DetectSwipe("DBR", "UBR")) manager.RotRight(-1);
                else if (DetectSwipe("UBL", "DBL")) manager.RotLeft(-1);
                else if (DetectSwipe("DBL", "UBL")) manager.RotLeft(1);
                break;
            case "U":
                if (DetectSwipe("UFL", "UFR")) manager.RotFront(1);
                else if (DetectSwipe("UFR", "UFL")) manager.RotFront(-1);
                else if (DetectSwipe("UBL", "UBR")) manager.RotBack(-1);
                else if (DetectSwipe("UBR", "UBL")) manager.RotBack(1);
                else if (DetectSwipe("UFR", "UBR")) manager.RotRight(1);
                else if (DetectSwipe("UBR", "UFR")) manager.RotRight(-1);
                else if (DetectSwipe("UFL", "UBL")) manager.RotLeft(-1);
                else if (DetectSwipe("UBL", "UFL")) manager.RotLeft(1);
                break;
            case "D":
                if (DetectSwipe("DFL", "DFR")) manager.RotFront(-1);
                else if (DetectSwipe("DFR", "DFL")) manager.RotFront(1);
                else if (DetectSwipe("DBL", "DBR")) manager.RotBack(1);
                else if (DetectSwipe("DBR", "DBL")) manager.RotBack(-1);
                else if (DetectSwipe("DFR", "DBR")) manager.RotRight(-1);
                else if (DetectSwipe("DBR", "DFR")) manager.RotRight(1);
                else if (DetectSwipe("DFL", "DBL")) manager.RotLeft(1);
                else if (DetectSwipe("DBL", "DFL")) manager.RotLeft(-1);
                break;
            case "L":
                if (DetectSwipe("UFL", "UBL")) manager.RotUp(1);
                else if (DetectSwipe("UBL", "UFL")) manager.RotUp(-1);
                else if (DetectSwipe("DFL", "DBL")) manager.RotDown(-1);
                else if (DetectSwipe("DBL", "DFL")) manager.RotDown(1);
                else if (DetectSwipe("UFL", "DFL")) manager.RotFront(-1);
                else if (DetectSwipe("DFL", "UFL")) manager.RotFront(1);
                else if (DetectSwipe("UBL", "DBL")) manager.RotBack(1);
                else if (DetectSwipe("DBL", "UBL")) manager.RotBack(-1);
                break;
            case "R":
                if (DetectSwipe("UFR", "UBR")) manager.RotUp(-1);
                else if (DetectSwipe("UBR", "UFR")) manager.RotUp(1);
                else if (DetectSwipe("DFR", "DBR")) manager.RotDown(1);
                else if (DetectSwipe("DBR", "DFR")) manager.RotDown(-1);
                else if (DetectSwipe("UFR", "DFR")) manager.RotFront(1);
                else if (DetectSwipe("DFR", "UFR")) manager.RotFront(-1);
                else if (DetectSwipe("UBR", "DBR")) manager.RotBack(-1);
                else if (DetectSwipe("DBR", "UBR")) manager.RotBack(1);
                break;
            default:
                break;
        }

        if (DetectSwipe("UL", "UR") || DetectSwipe("DR", "DL") || DetectSwipe("UR", "DR") || DetectSwipe("DL", "UL")) manager.RotMidS(1);
        else if (DetectSwipe("UR", "UL") || DetectSwipe("DL", "DR") || DetectSwipe("DR", "UR") || DetectSwipe("UL", "DL")) manager.RotMidS(-1);
        else if (DetectSwipe("FL", "FR") || DetectSwipe("BR", "BL") || DetectSwipe("FR", "BR") || DetectSwipe("BL", "FL")) manager.RotMidE(1);
        else if (DetectSwipe("FR", "FL") || DetectSwipe("BL", "BR") || DetectSwipe("BR", "FR") || DetectSwipe("FL", "BL")) manager.RotMidE(-1);
        else if (DetectSwipe("UF", "DF") || DetectSwipe("DB", "UB") || DetectSwipe("UB", "UF") || DetectSwipe("DF", "DB")) manager.RotMidM(1);
        else if (DetectSwipe("DB", "DF") || DetectSwipe("DF", "UF") || DetectSwipe("UB", "DB") || DetectSwipe("UF", "UB")) manager.RotMidM(-1);
    }
    private void DetectEdgeRotate()
    {
        pieceNames[1] = pieceNames[0];
        pieceNames[0] = pieceNames[0].Remove(pieceNames[0].IndexOf(planes[1].name.ToCharArray()[0]), 1);
        planeNames[0] = planeNames[0];
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
