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
    bool timerResumed = true;


    public string GetTimerText => text.text;

    private void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }
    public void StartTimer() => timerStarted = true;

    public void PauseTimer() => timerResumed = false;

    public void ResumeTimer() => timerResumed = timerStarted;


    public void Enable() => isEnabled = true;

    public void Disable() => isEnabled = false;

    public float GetTime() => time;


    public void StopTimer()
    {
        if (timerStarted && isEnabled)
        {
            timerStarted = false;
            time = 0f;
            //text.text = "0" + time.ToString("F2");
            text.text = "Timer was stopped";
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (timerStarted && isEnabled && timerResumed)
        {
            time += Time.deltaTime;
            if(time < 10)
                text.text = "0" + time.ToString("F2");
            else
                text.text = time.ToString("F2");
        }
    }
}
