using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextController : MonoBehaviour
{
    private TMP_Text selftmp;
    private List<string> pieces = new List<string>();
    private List<string> usedPieces = new List<string>();
    private System.Random rnd = new System.Random();
    private string[] pos_messages = new string[] { "Nice", "Good One", "On fire!" };
    private int pos_streak;
    private string[] neg_messages = new string[] { "Try again", "Careful", "You're close" };
    private int neg_streak;
    private bool thelast;

    // Start is called before the first frame update
    void Start()
    {
        selftmp = this.GetComponent<TMP_Text>();
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
        selftmp.text = pieces[index];
    }

    public IEnumerator GoodAnswer()
    {
        
        //Custom messages depending on the positive streak
        if(!thelast){
            selftmp.text = pos_messages[Mathf.Min(pos_streak, pos_messages.Length - 1)];            
            pos_streak++;
        }else{
            selftmp.text = "Next time!";
        }
        thelast = false;
        neg_streak = 0;
        PieceController.DisableAll();
        yield return new WaitForSeconds(1.5f);
        PieceController.ResetRed();
        PieceController.EnableWhite();
        AskPiece();
    }

    public IEnumerator IncorrectAnswer()
    {
        string current_answer = selftmp.text;
        neg_streak++;
        pos_streak = 0;
        //Custom messages depending on the negative streak
        if(neg_streak == 7 - usedPieces.Count)
        {
            selftmp.text = "The lasttt";
            thelast = true;
        }else{
            selftmp.text = neg_messages[Mathf.Min(neg_streak-1, neg_messages.Length - 1)];
        }
        PieceController.DisableAll();
        yield return new WaitForSeconds(1.5f);
        PieceController.EnableWhite();
        selftmp.text = current_answer;
    }
    public IEnumerator Completed()
    {
        selftmp.text = "Great Jobbb";
        PieceController.DisableAll();
        yield return new WaitForSeconds(1.5f);
        AskPiece();
        PieceController.EnableAll();
        PieceController.ResetAll();
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
        selftmp.text = "Welcome";
        yield return new WaitForSeconds(1f);
        pieces = PieceController.GetNames();
        AskPiece();
        PieceController.EnableAll();
    }

    public string GetText()
    {
        return selftmp.text;
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
