using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool muted;
    public GameObject volumeBut;
    public GameObject muteBut;
    private static int previousSceneBuildIndex;
    private bool inrange;
    private SoundController soundController;
    
    private void Start()
    {
        soundController = GetComponent<SoundController>();
        // Retrieve muted state from PlayerPrefs and set initial state
        muted = PlayerPrefs.GetInt("Muted", 0) == 1;
        UpdateMuteState();
    }

    // Update is called once per frame
    void Update()
    {
        if(muted){
            volumeBut.SetActive(false);
            muteBut.SetActive(true);
        }else{
            volumeBut.SetActive(true);
            muteBut.SetActive(false);
        }
    }
    public void Mute_Unmute(){
        soundController.ClickSound();
        muted = !muted;
        UpdateMuteState();
    }

    private void UpdateMuteState()
    {
        // Set mute state and store in PlayerPrefs
        AudioListener.pause = muted;
        PlayerPrefs.SetInt("Muted", muted ? 1 : 0);
    }

    public void Home()
    {
        soundController.ClickSound();
        SceneManager.LoadScene(1);
    }

    public void QuitGame(){
        soundController.ClickSound();
        Application.Quit();
    }
}
