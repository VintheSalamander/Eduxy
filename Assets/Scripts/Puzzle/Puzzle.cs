using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Puzzle : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public PuzTxtController Panel;
    public Collider2D correctPos;
    public TMP_Text selfText;
    private Canvas canvas;
    private Vector3 position;
    private Vector3 initialPos;
    private Image selfImage;
    private Button selfBut;
    private BoxCollider2D selfCol;
    public enum PuzzleState {Colliding, ToMove, InCorrectPos, Correct}
    private PuzzleState currentState;
    private bool stateLock;
   
    void Start()
    {
        selfImage = this.GetComponent<Image>();
        selfBut = this.GetComponent<Button>();
        selfCol = this.GetComponent<BoxCollider2D>();
        canvas = selfImage.canvas;
        SetState(PuzzleState.ToMove);
        Disable();
        //custom hit area for the Button
        selfImage.alphaHitTestMinimumThreshold = 0.1f;
        //store the Puzzle asked
        PuzzleController.AddPuzzle(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(currentState == PuzzleState.Correct){
            return;
        }
        PuzzleController.DisableAllButOne(this);
        position = eventData.position;
        position.z = canvas.planeDistance;
        selfImage.transform.position = canvas.worldCamera.ScreenToWorldPoint(position);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(currentState == PuzzleState.Correct){
            return;
        }
        //To make the object the first in the foreground
        transform.SetAsLastSibling();
        position.z = canvas.planeDistance;
        //To make the object a little bit transparent
        selfImage.color = new Vector4(1f, 1f, 1f, 0.5f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(currentState == PuzzleState.Correct){
            return;
        }else if(currentState == PuzzleState.InCorrectPos){
            this.transform.position = correctPos.transform.position;
            PuzzleController.RemoveUsed(this);
            SetState(PuzzleState.Correct);
            Disable();
            if(PuzzleController.CompleteCheck()){
                StartCoroutine(Panel.Completed());
                ChangeTxtGreen();
            }else{
                StartCoroutine(Panel.GoodAnswer());
                ChangeTxtGreen();
            }
        }else if(currentState == PuzzleState.Colliding){
            StartCoroutine(Panel.IncorrectAnswer());
            Respawn();
        }else{
            position = eventData.position;
            position.z = canvas.planeDistance;
            this.transform.position = canvas.worldCamera.ScreenToWorldPoint(position);
        }
        selfImage.color = new Vector4(1f, 1f, 1f, 1f);
        PuzzleController.EnableAllNotUsed();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6) {
            if (collision == correctPos) {
                SetState(PuzzleState.InCorrectPos);
            }else{
                SetState(PuzzleState.Colliding);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
     {
        if (collision == correctPos) {
            SetState(PuzzleState.ToMove);
        }
    }

    public void Spawn(Vector3 newPos)
    {
        position = newPos;
        position.z = canvas.planeDistance + 10;
        selfImage.transform.position = position;
        initialPos = position;
        ResetTxtColor();
    }

    public void Respawn()
    {
        SetState(PuzzleState.ToMove);
        position = initialPos;
        position.z = canvas.planeDistance + 10;
        selfImage.transform.position = position;
    }

    public void Disable()
    {
        selfBut.interactable = false;
        selfCol.enabled = false;
    }

    public void Enable()
    {
        selfBut.interactable = true;
        selfCol.enabled = true;
    }

    public void SetState(PuzzleState newState) {
        if (stateLock) {
            return; // Enum state is locked, so don't change it
        }
        if(newState == PuzzleState.Correct){
            LockState();
        }
        currentState = newState;
    }

    public void LockState() {
        stateLock = true;
    }

    public void UnlockState() {
        stateLock = false;
        SetState(PuzzleState.ToMove);
    }

    public void ResetTxtColor(){
        selfText.color = Color.white;
    }
    public void ChangeTxtGreen(){
        selfText.color = Color.green;
    }
}