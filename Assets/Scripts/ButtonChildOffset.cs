using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ButtonChildOffset : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float offsetX = 3;
    public float offsetY = 3;
    private Vector3 pos;

    public void OnPointerDown(PointerEventData eventData)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            pos = transform.GetChild(i).GetComponent<RectTransform>().localPosition;
            transform.GetChild(i).GetComponent<RectTransform>().localPosition =
                new Vector3(pos.x + (float)offsetX, pos.y - (float)offsetY, pos.z);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            pos = transform.GetChild(i).GetComponent<RectTransform>().localPosition;
            transform.GetChild(i).GetComponent<RectTransform>().localPosition =
                new Vector3(pos.x - (float)offsetX, pos.y + (float)offsetY, pos.z);
        }
    }
}
