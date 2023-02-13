using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Puzzle : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
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
        Disable();
        //custom hit area for the Button
        selfImage.alphaHitTestMinimumThreshold = 0.1f;
        //store the Puzzle asked
        PuzzleController.AddPuzzle(this);
        selfText.text = selfBut.name;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        PuzzleController.DisableAllButOne(this);
        position = eventData.position;
        position.z = canvas.planeDistance;
        selfImage.transform.position = canvas.worldCamera.ScreenToWorldPoint(position);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
        position.z = canvas.planeDistance;
        selfImage.color = new Vector4(1f, 1f, 1f, 0.5f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        position = eventData.position;
        position.z = canvas.planeDistance;
        selfImage.transform.position = canvas.worldCamera.ScreenToWorldPoint(position);
        selfImage.color = new Vector4(1f, 1f, 1f, 1f);
        PuzzleController.EnableAll();
    }

    public void Spawn(Vector3 newPos)
    {
        position = newPos;
        position.z = canvas.planeDistance + 10;
        Debug.Log(selfImage.transform.position);
        selfImage.transform.position = position;
        Debug.Log("1: " + selfImage.transform.position);
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