using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMove : MonoBehaviour
{
    Animator animator;
    private float startDelay = 2f;
    private bool isDelay = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
            animator.SetBool("isWalk", true);
            StartCoroutine(goToKingdom());
        }

        
    }

    IEnumerator goToKingdom()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("mapSelectTest");
    }
}
