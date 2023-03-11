using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public bool paused;
    public GameObject PauseMenu;
    public GameObject pauseBut;

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
    public void Resume()
    {
        paused = !paused;
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}
