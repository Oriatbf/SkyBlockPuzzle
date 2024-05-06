using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScene_UI : MonoBehaviour
{
    public Image fade;
    bool isFade,playerGo;
    float timer = 0;
    float dur = 0.8f;
    public GameObject player;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFade)
        {
            timer += Time.deltaTime;
            fade.fillAmount = timer/ dur;
            if(fade.fillAmount >= 1)
            {
                isFade = false;
                SceneManager.LoadScene(1);
            }
        }

        if(playerGo)
        {
            player.transform.DOMoveZ(50, 10f);
            animator.SetBool("isWalk", true);
            StartCoroutine(waitMove());
        }
            
    }

    public void goMapSelectScene()
    {
       playerGo= true;
    }

    IEnumerator waitMove()
    {
        yield return new WaitForSeconds(1.5f);
        isFade = true;
    }
}
