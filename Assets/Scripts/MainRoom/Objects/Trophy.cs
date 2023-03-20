using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trophy : MonoBehaviour
{
    public enum TrophState{Hide, Show}
    private TrophState currentState;
    // Start is called before the first frame update
    void Start()
    {
        if(currentState == TrophState.Hide){
            gameObject.SetActive(false);
        }else{
            this.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    public void EarnedTrophy(string name)
    {
        if(gameObject.name == name){
            currentState = TrophState.Show;
        }
    }
}
