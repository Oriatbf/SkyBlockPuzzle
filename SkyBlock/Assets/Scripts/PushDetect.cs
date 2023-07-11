using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushDetect : MonoBehaviour
{
    private bool On = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PushBlock") && On)
        {
            PushBlock push = other.gameObject.GetComponent<PushBlock>();
            push.player = gameObject;
            Destroy(gameObject);
            On = false;
        }
    }
}