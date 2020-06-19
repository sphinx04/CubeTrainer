using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefManager : MonoBehaviour
{
    public Slider slider;
    public Toggle ACToggle;
    public Toggle SCToggle;
    public Toggle FRToggle;

    // Start is called before the first frame update
    void Awake()
    {
        slider.value = PlayerPrefs.GetInt("Speed");

        ACToggle.isOn = PlayerPrefs.GetInt("Arrow Control", 0) == 1;

        SCToggle.isOn = PlayerPrefs.GetInt("Swipe Control", 1) == 1;

        FRToggle.isOn = PlayerPrefs.GetInt("Free Rotation", 1) == 1;
        FRToggle.IsInvoking();

        print(PlayerPrefs.GetInt("Speed") + " get prefman");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerPrefs()
    {
        PlayerPrefs.SetInt("Speed", (int)slider.value);
        print(PlayerPrefs.GetInt("Speed") + " set prefman");
        PlayerPrefs.SetInt("Arrow Control", ACToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("Swipe Control", SCToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("Free Rotation", FRToggle.isOn ? 1 : 0);
    }
}
