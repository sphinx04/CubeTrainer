using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInactiveDelayed : MonoBehaviour
{
    public void SetInactive(float t)
    {
        GetComponent<Animator>().SetTrigger("out");
        StartCoroutine(WaitForSec(t));
    }

    private IEnumerator WaitForSec(float t)
    {
        yield return new WaitForSeconds(t);
        gameObject.SetActive(false);
    }
}
