using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainSceneChange : MonoBehaviour
{
    public Image MainFade;
    private bool isFade = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float lerpSpeed = 4;
        if (isFade)
        {
            MainFade.fillAmount = Mathf.Lerp(MainFade.fillAmount, 1, Time.deltaTime * lerpSpeed);
        }
       
    }

    public void MainSceneClick()
    {
       
        isFade= true;

        StartCoroutine(mainsceneClick());
       
    }

    IEnumerator mainsceneClick()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("mapSelectTest");
    }

    public void kingdomEnter()
    {

        SceneManager.LoadScene("mapSelectTest");
    
    }
    public void kingdomStage1()
    {
        SceneManager.LoadScene("TestScene");
 
    }

    public void NextStage()
    {
        int currentSceneNumber = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneNumber+1);
     

    }

    public void Retry()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
   
    }
}
