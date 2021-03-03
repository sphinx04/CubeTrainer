using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    public List<Material> materials;
    public List<string> names;
    public Dictionary<string, Material> planes;
    
    
    // Start is called before the first frame update
    private Material material;
    private string name;
    private bool canPaint = true;

    void Start()
    {
        planes = new Dictionary<string, Material>();
        for (int i = 0; i < names.Count; i++)
        {
            planes.Add(names[i], materials[i]);
        }

        name = names[0];
        material = materials[0];
    }

    private void OnEnable()
    {
        planes = new Dictionary<string, Material>();
        for (int i = 0; i < names.Count; i++)
        {
            planes.Add(names[i], materials[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && canPaint && !CameraMovement.instance.Dragging)
        {
            int layerMask = LayerMask.GetMask("CubeFaces");
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                if (hit.collider.transform.parent.name.Length > 1)
                {
                    hit.collider.GetComponent<MeshRenderer>().material = material;
                    hit.collider.transform.name = name;
                    canPaint = false;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            canPaint = true;
        }
    }

    public void SetPainter(string planeName)
    {
        if (planes.TryGetValue(planeName, out var tryMaterial))
        {
            material = tryMaterial;
            name = planeName;
        }
    }

    public void SetCanPaint(bool val) => canPaint = val;
}
