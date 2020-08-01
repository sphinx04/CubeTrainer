using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public List<GameObject> cubes;
    // Start is called before the first frame update

    public void CreateCube(int index)
    {
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
                Debug.Log("DEeeeeeeeeeLETE");
            }
        }
        GameObject cube = Instantiate(cubes[index]);
        cube.transform.SetParent(transform);
    }

    public void Exit() => Application.Quit();
}
