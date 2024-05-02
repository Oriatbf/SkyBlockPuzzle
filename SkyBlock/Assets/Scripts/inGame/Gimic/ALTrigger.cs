using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALTrigger : MonoBehaviour
{
    public GameObject linkedArrowLauncher;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            linkedArrowLauncher.GetComponent<ArrowLauncher>().Shoot();
        }
    }
}
