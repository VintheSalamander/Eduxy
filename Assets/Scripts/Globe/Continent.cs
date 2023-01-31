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
    void Start()
    {
        selfimage = this.GetComponent<Image>();
        selfbut = this.GetComponent<Button>();

        //custom hit area for the button
        selfimage.alphaHitTestMinimumThreshold = 0.1f;
        //when clicked do OnClick
        selfbut.onClick.AddListener(OnClick);
        //store the continent asked
        ContiController.AddContinent(this);
    }

    void OnClick()
    {
        //if button clicked has the same name as the text do
        if(this.name.Equals(textC.GetText())){
            //turn the image green
            selfimage.color = new Vector4(0f, 1f, 0f, 1f);
            if(TextController.CompleteCheck())
            {
                StartCoroutine(textC.Completed());
            }else{
                StartCoroutine(textC.GoodAnswer(this));
                answer = textC.GetText();
                this.Disable();
            }
        }else{
            StartCoroutine(textC.IncorrectAnswer(this));
            //turn the image red
            selfimage.color = new Vector4(1f, 0f, 0f, 1f);
            this.Disable();
        }
    }

    public void ResetAll()
    {
        selfimage.color = new Vector4(1f, 1f, 1f, 1f);
        //store new continent asked
        
    }

    public void ResetRed()
    {
        List<string> used = textC.GetUsedCont();
        if(!used.Contains(this.name))
        {
            this.Enable();
            //turn the image back to how it was by the vector that represents white
            selfimage.color = new Vector4(1f, 1f, 1f, 1f);
        }
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