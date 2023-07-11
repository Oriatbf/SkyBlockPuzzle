using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class dissolve : MonoBehaviour
{
    public Transform pos;
    public Vector3 boxSize;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapBox(pos.position, boxSize);
        foreach(Collider collider in colliders)
        {
            if (collider.tag == "Player")
                Destroy(gameObject, 1f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(pos.position, boxSize);
    }
}
