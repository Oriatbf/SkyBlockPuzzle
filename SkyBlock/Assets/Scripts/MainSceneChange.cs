using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainSceneChange : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainSceneClick()
    {
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
