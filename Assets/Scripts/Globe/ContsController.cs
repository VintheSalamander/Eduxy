using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ContsController
{
    static private List<Continent> ContiList = new List<Continent>();

    //To store all continents on the continent List
    public static void AddContinent(Continent newCont)
    {
        ContiList.Add(newCont);
    }

    //To Notify all other buttons that one has been pressed
    public static void ResetRed()
    {
        foreach (Continent c in ContiList){
            if(c.GetState() == Continent.ImgColor.Red){
                c.Reset();
            }
        } 
    }

    public static void ResetAll()
    {
        foreach (Continent c in ContiList){
            c.Reset();
        } 
    }

    public static void DisableAll()
    {
        foreach (Continent c in ContiList){
            c.Disable();
        } 
    }

    public static void EnableAll()
    {
        foreach (Continent c in ContiList){
            c.Enable();
        } 
    }

    public static void EnableWhite()
    {
        foreach (Continent c in ContiList){
            if(c.GetState() == Continent.ImgColor.White){
                c.Enable();
            }
        } 
    }
}
