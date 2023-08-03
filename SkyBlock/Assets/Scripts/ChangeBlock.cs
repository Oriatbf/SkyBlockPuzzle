using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBlock : MonoBehaviour
{
    public int i = 1;
    public Vector3 pos1;
    public Vector3 pos2;
    public LayerMask blockLayer;

    public GameObject Aobj;
    public GameObject Bobj;
    public float time = 0.2f;
    public bool start = false;
    public bool isChange = false;
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
        if (Input.GetMouseButton(0) && i == 1 && isChange)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, blockLayer))
            {
                start = true;
                pos1 = hit.collider.transform.position;
                Aobj = hit.collider.gameObject;
                i += 1;
            }
        }
        if (Input.GetMouseButton(0) && i == 2 && !start && isChange)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, blockLayer))
            {
                pos2 = hit.collider.transform.position;
                Bobj = hit.collider.gameObject;
                i += 1;
            }
        }


    }
    public void Change()
    {
        if (!isChange)
        {
            PlayerSwipe.isChangeButton = true;
            StartCoroutine(wait1sec());
        }
        if (isChange)
        {
            Aobj.transform.position = pos2;
            Bobj.transform.position = pos1;
            i = 1;
            time = 0.2f;
            isChange = false;
            PlayerSwipe.isChangeButton = false;
        }

    }

    IEnumerator wait1sec()
    {
        yield return new WaitForSeconds(1f);
        isChange = true;
    }
}
