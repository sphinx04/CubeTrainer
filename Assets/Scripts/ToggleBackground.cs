using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleBackground : MonoBehaviour
{
    public Sprite isOnSprite;
    public Sprite isOffSprite;

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.gameObject.GetComponent<Toggle>().isOn)
        {
            gameObject.GetComponent<Image>().sprite = isOnSprite;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = isOffSprite;
        }
    }
}
