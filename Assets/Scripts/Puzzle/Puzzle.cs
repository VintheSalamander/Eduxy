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
    private Vector3 position;
    private Canvas canvas;
    void Start()
    {
        selfImage = this.GetComponent<Image>();
        selfBut = this.GetComponent<Button>();
        selfText = selfText.GetComponent<TMP_Text>();
        canvas = selfImage.canvas;
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
        PuzzleController.DisableDefSpriteRest(this);
        position = eventData.position;
        Debug.Log(canvas.planeDistance);
        position.z = canvas.planeDistance - 5f;
        selfImage.transform.position = canvas.worldCamera.ScreenToWorldPoint(position);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
        selfImage.color = new Vector4(1f, 1f, 1f, 0.5f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        position = eventData.position;
        selfImage.color = new Vector4(1f, 1f, 1f, 1f);
        position.z = canvas.planeDistance;
        selfImage.transform.position = canvas.worldCamera.ScreenToWorldPoint(position);
        PuzzleController.EnableAll();
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
                //StartCoroutine(Panel.Completed());
            }else{
                //StartCoroutine(Panel.GoodAnswer());
            }
        }else{
            //StartCoroutine(Panel.IncorrectAnswer());
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