using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class MapButton : MonoBehaviour
{
    [SerializeField]
    public Transform[] buttonPrefab;
    public GameObject StageGoButton;
    public GameObject player;
    private MapSelectPlayerMove Playermove;
    private Camera mCamera;
    public int playerPosition;
    private Animator walkAinm;
    public int selNum;
    private Vector3 direction;


    void Awake()
    {
        walkAinm = GetComponent<Animator>();
    }
    private void Start()
    {
        playerPosition = 0;
        mCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Playermove = GameObject.FindGameObjectWithTag("Player").GetComponent<MapSelectPlayerMove>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mCamera.ScreenPointToRay(Input.mousePosition);
            Debug.Log(ray);
            Physics.Raycast(ray, out hit);
            for (int i = 0; i < buttonPrefab.Length; i++)
            {
                if (hit.transform == buttonPrefab[i])
                {
                    selNum = i;
                }
            }
        }

        else
        {
            walkAinm.SetBool("Walk", false);
        }
        if (playerPosition != selNum)
        {
            StageGoButton.SetActive(true);
            if (playerPosition < selNum)
            {
                Playermove.Settarget(buttonPrefab[playerPosition + 1]);
                direction = buttonPrefab[playerPosition + 1].transform.position - player.transform.position;
                if (Vector3.Distance(transform.position, buttonPrefab[playerPosition + 1].position) < 0.1f)
                {
                    playerPosition++;
                }
            }
            else
            {
                Playermove.Settarget(buttonPrefab[playerPosition - 1]);
                direction = buttonPrefab[playerPosition - 1].transform.position - player.transform.position;
                if (Vector3.Distance(transform.position, buttonPrefab[playerPosition - 1].position) < 0.1f)
                {
                    playerPosition--;
                }
            }
            player.transform.rotation = Quaternion.LookRotation(direction);
            walkAinm.SetBool("Walk", true);
        }
    }

    public void EnterInGame()
    {
        if(selNum == 0)
        {
            SceneManager.LoadScene("TestScene");
        }
        if (selNum == 1)
        {
            SceneManager.LoadScene("TestScene2");
        }
    }
}