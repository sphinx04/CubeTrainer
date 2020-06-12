using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCubeState : MonoBehaviour
{
    public Transform tUp;
    public Transform tDown;
    public Transform tLeft;
    public Transform tRight;
    public Transform tFront;
    public Transform tBack;

    private List<Transform> tPoses = new List<Transform>();
    private int layerMask = 1 << 9;
    // Start is called before the first frame update
    void Start()
    {
        tPoses.Add(tUp);
        tPoses.Add(tDown);
        tPoses.Add(tLeft);
        tPoses.Add(tRight);
        tPoses.Add(tFront);
        tPoses.Add(tBack);
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
            print(IsSideSolved(tSide));
        }
        return solved;
    }

    bool IsSideSolved(Transform tSide)
    {
        List<GameObject> facesHit = new List<GameObject>();
        RaycastHit hit;
        for (int i = 0; i < 9; i++)
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
        return facesHit[0].name == facesHit[1].name &&
               facesHit[0].name == facesHit[2].name &&
               facesHit[0].name == facesHit[3].name &&
               facesHit[0].name == facesHit[4].name &&
               facesHit[0].name == facesHit[5].name &&
               facesHit[0].name == facesHit[6].name &&
               facesHit[0].name == facesHit[7].name &&
               facesHit[0].name == facesHit[8].name;
    }
}