using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PushBlock : MonoBehaviour
{
    [SerializeField]
    private LayerMask StopBlock;
    [SerializeField]
    private LayerMask PushBlockLayer;
    [SerializeField]
    private LayerMask MoveTile;

    public GameObject player;
    public GameObject ClickPoint;
    public GameObject parentObject;
    public Ease ease;
    public static bool DetetcON;

    RaycastHit hit;
    RaycastHit hitt;

    [SerializeField]
    private float YRay;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ClickPoint.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        RaycastHit hittt;
        if (Physics.Raycast(transform.position, Vector3.up, out hittt, 2f, MoveTile))
        {
            MeshRenderer detectMeshRenderer = hittt.collider.GetComponent<MeshRenderer>();
            if (detectMeshRenderer.enabled == true)
                hittt.collider.transform.parent = parentObject.transform;
            else
                hittt.collider.transform.parent = gameObject.transform;
        }

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && ClickPoint.gameObject.activeSelf)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitt, Mathf.Infinity, PushBlockLayer))
            {
                Vector3 lookDirection = transform.position - player.transform.position;
                lookDirection.y = 0f;
                player.transform.rotation = Quaternion.LookRotation(lookDirection);

                blockMove();
            }
        }
    }

    public void blockMove()
    {
        if (player.transform.forward == Vector3.right && !Physics.Raycast(transform.position + new Vector3(0, YRay, 0), Quaternion.Euler(0, 270, 0) * new Vector3(0, 0, -1), out hit, 1.2f, StopBlock))
        {
            transform.DOMoveX(transform.position.x + 2f, 1).SetEase(ease);
            ClickPoint.gameObject.SetActive(false);
            PlayerController.timer = 1f;
            if (Physics.Raycast(hitt.transform.position + Vector3.up, Vector3.down, out hit, 2f, MoveTile))
            {
                MeshRenderer detectMeshRenderer = hit.collider.GetComponent<MeshRenderer>();
                detectMeshRenderer.enabled = true;
            }
        }
        if (player.transform.forward == Vector3.left && !Physics.Raycast(transform.position + new Vector3(0, YRay, 0), Quaternion.Euler(0, 90, 0) * new Vector3(0, 0, -1), out hit, 1.2f, StopBlock))
        {
            transform.DOMoveX(transform.position.x - 2f, 1).SetEase(ease);
            ClickPoint.gameObject.SetActive(false);
            PlayerController.timer = 1f;
            if (Physics.Raycast(hitt.transform.position + Vector3.up, Vector3.down, out hit, 2f, MoveTile))
            {
                MeshRenderer detectMeshRenderer = hit.collider.GetComponent<MeshRenderer>();
                detectMeshRenderer.enabled = true;
            }
        }
        if(player.transform.forward == Vector3.forward && !Physics.Raycast(transform.position + new Vector3(0, YRay, 0), Quaternion.Euler(0, 180, 0) * new Vector3(0, 0, -1), out hit, 1.2f, StopBlock))
        {
            transform.DOMoveZ(transform.position.z + 2f, 1).SetEase(ease);
            ClickPoint.gameObject.SetActive(false);
            PlayerController.timer = 1f;
            if (Physics.Raycast(hitt.transform.position + Vector3.up, Vector3.down, out hit, 2f, MoveTile))
            {
                MeshRenderer detectMeshRenderer = hit.collider.GetComponent<MeshRenderer>();
                detectMeshRenderer.enabled = true;
            }
        }
        if (player.transform.forward == Vector3.back && !Physics.Raycast(transform.position + new Vector3(0, YRay, 0), Quaternion.Euler(0, 0, 0) * new Vector3(0, 0, -1), out hit, 1.2f, StopBlock))
        {
            transform.DOMoveZ(transform.position.z - 2f, 1).SetEase(ease);
            ClickPoint.gameObject.SetActive(false);
            PlayerController.timer = 1f;
            if (Physics.Raycast(hitt.transform.position + Vector3.up, Vector3.down, out hit, 2f, MoveTile))
            {
                MeshRenderer detectMeshRenderer = hit.collider.GetComponent<MeshRenderer>();
                detectMeshRenderer.enabled = true;
            }
        }
    }

    void MoveTIleRegen()
    { 
    }
}
