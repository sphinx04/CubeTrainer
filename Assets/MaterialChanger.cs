using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Material material;
    private bool canPaint = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && canPaint)
        {
            int layerMask = LayerMask.GetMask("CubeFaces");
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                hit.collider.GetComponent<MeshRenderer>().material = material;
                canPaint = false;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            canPaint = true;
        }
    }
}
