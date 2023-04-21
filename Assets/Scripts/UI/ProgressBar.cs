using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public float maximumProg;
    public static int targetProg;
    public float transitionSpeed;
    public string trophyName;
    public enum ProgState {Prog1, Prog2, Prog3, Star}
    public Image bar1;
    public Image bar2;
    public Image bar3;
    public GameObject star;
    public GameObject home;
    private static ProgState currentState;
    private Image currentBar;
    private float currentProg;
    private SoundController soundController;
    

    // Start is called before the first frame update
    void Start()
    {
        soundController = GetComponent<SoundController>();
        currentProg = 0;
        targetProg = 0;
        star.SetActive(false);
        home.SetActive(false);
        currentState = TrophyController.GetTrophyStatus(trophyName);
        if(currentState == ProgState.Prog1){
            bar1.fillAmount = 0;
            bar2.fillAmount = 0;
            bar3.fillAmount = 0;
            currentBar = bar1;
        }else if(currentState == ProgState.Prog2){
            bar1.fillAmount = maximumProg;
            bar2.fillAmount = 0;
            bar3.fillAmount = 0;
            currentBar = bar2;
        }else if(currentState == ProgState.Prog3){
            bar1.fillAmount = maximumProg;
            bar2.fillAmount = maximumProg;
            bar3.fillAmount = 0;
            currentBar = bar3;
        }else{
            star.SetActive(true);
            home.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState != ProgState.Star){
            currentProg = Mathf.Lerp(currentProg, targetProg, transitionSpeed * Time.deltaTime);
            GetCurrentFill();
        }  
    }

    
    void GetCurrentFill()
    {
        float fillQuantity =  currentProg / maximumProg;
        currentBar.fillAmount = fillQuantity;
        if(currentProg >= maximumProg-0.05f){
            currentProg = 0;
            targetProg = 0;
            currentState += 1;
            BarToModify();
        }
    }

    void BarToModify(){
        if(currentState == ProgState.Prog1){
            currentBar = bar1;
        }else if(currentState == ProgState.Prog2){
            currentBar = bar2;
        }else if(currentState == ProgState.Prog3){
            currentBar = bar3;
        }else{
            StarReached();
        }
    }

    void StarReached(){
        star.SetActive(true);
        home.SetActive(true);
        soundController.ApplauseSound();
    }

    public void AddCurrent()
    {
        targetProg += 1;
        soundController.CorrectSound();
    }

    public void MinusCurrent()
    {
        if(targetProg != 0){  
            targetProg -= 1;
        }
        soundController.WrongSound();
    } 

    public int CompleteOrReset()
    {
        if(currentState == ProgState.Prog3){
            targetProg = 0;
            return 0;
        }else if(currentState == ProgState.Star){
            return 1;
        }
        return 2;
    }

    public static ProgState GetCurrentState(){
        return currentState;
    }
}
