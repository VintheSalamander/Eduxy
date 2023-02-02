using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PuzTxtController : MonoBehaviour
{
    public TMP_Text askText;
    public TMP_Text feedbackText;
    private List<string> puzzles = new List<string>();
    private List<string> usedPuzzles = new List<string>();
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
    

    public void AskPuzzle()
    {
        int index = rnd.Next(puzzles.Count);
        while (usedPuzzles.Contains(puzzles[index]))
        {
            index = rnd.Next(puzzles.Count);
        }
        usedPuzzles.Add(puzzles[index]);
        askText.text = puzzles[index];
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
        PuzzleController.DisableAll();
        yield return new WaitForSeconds(1.5f);
        AskPuzzle();
    }

    public IEnumerator IncorrectAnswer()
    {
        neg_streak++;
        pos_streak = 0;
        //Custom messages depending on the negative streak
        if(neg_streak == 7 - usedPuzzles.Count)
        {
            feedbackText.text = "The lasttt";
            thelast = true;
        }else{
            feedbackText.text = neg_messages[Mathf.Min(neg_streak-1, neg_messages.Length - 1)];
        }
        PuzzleController.DisableAll();
        yield return new WaitForSeconds(1.5f);
    }
    public IEnumerator Completed()
    {
        feedbackText.text = "Great Jobbb";
        PuzzleController.DisableAll();
        yield return new WaitForSeconds(1.5f);
        AskPuzzle();
        PuzzleController.HideAllTxt();
        PuzzleController.RespawnAll();
        PuzzleController.EnableAll();
    }

    public bool CompleteCheck()
    {
        if (usedPuzzles.Count == puzzles.Count){
            usedPuzzles.Clear();
            return true;
        }
        return false;
    }

    private IEnumerator SetUp()
    {
        askText.text = "Continent";
        feedbackText.text = "Welcome";
        yield return new WaitForSeconds(3f);
        puzzles = PuzzleController.GetNames();
        AskPuzzle();
        PuzzleController.HideAllTxt();
        PuzzleController.EnableAll();
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
        return usedPuzzles.Count;
    }
}
