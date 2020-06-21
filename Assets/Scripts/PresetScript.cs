using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetScript : MonoBehaviour
{
    public string text;
    public CubeManager manager;
    public float delay = 1f;
    public int defaultSpeed;

    private string[] notation;
    private string[] x;
    private string[] y;
    private string[] z;
    private string value;
    private string note;
    int size = 50;


    IEnumerator Start()
    {
        x = new string[] { "R ", "R' ", "R2 ", "L ", "L' ", "L2 " };
        y = new string[] { "U ", "U' ", "U2 ", "D ", "D' ", "D2 " };
        z = new string[] { "F ", "F' ", "F2 ", "B ", "B' ", "B2 " };
        notation = new string[size + 2]; //Size set to 27 because any less and I get a null reference error
        text = setScramble();
        print(text);


        //string[] moves = text.ToUpper().Split(' ');
        string[] moves = text.ToUpper().Split(' ');
        yield return new WaitForSeconds(delay);
        manager.defaultRotationSpeed = defaultSpeed;
        StartCoroutine(manager.TurnFromScramble(moves));
        //manager.TurnFromDefaultScramble(moves);
        yield return new WaitUntil(() => manager.GetCanRotate());
        manager.SetCanRotate(false);
        manager.isScrambled = true;
    }


    string setScramble()
    {
        for (int i = 0; i < size; i++)
        {
            notation[i] = setX();
            if (i == size)
                break;
            notation[i + 1] = setY();
            if (i == size)
                break;
            notation[i + 2] = setZ();
            if (i == size)
                break;
            i += 2;
        }
        for (int i = 0; i < size; i++) //Adds each string to the end of the current string
        {
            note += notation[i];
        }
        return note;
    }
    //Sets value for the X axis
    string setX()
    {
        value = x[Random.Range(0, 5)];
        return value;
    }
    //Sets value for the Y axis
    string setY()
    {
        value = y[Random.Range(0, 5)];
        return value;
    }
    //Sets value for the Z axis
    string setZ()
    {
        value = z[Random.Range(0, 5)];
        return value;
    }
}
