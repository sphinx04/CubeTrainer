using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetScramble : MonoBehaviour
{
    public UnityEngine.UI.InputField text;
    public GameObject cube;

public void SendToScrumble()
    {
        string[] moves = text.text.ToUpper().Split(' ');
        StartCoroutine(cube.GetComponent<CubeManager>().TurnFromScramble(moves));
    }
}
