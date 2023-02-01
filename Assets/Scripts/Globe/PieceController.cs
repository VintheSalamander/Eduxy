using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PieceController
{
    static private List<Piece> PieceList = new List<Piece>();
    static private List<string> pieces_names = new List<string>();
    //To store all pieces on the piece List
    public static void AddPiece(Piece newCont)
    {
        PieceList.Add(newCont);
    }

    public static List<string> GetNames()
    {
        foreach (Piece p in PieceList){
            pieces_names.Add(p.name);
        }
        return pieces_names;
    }

    //Reset only the pieces that are Red
    public static void ResetRed()
    {
        foreach (Piece p in PieceList){
            if(p.GetState() == Piece.ImgColor.Red){
                p.Reset();
            }
        } 
    }

    public static void ResetAll()
    {
        foreach (Piece p in PieceList){
            p.Reset();
        } 
    }

    public static void DisableAll()
    {
        foreach (Piece p in PieceList){
            p.Disable();
        } 
    }

    public static void EnableAll()
    {
        foreach (Piece p in PieceList){
            p.Enable();
        } 
    }

    public static void EnableWhite()
    {
        foreach (Piece p in PieceList){
            if(p.GetState() == Piece.ImgColor.White){
                p.Enable();
            }
        } 
    }
}
