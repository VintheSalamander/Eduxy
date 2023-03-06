using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController
{
    private Vector2 pos;
    static private List<Puzzle> PuzzleList = new List<Puzzle>();
    static private List<Vector3> PosList = new List<Vector3>();
    static private List<string> Puzzles_names = new List<string>();
    static private List<Puzzle> notUsedPList = new List<Puzzle>();

    public static void SpawnAll(){
        List<Vector3> selectedPos = new List<Vector3>(PosList);
        foreach (Puzzle p in PuzzleList){
            int i = UnityEngine.Random.Range(0, selectedPos.Count);
            Vector3 newPos = selectedPos[i];
            selectedPos.RemoveAt(i);
            p.Spawn(newPos);
            notUsedPList.Add(p);
        }
    }

    //To store all Puzzles on the Puzzle List
    public static void AddPuzzle(Puzzle newPuz)
    {
        PuzzleList.Add(newPuz);
        PosList.Add(newPuz.transform.position);
    }

    public static void RemoveUsed(Puzzle remPuz)
    {
        notUsedPList.Remove(remPuz);
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

    public static void EnableAllNotUsed()
    {
        foreach (Puzzle p in notUsedPList){
            p.Enable();
        } 
    }

    public static void UnlockAll(){
        foreach (Puzzle p in PuzzleList){
            p.UnlockState();
        } 
    }

    public static bool CompleteCheck(){
        if(notUsedPList.Count == 0){
            return true;
        }
        return false;
    }
    public static int GetPuzzlesCount(){
        return PuzzleList.Count;
    }

    public static int GetUsedPuzCount(){
        return (PuzzleList.Count - notUsedPList.Count);
    }
}
