using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresetScript : MonoBehaviour
{
    public string text;
    public CubeManager manager;
    public float delay = 1f;
    public int defaultSpeed;

    //String arrays to hold the notations for each axis
    //and the scramble as a whole
    private string[] notation;
    private string[] x;
    private string[] y;
    private string[] z;
    private string value;
    private string note;



    //private void Awake()
    //{
    //    string[] moves = text.ToUpper().Split(' ');
    //    manager.SetDefaultRotationSpeed(90);
    //    StartCoroutine(manager.TurnFromScramble(moves));
    //    manager.SetDefaultRotationSpeed(2);
    //}
    IEnumerator Start()
    {
        x = new string[] { "R ", "R' ", "R2 ", "L ", "L' ", "L2 " };
        y = new string[] { "U ", "U' ", "U2 ", "D ", "D' ", "D2 " };
        z = new string[] { "F ", "F' ", "F2 ", "B ", "B' ", "B2 " };
        notation = new string[27]; //Size set to 27 because any less and I get a null reference error
        text = setScramble();
        print(text);


        //string[] moves = text.ToUpper().Split(' ');
        string[] moves = text.ToUpper().Split(' ');
        yield return new WaitForSeconds(delay);
        manager.defaultRotationSpeed = 90;// defaultSpeed;
        StartCoroutine(manager.TurnFromScramble(moves));
        //manager.TurnFromDefaultScramble(moves);
        yield return new WaitUntil(() => manager.GetCanRotate());
        manager.SetCanRotate(false);




    }


    string setScramble()
    {
        for (int i = 0; i < 25; i++)
        {
            notation[i] = setX();
            if (i == 25)
                break;
            notation[i + 1] = setY();
            if (i == 25)
                break;
            notation[i + 2] = setZ();
            if (i == 25)
                break;
            i += 2;
        }
        for (int i = 0; i < 25; i++) //Adds each string to the end of the current string
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
