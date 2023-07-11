using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PushBlock : MonoBehaviour
{
    public GameObject player;
    public Ease ease;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void blockMove()
    {
        if (player.transform.forward == Vector3.right)
            transform.DOMoveX(transform.position.x + 2.006f, 1).SetEase(ease);
        if (player.transform.forward == Vector3.left)
            transform.DOMoveX(transform.position.x - 2.006f, 1).SetEase(ease);
        if (player.transform.forward == Vector3.forward)
            transform.DOMoveZ(transform.position.z + 2.006f, 1).SetEase(ease);
        if (player.transform.forward == Vector3.back)
            transform.DOMoveZ(transform.position.z - 2.006f, 1).SetEase(ease);


    }
}
