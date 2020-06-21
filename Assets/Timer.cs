using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{ 
    TextMeshProUGUI text;
    float time = 0;
    public bool isEnabled;
    bool timerStarted = true;

    private void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }
    public void StartTimer()
    {
        timerStarted = true;
    }
    public void PauseTimer()
    {
        timerStarted = false;
    }
    public void StopTimer()
    {
        PauseTimer();
        time = 0f;
    }

    public void Enable()
    {
        isEnabled = true;
    }
    public void Disable()
    {
        isEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerStarted && isEnabled)
        {
            time += Time.deltaTime;
            if(time < 10)
                text.text = "0" + time.ToString("F2");
            else
                text.text = time.ToString("F2");
            //print(time.ToString("F2"));
        }
    }
}
