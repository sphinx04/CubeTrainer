using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kociemba;

public class SolveCube : MonoBehaviour
{
    public ReadCubeState RCS;
    public CubeManager cm;

    public void Solve()
    {
        string info = "";
        //string solution = SearchRunTime.solution(RCS.GetString(), out info, buildTables: true);
        string solution = Search.solution(RCS.GetString(), out info);
        StartCoroutine(cm.TurnFromScramble(solution));

        print(solution);

    }
}
