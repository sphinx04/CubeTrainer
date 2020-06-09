using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectSide : MonoBehaviour
{
    public GameObject LeftControl;
    public GameObject RightControl;
    public GameObject BackControl;
    public GameObject FrontControl;
    public GameObject UpControl;
    public GameObject DownControl;

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.name)
        {
            case "DetectLeft":
                //Debug.Log("Left");
                LeftControl.SetActive(true);
                RightControl.SetActive(false);
                BackControl.SetActive(false);
                FrontControl.SetActive(false);
                UpControl.SetActive(false);
                DownControl.SetActive(false);
                break;
            case "DetectRight":
                //Debug.Log("Right");
                LeftControl.SetActive(false);
                RightControl.SetActive(true);
                BackControl.SetActive(false);
                FrontControl.SetActive(false);
                UpControl.SetActive(false);
                DownControl.SetActive(false);
                break;
            case "DetectDown":
                //Debug.Log("Down");
                LeftControl.SetActive(false);
                RightControl.SetActive(false);
                BackControl.SetActive(false);
                FrontControl.SetActive(false);
                UpControl.SetActive(false);
                DownControl.SetActive(true);
                break;
            case "DetectUp":
                //Debug.Log("Up");
                LeftControl.SetActive(false);
                RightControl.SetActive(false);
                BackControl.SetActive(false);
                FrontControl.SetActive(false);
                UpControl.SetActive(true);
                DownControl.SetActive(false);
                break;
            case "DetectFront":
                //Debug.Log("Front");
                LeftControl.SetActive(false);
                RightControl.SetActive(false);
                BackControl.SetActive(false);
                FrontControl.SetActive(true);
                UpControl.SetActive(false);
                DownControl.SetActive(false);
                break;
            case "DetectBack":
                //Debug.Log("Back");
                LeftControl.SetActive(false);
                RightControl.SetActive(false);
                BackControl.SetActive(true);
                FrontControl.SetActive(false);
                UpControl.SetActive(false);
                DownControl.SetActive(false);
                break;
            default:
                LeftControl.SetActive(false);
                RightControl.SetActive(false);
                BackControl.SetActive(false);
                FrontControl.SetActive(false);
                UpControl.SetActive(false);
                DownControl.SetActive(false);
                break;
        }
    }
}
