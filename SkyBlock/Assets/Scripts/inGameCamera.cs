using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inGameCamera : MonoBehaviour
{
    public GameObject target;
    public Transform pos;

    // Start is called before the first frame update
    void Start()
    {
        pos = target.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(pos.position.x, 14.6f, pos.position.z-4.7f);

    }
}
