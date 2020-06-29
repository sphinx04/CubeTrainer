using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnCubeHolder : MonoBehaviour
{
    public string allCubeHolderName;
    public int thisCubePrefabIndex;

    public void Respawn()
    {
        GameObject.Find(allCubeHolderName).GetComponent<CubeSpawner>().CreateCube(thisCubePrefabIndex);
    }

}
