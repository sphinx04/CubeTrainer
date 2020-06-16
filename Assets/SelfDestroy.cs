using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float delay;
    // Start is called before the first frame update
    void Awake()
    {
        Destroy(gameObject, delay);
    }
}
