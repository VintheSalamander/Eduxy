using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public float maximumProg;
    public static int targetProg;
    public float transitionSpeed;
    public enum ProgState {Prog1, Prog2, Prog3, Star}
    public Image bar1;
    public Image bar2;
    public Image bar3;
    public GameObject star;
    public GameObject home;
    private static ProgState currentState;
    private Image currentBar;
    private float currentProg;
    

    // Start is called before the first frame update
    void Start()
    {
        currentProg = 0;
        targetProg = 0;
        star.SetActive(false);
        home.SetActive(false);
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
            StarReached();
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
    }

    public static void AddCurrent()
    {
        targetProg += 1;
    }

    public static void MinusCurrent()
    {
        if(targetProg != 0){  
            targetProg -= 1;
        }
    } 

    public static void ResetCurrent()
    {
        if(currentState == ProgState.Prog3){
            targetProg = 0;
        }
    }

}
