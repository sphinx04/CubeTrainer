using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckSolved : MonoBehaviour
{
    public List<GameObject> particles;
    public Timer timer;
    //public EasyTween winPanel;
    public TextMeshProUGUI totalTime;
    public TextMeshProUGUI totalMoves;
    public GameObject controlUI;
    public GameObject tapButton;


    public void Emit(int count)
    {
        foreach (GameObject particle in particles)
        {
            Instantiate(particle, transform);
            particle.GetComponent<ParticleSystem>().Emit(count);
        }
    }

    public void StopTimer()
    {
        timer.PauseTimer();
        timer.Disable();
        controlUI.SetActive(false);
    }

    public IEnumerator ShowWinPanel()
    {
        yield return new WaitForSeconds(0);
        //yield return new WaitUntil(()=> Input.GetMouseButtonDown(0));

        //winPanel.OpenCloseObjectAnimation();
        tapButton.SetActive(true);

        if (timer.GetTime() < 10)
            totalTime.text = "0" + timer.GetTime().ToString("F2");
        else
            totalTime.text = timer.GetTime().ToString("F2");
    }
}
