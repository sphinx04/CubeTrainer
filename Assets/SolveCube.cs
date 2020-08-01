using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kociemba;
using UnityEngine.UI;

public class SolveCube : MonoBehaviour
{
    public ReadCubeState RCS;
    public CubeManager cm;
    public InputField solutionText;

    public void Solve()
    {
        cm.ToDefaultKeyRotation();
        cm.TurnToDefault();
        StartCoroutine(FindSolution());
    }

    IEnumerator FindSolution()
    {
        yield return new WaitUntil(() => cm.CanRotate);
        string solution = Search.solution(RCS.GetString(), out _);
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
