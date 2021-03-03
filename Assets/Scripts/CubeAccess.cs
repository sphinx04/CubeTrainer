using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAccess : MonoBehaviour
{
    public void TurnToDefault() => CubeManager.instance.TurnToDefault();
    public void SetCanRotate(bool value) => CubeManager.instance.SetCanRotate(value);
    public void RotBack() => CubeManager.instance.RotBack();
    public void RotFront() => CubeManager.instance.RotFront();
    public void RotLeft() => CubeManager.instance.RotLeft();
    public void RotRight() => CubeManager.instance.RotRight();
    public void RotUp() => CubeManager.instance.RotUp();
    public void RotDown() => CubeManager.instance.RotDown();
    public void RotX() => CubeManager.instance.RotX();
    public void RotY() => CubeManager.instance.RotY();
    public void RotZ() => CubeManager.instance.RotZ();
    public void RotM() => CubeManager.instance.RotMidM();
    public void RotE() => CubeManager.instance.RotMidE();
    public void RotS() => CubeManager.instance.RotMidS();

    public void SetArrowUiActive(bool value) => CubeManager.instance.ControlUI.SetActive(value);
    public void SetSideControlActive(bool value) => CubeManager.instance.SideControl.SetActive(value);
    public void SetDefaultRotationSpeed(float speed) => CubeManager.instance.SetDefaultRotationSpeed(speed);



    public void SetSwipeControl(bool value) => CameraMovement.instance.SetSwipeControl(value);
    public void SetDragging(bool value) => CameraMovement.instance.SetDragging(value);

    public void IsArrowModeOn(bool value) => SetFieldOfView.instance.IsArrowModeOn(value);
}
