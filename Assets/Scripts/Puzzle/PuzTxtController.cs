using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PuzTxtController : MonoBehaviour
{
    public TMP_Text feedbackText;
    public ProgressBar progressBar;
    private System.Random rnd = new System.Random();
    private string[] pos_messages = new string[] { "Nice", "Good One", "On fire!" };
    private int pos_streak;
    private string[] neg_messages = new string[] { "Try again", "Careful", "Close" };
    private int neg_streak;
    private bool thelast;

    // Start is called before the first frame update
    void Start()
    {
        feedbackText = feedbackText.GetComponent<TMP_Text>();
        pos_streak = 0;
        neg_streak = 0;
        thelast = false;
        //We need to wait for all the buttons get passed to the controller
        StartCoroutine(SetUp());
    }

    private IEnumerator SetUp()
    {
        //Here it iterates through the all puzzles pieces each call of PuzzleController
        //as they are only a few items it does not matter complexity wise
        feedbackText.text = "Welcome";
        yield return new WaitForSeconds(0.01f);
        PuzzleController.SpawnAll();
        PuzzleController.EnableAll();
    }

    public IEnumerator GoodAnswer()
    {
        //Custom messages depending on the positive streak
        if(!thelast){
            feedbackText.text = pos_messages[Mathf.Min(pos_streak, pos_messages.Length - 1)];            
            pos_streak++;
            progressBar.AddCurrent();
        }else{
            feedbackText.text = "Next time!";
        }
        thelast = false;
        neg_streak = 0;
        PuzzleController.DisableAll();
        yield return new WaitForSeconds(1.5f);
    }

    public IEnumerator IncorrectAnswer()
    {
        neg_streak++;
        pos_streak = 0;
        //Custom messages depending on the negative streak
        if(neg_streak == (PuzzleController.GetPuzzlesCount() - PuzzleController.GetUsedPuzCount()))
        {
            feedbackText.text = "The last!";
            thelast = true;
        }else{
            feedbackText.text = neg_messages[Mathf.Min(neg_streak-1, neg_messages.Length - 1)];
        }
        progressBar.MinusCurrent();
        PuzzleController.DisableAll();
        yield return new WaitForSeconds(0.2f);
    }

    public IEnumerator Completed()
    {
        neg_streak = 0;
        pos_streak = 0;
        feedbackText.text = "Great Job";
        PuzzleController.DisableAll();
        progressBar.AddCurrent();
        yield return new WaitForSeconds(1.5f);
        int m = progressBar.CompleteOrReset();
        if(m == 0){
            feedbackText.text = "Need All";
        }else if(m == 1){
            feedbackText.text = "Congrats!";
        }
        PuzzleController.SpawnAll();
        PuzzleController.EnableAll();
        PuzzleController.UnlockAll();
    }

}
