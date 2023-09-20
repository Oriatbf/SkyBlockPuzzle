using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerMove : MonoBehaviour
{
    public GameObject FadeObj;
    SpriteRenderer sr;
    Animator animator;
    private float startDelay = 1f;
    private bool isDelay = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sr = FadeObj.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        startDelay -= Time.deltaTime;
        if (startDelay <= 0)
        {
            isDelay = false;
        }
    }

    public void Onclick()
    {
        if (!isDelay)
        {
            FadeObj.SetActive(true);
            animator.SetBool("isWalk", true);
            StartCoroutine(goToKingdom());
        }

        
    }

    IEnumerator goToKingdom()
    {
        yield return new WaitForSeconds(1);
        for (float f = 0f; f < 1; f += 0.02f)
        {
            Color c = FadeObj.GetComponent<Image>().color;
            c.a = f;
            FadeObj.GetComponent<Image>().color = c;
            yield return null;
        }

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("ChapterSelect");
    }

    /*
     * public IEnumerator FadeInStart()
{
    FadePannel.SetActive(true);
    for (float f = 1f; f > 0; f -= 0.02f)
    {
        Color c = FadePannel.GetComponent<Image>().color;
        c.a = f;
        FadePannel.GetComponent<Image>().color = c;
        yield return null;
    }
    FadePannel.SetActive(false);
}
     */
}
