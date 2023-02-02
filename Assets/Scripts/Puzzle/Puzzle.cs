using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Puzzle : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector2 pos;
    public PuzTxtController Panel;
    public TMP_Text selfText;
    private string answer;
    private Image selfImage;
    private Button selfBut;

    void Start()
    {
        selfImage = this.GetComponent<Image>();
        selfBut = this.GetComponent<Button>();
        selfText = selfText.GetComponent<TMP_Text>();
        this.Disable();
        //custom hit area for the Button
        selfImage.alphaHitTestMinimumThreshold = 0.1f;
        //when clicked do OnClick
        selfBut.onClick.AddListener(OnClick);
        //store the Puzzle asked
        PuzzleController.AddPuzzle(this);
        selfText.text = selfBut.name;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void Spawn()
    {

    }

    void OnClick()
    {
        //if Button clicked has the same name as the text do
        if(this.name.Equals(Panel.GetText())){
            if(Panel.CompleteCheck())
            {
                StartCoroutine(Panel.Completed());
            }else{
                StartCoroutine(Panel.GoodAnswer());
            }
        }else{
            StartCoroutine(Panel.IncorrectAnswer());
        }
    }

    public void Disable()
    {
        selfBut.interactable = false;
    }

    public void Enable()
    {
        selfBut.interactable = true;
    }    

    public void HideText()
    {
        selfText.gameObject.SetActive(false);
    }
}