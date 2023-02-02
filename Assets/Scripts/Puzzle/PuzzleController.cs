using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController
{
    private float MinX1, MinX2, MinY1, MinY2, MaxX1, MaxX2, MaxY1, MaxY2;
    static private List<Puzzle> PuzzleList = new List<Puzzle>();
    static private List<string> Puzzles_names = new List<string>();
    //To store all Puzzles on the Puzzle List
    public static void AddPuzzle(Puzzle newCont)
    {
        PuzzleList.Add(newCont);
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
    public static void DisableRest(Puzzle notdis)
    {
        foreach (Puzzle p in PuzzleList){
            if(p != notdis)
                p.Disable();
        } 
    }

    public static void EnableAll()
    {
        foreach (Puzzle p in PuzzleList){
            p.Enable();
        } 
    }

    public static void RespawnAll()
    {
        foreach (Puzzle p in PuzzleList){
            p.Spawn();
        } 
    }

    public static void HideAllTxt()
    {
        foreach (Puzzle p in PuzzleList){
            p.HideText();
        } 
    }
}
