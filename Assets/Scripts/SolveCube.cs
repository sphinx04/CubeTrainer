using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kociemba;
using UnityEngine.UI;

public class SolveCube : MonoBehaviour
{
    public InputField solutionText;

    public void Solve()
    {
        CubeManager.instance.ToDefaultKeyRotation();
        CubeManager.instance.TurnToDefault();
        StartCoroutine(FindSolution());
    }

    IEnumerator FindSolution()
    {
        yield return new WaitUntil(() => CubeManager.instance.CanRotate);
        string solution = Search.solution(ReadCubeState.instance.GetString(), out _);
        if (solution.Contains("Error"))
        {
            solution = "Invalid state of Cube"; //do smth
        }
        //else
            solutionText.text = solution;
        print(solution);
        yield return null;
    }
}
