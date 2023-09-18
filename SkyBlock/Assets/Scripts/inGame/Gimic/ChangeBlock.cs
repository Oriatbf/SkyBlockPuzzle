using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeBlock : MonoBehaviour
{

    public int i = 1;
    public Vector3 pos1;
    public Vector3 pos2;
    public LayerMask blockLayer;
    public GameObject ChangeButton;
    public GameObject CancelButton;
    public Image ChangeButtonImage;
    public Image ChangeButtonCoolImage;
    public GameObject ChangeButtonCoolObj;
    public ChangeButtonSprites ChangeButtonSpriteSc;
    public RectTransform ChangeButtonRT;

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
        ChangeButtonImage = GameObject.Find("ChangeButton").GetComponent<Image>();
        ChangeButtonRT = GameObject.Find("ChangeButton").GetComponent<RectTransform>();
        ChangeButtonCoolObj = GameObject.Find("ChangeButtonCool");
        ChangeButtonCoolImage = GameObject.Find("ChangeButtonCool").GetComponent<Image>();
        CancelButton = GameObject.Find("CancelButtonUI");
        ChangeButtonSpriteSc = GameObject.Find("ChangeButton").GetComponent<ChangeButtonSprites>();
    }
    void Start()
    {
        CancelButton.SetActive(false);
        ChangeButtonCoolObj.SetActive(false);
    }

    void FixedUpdate()
    {
        if(ChangeCoolStac == 3)
        {
            ChangeButtonCoolObj.SetActive(true);
            ChangeButtonCoolImage.fillAmount = 1;
        }
        else if(ChangeCoolStac == 2)
        {
            ChangeButtonCoolImage.fillAmount = 0.66f;
        }
        else if (ChangeCoolStac == 1)
        {
            ChangeButtonCoolImage.fillAmount = 0.33f;
        }
        else
        {
            ChangeButtonCoolImage.fillAmount = 0;
            ChangeButtonCoolObj.SetActive(false);
        }
    }

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
        if (Input.GetMouseButton(0) && i == 1 && !start && isChange && ChangeCoolStac<=0 && !EventSystem.current.IsPointerOverGameObject())
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
            ChangeButtonImage.sprite = ChangeButtonSpriteSc.ButtonSprites[0];
            ChangeButtonRT.sizeDelta = new Vector2(220, 262);
            ChangeButtonRT.anchoredPosition = new Vector2(ChangeButtonRT.anchoredPosition.x, -815f);
            ChangeCoolStac = 3;
            StartCoroutine(wait1sec2());
        }

    }

    IEnumerator wait1sec()
    {
        yield return new WaitForSeconds(0.1f);
        isChange = true;
        CancelButton.SetActive(true);
        ChangeButtonImage.sprite = ChangeButtonSpriteSc.ButtonSprites[1];
        ChangeButtonRT.sizeDelta = new Vector2(201, 218);
        ChangeButtonRT.anchoredPosition = new Vector2(ChangeButtonRT.anchoredPosition.x, -824f);
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
        ChangeButtonImage.sprite = ChangeButtonSpriteSc.ButtonSprites[0];
        ChangeButtonRT.sizeDelta = new Vector2(220, 262);
        ChangeButtonRT.anchoredPosition = new Vector2(ChangeButtonRT.anchoredPosition.x, -816f);
        Aobj = null;
        Bobj = null;
        StartCoroutine(wait1sec2());
    }
}
