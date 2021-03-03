using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCubeState : MonoBehaviour
{
    public static ReadCubeState instance;

    public bool IsUseAutoSolve;
    public Transform tUp;
    public Transform tDown;
    public Transform tLeft;
    public Transform tRight;
    public Transform tFront;
    public Transform tBack;

    private List<Transform> tPoses = new List<Transform>();
    private int layerMask = 1 << 9;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        tPoses.Add(tUp);
        tPoses.Add(tRight);
        tPoses.Add(tFront);
        tPoses.Add(tDown);
        tPoses.Add(tLeft);
        tPoses.Add(tBack);
        //if (IsUseAutoSolve)
        //{
        //    tUp.parent.SetParent(transform);
        //}
    }
    void Update()
    {

    }

    public bool IsSolved()
    {
        bool solved = true;
        foreach (Transform tSide in tPoses)
        {
            solved = solved && IsSideSolved(tSide);
            //            print(IsSideSolved(tSide));
        }
        return solved;
    }

    bool IsSideSolved(Transform tSide)
    {
        List<GameObject> facesHit = GetSideString(tSide);

        bool solved = true;
        for (int i = 0; i < facesHit.Count - 1; i++)
        {
            //solved &= facesHit[facesHit.Count - 1].name == facesHit[i].name;
            solved &= facesHit[facesHit.Count - 1].GetComponent<MeshRenderer>().material.name == facesHit[i].GetComponent<MeshRenderer>().material.name;
            //print(facesHit[i].GetComponent<MeshRenderer>().material.name);
        }
        return solved;
    }

    private List<GameObject> GetSideString(Transform tSide)
    {
        List<GameObject> facesHit = new List<GameObject>();
        RaycastHit hit;
        for (int i = 0; i < tSide.childCount; i++)
        {
            //Если объект луча в иерархии не активен проверка не производится
            if (tSide.GetChild(i).gameObject.activeInHierarchy)
            {
                Vector3 ray = tSide.GetChild(i).transform.position;
                if (Physics.Raycast(ray, tSide.GetChild(i).right, out hit, Mathf.Infinity, layerMask))
                {
                    Debug.DrawRay(ray, tSide.GetChild(i).right * hit.distance, Color.yellow);
                    facesHit.Add(hit.collider.gameObject);
                    //print(hit.collider.gameObject.name);
                }
                else
                {
                    Debug.DrawRay(ray, tSide.GetChild(i).right * 1000, Color.green);
                }
            }
        }
        return facesHit;
    }

    public string GetString()
    {
        string searchString = "";
        foreach (Transform tSide in tPoses)
        {
            List<GameObject> facesHit = GetSideString(tSide);
            foreach (GameObject item in facesHit)
            {
                searchString += item.name;
            }
        }
        print(searchString);
        return searchString;
    }
}