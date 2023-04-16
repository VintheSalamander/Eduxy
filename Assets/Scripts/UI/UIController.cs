using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public bool paused;
    public bool muted;
    public GameObject PauseMenu;
    public GameObject pauseBut;
    public GameObject volumeBut;
    public GameObject muteBut;
    private static int previousSceneBuildIndex;
    private bool inrange;
    
    private void Start()
    {
        // Retrieve muted state from PlayerPrefs and set initial state
        muted = PlayerPrefs.GetInt("Muted", 0) == 1;
        UpdateMuteState();
    }


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
        if(muted){
            volumeBut.SetActive(false);
            muteBut.SetActive(true);
        }else{
            volumeBut.SetActive(true);
            muteBut.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            paused = !paused;
        }
        if(inrange){

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

    public void Mute_Unmute(){
        muted = !muted;
        UpdateMuteState();
    }

    private void UpdateMuteState()
    {
        // Set mute state and store in PlayerPrefs
        AudioListener.pause = muted;
        PlayerPrefs.SetInt("Muted", muted ? 1 : 0);
    }
}
