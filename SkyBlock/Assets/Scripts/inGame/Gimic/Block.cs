using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private int i;
    public Material ClickMaterial;
    public Material[] defaultMaterials;
    public Material[] realDefault;
    Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        defaultMaterials = renderer.materials;
        realDefault= renderer.materials;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        Material[] materials = defaultMaterials;

        for (i = 0; i < materials.Length; i++)
        {
            if (materials[i].name == "tree2 (Instance)")
            {
                materials[i] = ClickMaterial;
            }
        }

        renderer.materials = materials;
    }

    public void DefaultMeterial()
    {
        renderer.materials = realDefault;
    }
}
