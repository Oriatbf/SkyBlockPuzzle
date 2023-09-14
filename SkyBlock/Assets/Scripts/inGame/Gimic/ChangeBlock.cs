using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeBlock : MonoBehaviour
{
    public int i = 1;
    public Vector3 pos1;
    public Vector3 pos2;
    public LayerMask blockLayer;
    public GameObject ChangeButton;
    public GameObject CancelButton;

    public GameObject Aobj;
    public GameObject Bobj;
    public float time = 0.2f;
    public bool start = false;
    public bool isChange = false;
    public int ChangeCoolStac = 0;
    [SerializeField]
    private bool SelectA = false;
    [SerializeField]
    private bool SelectB = false;

    public PlayerController plconSc;

    private void Awake()
    {
        GameObject plconObj = GameObject.FindGameObjectWithTag("Player");
        plconSc = plconObj.GetComponent<PlayerController>();
        CancelButton = GameObject.Find("CancelButtonUI");
    }
    void Start()
    {
        CancelButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                start = false;
            }
        }
        if (Input.GetMouseButton(0) && i == 1 && !start && isChange && ChangeCoolStac<=0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, blockLayer))
            {
                if (hit.transform.CompareTag("ChangePlatform"))
                {
                    if (Aobj != null)
                    {
                        if(hit.transform.position != Aobj.transform.position
                            && hit.transform.position != Bobj.transform.position)
                        {
                            Aobj.GetComponent<Block>().DefaultMeterial();
                            time = 0.2f;
                            start = true;
                            pos1 = hit.collider.transform.position;
                            Aobj = hit.collider.gameObject;
                            hit.collider.transform.GetComponent<Block>().Click();
                            i += 1;
                            SelectA = true;
                        }
                    }
                    else
                    {
                        time = 0.2f;
                        start = true;
                        pos1 = hit.collider.transform.position;
                        Aobj = hit.collider.gameObject;
                        hit.collider.transform.GetComponent<Block>().Click();
                        i += 1;
                        SelectA = true;
                    }
                }
            }
        }
        if (Input.GetMouseButton(0) && i == 2 && !start && isChange)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, blockLayer))
            {
                if (hit.transform.CompareTag("ChangePlatform"))
                {
                    if (hit.transform.position != Aobj.transform.position)
                    {
                        if(Bobj != null)
                        {
                            if (hit.transform.position != Bobj.transform.position)
                            {
                                Bobj.GetComponent<Block>().DefaultMeterial();
                                time = 0.2f;
                                start = true;
                                pos2 = hit.collider.transform.position;
                                Bobj = hit.collider.gameObject;
                                hit.collider.transform.GetComponent<Block>().Click();
                                i = 1;
                                SelectB = true;
                            }
                        }
                        else
                        {
                            time = 0.2f;
                            start = true;
                            pos2 = hit.collider.transform.position;
                            Bobj = hit.collider.gameObject;
                            hit.collider.transform.GetComponent<Block>().Click();
                            i = 1;
                            SelectB = true;
                        }
                    }
                   
                }
            }
        }


    }
    public void Change()
    {
        if (!isChange && ChangeCoolStac <= 0)
        {
            //PlayerSwipe.isChangeButton = true;
            plconSc.AllMeshFalse();
            StartCoroutine(wait1sec());
        }
        if (isChange && SelectA && SelectB)
        {
            Aobj.GetComponent<Block>().DefaultMeterial();
            Bobj.GetComponent<Block>().DefaultMeterial();
            Aobj.transform.position = pos2;
            Bobj.transform.position = pos1;
           
            i = 1;
            time = 0.2f;
            isChange = false;
            //PlayerSwipe.isChangeButton = false;
            SelectA = false;
            SelectB = false;
            Aobj = null;
            Bobj = null;
            CancelButton.SetActive(false);
            ChangeCoolStac = 3;
            StartCoroutine(wait1sec2());
        }

    }

    IEnumerator wait1sec()
    {
        yield return new WaitForSeconds(0.1f);
        isChange = true;
        CancelButton.SetActive(true);
    }

    IEnumerator wait1sec2()
    {
        yield return new WaitForSeconds(0.1f);
        plconSc.Detect();
    }

    public void Cancel()
    {
        if (Aobj != null)
            Aobj.GetComponent<Block>().DefaultMeterial();
        if (Bobj != null)
            Bobj.GetComponent<Block>().DefaultMeterial();

        time = 0.2f;
        i = 1;
        SelectA = false;
        SelectB = false;
        isChange = false;
        CancelButton.SetActive(false);
        Aobj = null;
        Bobj = null;
        StartCoroutine(wait1sec2());
    }
}
