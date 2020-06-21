using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDurationSetter : MonoBehaviour
{
    public CubeManager manager;
    Animator animator;
    // Start is called before the first frame update

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void ExitAnimation()
    {
        animator.SetTrigger("Exit");
        Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.isScrambled)
        {
            ExitAnimation();
        }
    }
}
