using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Continent : MonoBehaviour
{
    public TextController textC;
    private string answer;
    private Image selfimage;
    private Button selfbut;
    public enum ImgColor {Red, Green, White}
    public ImgColor currentIC;

    void Start()
    {
        selfimage = this.GetComponent<Image>();
        selfbut = this.GetComponent<Button>();
        currentIC = ImgColor.White;
        //custom hit area for the button
        selfimage.alphaHitTestMinimumThreshold = 0.1f;
        //when clicked do OnClick
        selfbut.onClick.AddListener(OnClick);
        //store the continent asked
        ContsController.AddContinent(this);
    }

    void OnClick()
    {
        //if button clicked has the same name as the text do
        if(this.name.Equals(textC.GetText())){
            //turn the image green
            selfimage.color = new Vector4(0f, 1f, 0f, 1f);
            currentIC = ImgColor.Green;
            if(TextController.CompleteCheck())
            {
                StartCoroutine(textC.Completed());
            }else{
                StartCoroutine(textC.GoodAnswer());
                answer = textC.GetText();
            }
        }else{
            StartCoroutine(textC.IncorrectAnswer());
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