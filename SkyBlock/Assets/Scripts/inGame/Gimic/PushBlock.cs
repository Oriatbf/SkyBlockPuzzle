using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PushBlock : MonoBehaviour
{
    [SerializeField]
    private LayerMask StopBlock;

    public GameObject player;
    public Ease ease;

    [SerializeField]
    private float YRay; //상자라면 0.1, 이동 플랫폼이라면 -0.8
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void blockMove()
    {
        RaycastHit hit;
        if (player.transform.forward == Vector3.right && !Physics.Raycast(transform.position + new Vector3(0, YRay, 0), Quaternion.Euler(0, 270, 0) * new Vector3(0, 0, -1), out hit, 1.2f, StopBlock))
            transform.DOMoveX(transform.position.x + 2f, 1).SetEase(ease);
        if (player.transform.forward == Vector3.left && !Physics.Raycast(transform.position + new Vector3(0, YRay, 0), Quaternion.Euler(0, 90, 0) * new Vector3(0, 0, -1), out hit, 1.2f, StopBlock))
            transform.DOMoveX(transform.position.x - 2f, 1).SetEase(ease);
        if (player.transform.forward == Vector3.forward && !Physics.Raycast(transform.position + new Vector3(0, YRay, 0), Quaternion.Euler(0, 180, 0) * new Vector3(0, 0, -1), out hit, 1.2f, StopBlock))
            transform.DOMoveZ(transform.position.z + 2f, 1).SetEase(ease);
        if (player.transform.forward == Vector3.back && !Physics.Raycast(transform.position + new Vector3(0, YRay, 0), Quaternion.Euler(0, 0, 0) * new Vector3(0, 0, -1), out hit, 1.2f, StopBlock))
            transform.DOMoveZ(transform.position.z - 2f, 1).SetEase(ease);


    }
}
