using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController
{
    private Vector2 pos;
    static private List<Puzzle> PuzzleList = new List<Puzzle>();
    static private List<Vector3> PosList = new List<Vector3>();
    static private List<string> Puzzles_names = new List<string>();

    public static void SpawnAll(){
        Debug.Log("Check");
        List<Vector3> selectedPos = PosList;
        foreach (Puzzle p in PuzzleList){
            int i = UnityEngine.Random.Range(0, selectedPos.Count);
            Vector3 newPos = selectedPos[i];
            selectedPos.RemoveAt(i);
            p.Spawn(newPos);
        }
    }

    //To store all Puzzles on the Puzzle List
    public static void AddPuzzle(Puzzle newCont)
    {
        PuzzleList.Add(newCont);
        PosList.Add(newCont.transform.position);
    }

    public static List<string> GetNames()
    {
        foreach (Puzzle p in PuzzleList){
            Puzzles_names.Add(p.name);
        }
        return Puzzles_names;
    }

    public static void DisableAll()
    {
        foreach (Puzzle p in PuzzleList){
            p.Disable();
        } 
    }
    public static void DisableAllButOne(Puzzle nothis)
    {
        foreach (Puzzle p in PuzzleList){
            if(p != nothis)
                p.Disable();
        } 
    }

    public static void EnableAll()
    {
        foreach (Puzzle p in PuzzleList){
            p.Enable();
        } 
    }

    public static void HideAllTxt()
    {
        foreach (Puzzle p in PuzzleList){
            p.HideText();
        } 
    }
}
