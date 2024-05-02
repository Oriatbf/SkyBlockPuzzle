using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLauncher : MonoBehaviour
{
    public GameObject arrow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        GameObject a = Instantiate(arrow,transform.position,transform.rotation);
        a.GetComponent<Rigidbody>().AddForce(transform.forward * 40f,ForceMode.Impulse);
        Destroy(a,.7f);
    }
}
