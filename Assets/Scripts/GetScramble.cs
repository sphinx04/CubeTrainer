using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetScramble : MonoBehaviour
{
    public UnityEngine.UI.InputField text;
    public GameObject cube;

    public void SendToScrumble() => StartCoroutine(cube.GetComponent<CubeManager>().TurnFromScramble(text.text));
}
