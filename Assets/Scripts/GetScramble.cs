using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetScramble : MonoBehaviour
{
    public UnityEngine.UI.InputField text;

    public void SendToScrumble() => StartCoroutine(CubeManager.instance.TurnFromScramble(text.text));
}
