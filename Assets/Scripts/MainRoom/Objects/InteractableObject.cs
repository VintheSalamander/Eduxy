using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(BoxCollider2D))]
public class InteractableObject : MonoBehaviour
{
    public int sceneBuildIndex;
    public Sprite highlightedSprite;
    private Sprite basicSprite;
    private bool inRange;
    void Start(){
        basicSprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    void Update(){
        if(inRange){
            if(Input.GetKeyDown(KeyCode.E)){
                SceneManager.LoadScene(sceneBuildIndex);
            }
            this.gameObject.GetComponent<SpriteRenderer>().sprite = highlightedSprite;
        }
        else{
            this.gameObject.GetComponent<SpriteRenderer>().sprite = basicSprite;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Player")){
            inRange = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Player")){
            inRange = false;
        }
    }

}
