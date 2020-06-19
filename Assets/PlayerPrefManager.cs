using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefManager : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update
    void Awake()
    {
        slider.value = PlayerPrefs.GetInt("speed");
        print(PlayerPrefs.GetInt("speed"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
