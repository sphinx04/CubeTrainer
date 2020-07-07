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
        StartCoroutine(FindSolution());
    }

    IEnumerator FindSolution()
    {
        yield return new WaitUntil(() => cm.CanRotate);
        string info = "";
        //string solution = SearchRunTime.solution(RCS.GetString(), out info, buildTables: true);
        string solution = Search.solution(RCS.GetString(), out info);
        //StartCoroutine(cm.TurnFromScramble(solution));
        solutionText.text = solution;
        print(solution);
        yield return null;
    }
}
