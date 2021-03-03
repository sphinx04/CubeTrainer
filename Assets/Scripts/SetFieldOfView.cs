using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFieldOfView : MonoBehaviour
{
    public static SetFieldOfView instance;
    [Range(0,100)]
    public int wide;
    [Range(0,100)]
    public int narrow;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    public void IsArrowModeOn(bool mode)
    {
        if (mode)
        {
            gameObject.GetComponent<Camera>().fieldOfView = wide;
        }
        else
        {
            gameObject.GetComponent<Camera>().fieldOfView = narrow;
        }
    }
}
