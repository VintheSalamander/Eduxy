using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextController : MonoBehaviour
{
    public TMP_Text askText;
    public TMP_Text feedbackText;
    private List<string> pieces = new List<string>();
    private List<string> usedPieces = new List<string>();
    private System.Random rnd = new System.Random();
    private string[] pos_messages = new string[] { "Nice", "Good One", "On fire!" };
    private int pos_streak;
    private string[] neg_messages = new string[] { "Try again", "Careful", "Close" };
    private int neg_streak;
    private bool thelast;

    // Start is called before the first frame update
    void Start()
    {
        askText = askText.GetComponent<TMP_Text>();
        feedbackText = feedbackText.GetComponent<TMP_Text>();
        pos_streak = 0;
        neg_streak = 0;
        thelast = false;
        //We need to wait for all the buttons get passed to the controller
        StartCoroutine(SetUp());
    }
    

    public void AskPiece()
    {
        int index = rnd.Next(pieces.Count);
        while (usedPieces.Contains(pieces[index]))
        {
            index = rnd.Next(pieces.Count);
        }
        usedPieces.Add(pieces[index]);
        askText.text = pieces[index];
    }

    public IEnumerator GoodAnswer()
    {
        
        //Custom messages depending on the positive streak
        if(!thelast){
            feedbackText.text = pos_messages[Mathf.Min(pos_streak, pos_messages.Length - 1)];            
            pos_streak++;
        }else{
            feedbackText.text = "Next time!";
        }
        thelast = false;
        neg_streak = 0;
        PieceController.DisableAll();
        yield return new WaitForSeconds(1.5f);
        PieceController.ResetRed();
        PieceController.EnableWhite();
        AskPiece();
        ProgressBar.AddCurrent();
    }

    public IEnumerator IncorrectAnswer()
    {
        neg_streak++;
        pos_streak = 0;
        //Custom messages depending on the negative streak
        if(neg_streak == pieces.Count - usedPieces.Count)
        {
            feedbackText.text = "The lasttt";
            thelast = true;
        }else{
            feedbackText.text = neg_messages[Mathf.Min(neg_streak-1, neg_messages.Length - 1)];
        }
        PieceController.DisableAll();
        yield return new WaitForSeconds(1.5f);
        PieceController.EnableWhite();
    }
    public IEnumerator Completed()
    {
        feedbackText.text = "Great Jobbb";
        PieceController.DisableAll();
        yield return new WaitForSeconds(1.5f);
        AskPiece();
        PieceController.HideAllTxt();
        PieceController.ResetAll();
        PieceController.EnableAll();
    }

    public bool CompleteCheck()
    {
        if (usedPieces.Count == pieces.Count){
            usedPieces.Clear();
            return true;
        }
        return false;
    }

    private IEnumerator SetUp()
    {
        askText.text = "Continent";
        feedbackText.text = "Welcome";
        yield return new WaitForSeconds(3f);
        pieces = PieceController.GetNames();
        AskPiece();
        PieceController.HideAllTxt();
        PieceController.EnableAll();
        feedbackText.text = "Choose one";
    }

    public string GetText()
    {
        return askText.text;
    }

    public int GetNegStreak()
    {
        return neg_streak;
    }

    public int GetUsedCount()
    {
        return usedPieces.Count;
    }
}
