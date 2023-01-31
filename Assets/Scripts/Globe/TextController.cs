using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextController : MonoBehaviour
{
    private TMP_Text selftmp;
    private static List<string> continents = new List<string>()
    {
        "Asia",
        "Europe",
        "Africa",
        "Antartica",
        "North America",
        "South America",
        "Oceania"
    };
    private static List<string> usedContinents = new List<string>();
    private int index;
    private System.Random rnd = new System.Random();
    private string[] pos_messages = new string[] { "Nice", "Good One", "On fire!" };
    private int pos_streak;
    private string[] neg_messages = new string[] { "Try again", "Careful", "Next time!" };
    private int neg_streak;
    
    // Start is called before the first frame update
    void Start()
    {
        selftmp = this.GetComponent<TMP_Text>();

        //choose a random continent and display it 
        index = rnd.Next(continents.Count);
        selftmp.text = continents[index];
    }
    

    public void AskContinent()
    {
        int index = rnd.Next(continents.Count);
        while (usedContinents.Contains(continents[index]))
        {
            index = rnd.Next(continents.Count);
        }
        usedContinents.Add(continents[index]);
        selftmp.text = continents[index];
    }

    public IEnumerator GoodAnswer(Continent c)
    {
        
        //Custom messages depending on the positive streak
        selftmp.text = pos_messages[Mathf.Min(pos_streak, pos_messages.Length - 1)];
        neg_streak = 0;
        pos_streak++;
        ContiController.DisableButtons();
        yield return new WaitForSeconds(1.5f);
        ContiController.EnableButtons();
        this.AskContinent();
        c.Disable();
        ContiController.ResetRedButtons();
    }

    public IEnumerator IncorrectAnswer(Continent c)
    {
        string current_answer = selftmp.text;
        //Custom messages depending on the negative streak
        if(neg_streak == 5)
        {
            selftmp.text = "The lasttt";
        }else{
            selftmp.text = neg_messages[Mathf.Min(neg_streak, neg_messages.Length - 1)];
        }
        neg_streak++;
        pos_streak = 0;
        ContiController.DisableButtons();
        yield return new WaitForSeconds(1.5f);
        ContiController.EnableButtons();
        c.Disable();
        selftmp.text = current_answer;
    }
    public IEnumerator Completed()
    {
        selftmp.text = "Great Jobbb";
        ContiController.DisableButtons();
        yield return new WaitForSeconds(1.5f);
        this.AskContinent();
        ContiController.EnableButtons();
        ContiController.ResetAllButtons();
    }

    public static bool CompleteCheck()
    {
        if (usedContinents.Count == continents.Count){
            usedContinents.Clear();
            return true;
        }
        return false;
    }
    public string GetText()
    {
        return selftmp.text;
    }

    public List<string> GetUsedCont()
    {
        return usedContinents;
    }
}
