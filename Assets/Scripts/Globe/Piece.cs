using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Piece : MonoBehaviour
{
    public TextController Panel;
    public TMP_Text selfText;
    private string answer;
    private Image selfImage;
    private Button selfBut;

    //White and Red are the relevant values needed for PieceController
    //GYO represents when the piece is Green, Yellow or Orange, where tracking is no really needed 
    public enum ImgColor {White, Red, GYO}
    private ImgColor currentIC;

    void Start()
    {
        selfImage = this.GetComponent<Image>();
        selfBut = this.GetComponent<Button>();
        selfText = selfText.GetComponent<TMP_Text>();
        this.Disable();
        this.ShowText();
        currentIC = ImgColor.White;
        //custom hit area for the Button
        selfImage.alphaHitTestMinimumThreshold = 0.1f;
        //when clicked do OnClick
        selfBut.onClick.AddListener(OnClick);
        //store the piece asked
        PieceController.AddPiece(this);
        selfText.text = selfBut.name;
    }

    void OnClick()
    {
        //if Button clicked has the same name as the text do
        if(this.name.Equals(Panel.GetText())){
            //turn the image green
            int neg_streak = Panel.GetNegStreak();
            if(neg_streak == 0)
            {
                selfImage.color = new Vector4(0f, 1f, 0f, 1f);
            }else if(neg_streak != 7 - Panel.GetUsedCount())
            {
                selfImage.color = new Vector4(1f, 0.92f, 0.016f, 1f);
            }else{
                selfImage.color = new Vector4(1f, 0.5f, 0f, 1f);
            }
            currentIC = ImgColor.GYO;
            this.ShowText();
            if(Panel.CompleteCheck())
            {
                StartCoroutine(Panel.Completed());
            }else{
                StartCoroutine(Panel.GoodAnswer());
                answer = Panel.GetText();
            }
        }else{
            StartCoroutine(Panel.IncorrectAnswer());
            //turn the image red
            selfImage.color = new Vector4(1f, 0f, 0f, 1f);
            currentIC = ImgColor.Red;
        }
    }
    
    public ImgColor GetState(){
        return currentIC;
    }

    public void Reset()
    {
        selfImage.color = new Vector4(1f, 1f, 1f, 1f);
        currentIC = ImgColor.White;
    }

    public void Disable()
    {
        selfBut.interactable = false;
    }

    public void Enable()
    {
        selfBut.interactable = true;
    }

    public void ShowText()
    {
        selfText.gameObject.SetActive(true);
    }

    public void HideText()
    {
       selfText.gameObject.SetActive(false); 
    }
    
}