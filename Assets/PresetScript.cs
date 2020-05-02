using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetScript : MonoBehaviour
{
    public string text;
    public GameObject cube;
    public float delay = 1f;

    IEnumerator Start()
    {
        string[] moves = text.ToUpper().Split(' ');
        yield return new WaitForSeconds(delay);
        StartCoroutine(cube.GetComponent<CubeManager>().TurnFromScramble(moves));
    }
}
