using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Piece : MonoBehaviour
{
    public TextController controlText;
    private string answer;
    private Image selfimage;
    private Button selfbut;
    public enum ImgColor {White, Green, Red, Yellow, Gray}
    private ImgColor currentIC;

    void Start()
    {
        selfimage = this.GetComponent<Image>();
        selfbut = this.GetComponent<Button>();
        this.Disable();
        currentIC = ImgColor.White;
        //custom hit area for the button
        selfimage.alphaHitTestMinimumThreshold = 0.1f;
        //when clicked do OnClick
        selfbut.onClick.AddListener(OnClick);
        //store the piece asked
        PieceController.AddPiece(this);
    }

    void OnClick()
    {
        //if button clicked has the same name as the text do
        if(this.name.Equals(controlText.GetText())){
            //turn the image green
            int neg_streak = controlText.GetNegStreak();
            if(neg_streak == 0)
            {
                selfimage.color = new Vector4(0f, 1f, 0f, 1f);
                currentIC = ImgColor.Green;
            }else if(neg_streak != 7 - controlText.GetUsedCount())
            {
                selfimage.color = new Vector4(1f, 0.92f, 0.016f, 1f);
                currentIC = ImgColor.Yellow;
            }else{
                selfimage.color = new Vector4(0.5f, 0.5f, 0.5f, 1f);
                currentIC = ImgColor.Gray;
            }
            if(controlText.CompleteCheck())
            {
                StartCoroutine(controlText.Completed());
            }else{
                StartCoroutine(controlText.GoodAnswer());
                answer = controlText.GetText();
            }
        }else{
            StartCoroutine(controlText.IncorrectAnswer());
            //turn the image red
            selfimage.color = new Vector4(1f, 0f, 0f, 1f);
            currentIC = ImgColor.Red;
        }
    }
    
    public ImgColor GetState(){
        return currentIC;
    }

    public void Reset()
    {
        selfimage.color = new Vector4(1f, 1f, 1f, 1f);
        currentIC = ImgColor.White;
    }

    public void Disable()
    {
        selfbut.interactable = false;
    }

    public void Enable()
    {
        selfbut.interactable = true;
    }
}