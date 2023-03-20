using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trophy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(TrophyController.GetTrophyStatus(this.name)){
            gameObject.SetActive(true);
        }else{
            gameObject.SetActive(false);
        }
    }
}
