using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetScript : MonoBehaviour
{
    public string text;
    public CubeManager manager;
    public float delay = 1f;
    public int defaultSpeed;
    //private void Awake()
    //{
    //    string[] moves = text.ToUpper().Split(' ');
    //    manager.SetDefaultRotationSpeed(90);
    //    StartCoroutine(manager.TurnFromScramble(moves));
    //    manager.SetDefaultRotationSpeed(2);
    //}
    IEnumerator Start()
    {
        string[] moves = text.ToUpper().Split(' ');
        yield return new WaitForSeconds(delay);
        manager.defaultRotationSpeed = 10;// defaultSpeed;
        StartCoroutine(manager.TurnFromScramble(moves));
        //manager.TurnFromDefaultScramble(moves);
        //yield return new WaitForSeconds(.5f);
        manager.SetCurrentRotationSpeed(10);

    }
}
