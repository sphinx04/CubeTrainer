using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetScript : MonoBehaviour
{
    public CubeManager manager;
    public float delay = 1f;
    public int defaultSpeed;
    public int shuffleSize = 20;
    public bool willShuffle;

    private string[] notation;
    private string[] x;
    private string[] y;
    private string[] z;
    private string value;
    private string note;

    public bool IsScrambled { get; set; }

    IEnumerator Start()
    {
        if (willShuffle)
        {
            x = new string[] { "R ", "R' ", "R2 ", "L ", "L' ", "L2 ", "X ", "X' ", "X2 ", "M ", "M' ", "M2 " };
            y = new string[] { "U ", "U' ", "U2 ", "D ", "D' ", "D2 ", "Y ", "Y' ", "Y2 ", "S ", "S' ", "S2 "  };
            z = new string[] { "F ", "F' ", "F2 ", "B ", "B' ", "B2 ", "Z ", "Z' ", "Z2 ", "E ", "E' ", "E2 "  };
            notation = new string[shuffleSize + 2]; //Size set to 27 because any less and I get a null reference error
            string text = SetScramble();
            print(text);


            //string[] moves = text.ToUpper().Split(' ');
            yield return new WaitForSeconds(delay);
            manager.defaultRotationSpeed = defaultSpeed;
            manager.sandboxMode = true;
            StartCoroutine(manager.TurnFromScramble(text));
            //manager.TurnFromDefaultScramble(moves);
            yield return new WaitUntil(() => manager.GetCanRotate());
            manager.CanRotate = false;
            manager.sandboxMode = false;

            //IsScrambled = true;
        }
        IsScrambled = true;
        manager.TotalMoves = 0;
    }


    string SetScramble()
    {
        for (int i = 0; i < shuffleSize; i++)
        {
            notation[i] = setX();
            if (i == shuffleSize)
                break;
            notation[i + 1] = setY();
            if (i == shuffleSize)
                break;
            notation[i + 2] = setZ();
            if (i == shuffleSize)
                break;
            i += 2;
        }
        for (int i = 0; i < shuffleSize; i++)
            note += notation[i];

        return note;
    }
    string setX()
    {
        value = x[Random.Range(0, 11)];
        return value;
    }
    string setY()
    {
        value = y[Random.Range(0, 11)];
        return value;
    }
    string setZ()
    {
        value = z[Random.Range(0, 11)];
        return value;
    }
}
