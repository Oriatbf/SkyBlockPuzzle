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
        SceneManager.LoadScene("Kingdom Select");
        Player.TurnStac = 0;
    }

    public void kingdomEnter()
    {
        SceneManager.LoadScene("Kingdom Select");
        Player.TurnStac = 0;
    }
    public void kingdomStage1()
    {
        SceneManager.LoadScene("TestScene");
        Player.TurnStac = 0;
    }

    public void NextStage()
    {
        SceneManager.LoadScene("TestScene2");
        Player.TurnStac = 0;
    }

    public void Retry()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        Player.TurnStac = 0;
    }
}
