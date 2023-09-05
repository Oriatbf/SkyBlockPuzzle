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
    // Start is called before the first frame update
    void Start()
    {
        
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
        if (Input.GetMouseButton(0) && i == 1 && isChange && ChangeCoolStac<=0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, blockLayer))
            {
                if (hit.transform.CompareTag("ChangePlatform"))
                {
                    start = true;
                    pos1 = hit.collider.transform.position;
                    Aobj = hit.collider.gameObject;
                    hit.collider.transform.GetComponent<Block>().Click();
                    i += 1;
                    SelectA = true;
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
                        pos2 = hit.collider.transform.position;
                        Bobj = hit.collider.gameObject;
                        hit.collider.transform.GetComponent<Block>().Click();
                        i += 1;
                        SelectB = true;
                    }
                   
                }
            }
        }


    }
    public void Change()
    {
        if (!isChange)
        {
            //PlayerSwipe.isChangeButton = true;
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
            ChangeCoolStac = 3;
        }

    }

    IEnumerator wait1sec()
    {
        yield return new WaitForSeconds(0.1f);
        isChange = true;
    }
}
