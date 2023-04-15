using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public bool paused;
    public GameObject PauseMenu;
    public GameObject pauseBut;
    public GameObject openE;
    private static int previousSceneBuildIndex;
    private bool inrange;

    // Update is called once per frame
    void Update()
    {
        if(paused){
            PauseMenu.SetActive(true);
            pauseBut.SetActive(false);
            Time.timeScale = 0f;
        }else{
            PauseMenu.SetActive(false);
            pauseBut.SetActive(true);
            Time.timeScale = 1f;
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            paused = !paused;
        }
    }
    public void Resume_Pause()
    {
        paused = !paused;
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        previousSceneBuildIndex = SceneManager.GetActiveScene().buildIndex; 
        SceneManager.LoadScene(0);
    }
    public void Home()
    {
        Time.timeScale = 1f;
        previousSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(1);
    }

    public static int GetPreviousSceneIndex(){
        return previousSceneBuildIndex;
    }
}
