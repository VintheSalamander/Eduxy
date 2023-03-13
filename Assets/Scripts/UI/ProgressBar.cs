using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public int maximumProg;
    public static int currentProg;
    private ProgState currentState;
    public enum ProgState {Prog1, Prog2, Prog3, Star}
    public Image bar1;
    public Image bar2;
    public Image bar3;
    public GameObject star;
    private Image currentBar;
    // Start is called before the first frame update
    void Start()
    {
        currentProg = 0;
        currentState = 0;
        BarToModify();
        bar1.fillAmount = 0;
        bar2.fillAmount = 0;
        bar3.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState != ProgState.Star){
            GetCurrentFill();
        }  
    }

    void GetCurrentFill()
    {
        float fillQuantity = (float) currentProg / (float) maximumProg;
        currentBar.fillAmount = fillQuantity;
        if(currentProg == maximumProg){
            currentState += 1;
            BarToModify();
        }
        
    }

    void BarToModify(){
        if(currentState == ProgState.Prog1){
            currentBar = bar1;
            star.SetActive(false);
        }else if(currentState == ProgState.Prog2){
            currentBar = bar2;
            star.SetActive(false);
        }else if(currentState == ProgState.Prog3){
            currentBar = bar3;
            star.SetActive(false);
        }else{
            star.SetActive(true);
        }
    }

    public static void AddCurrent()
    {
        currentProg += 1;
    } 

}
