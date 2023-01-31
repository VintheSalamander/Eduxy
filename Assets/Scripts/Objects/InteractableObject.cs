using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class InteractableObject : MonoBehaviour
{
    public int sceneBuildIndex;
    private bool inRange;
    void Start(){

    }
    void Update(){
        if(inRange)
            if(Input.GetKeyDown(KeyCode.E)){
                Debug.Log("Scene Loading");
            }
    }
    
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            inRange = true;
            Debug.Log("Player is now in range");
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Player")){
            inRange = false;
            Debug.Log("Player is now not in range");
        }
    }

}
