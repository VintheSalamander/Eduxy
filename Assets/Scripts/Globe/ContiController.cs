using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ContiController
{
    static private List<Continent> ContiList = new List<Continent>();

    //To store all continents on the continent List
    public static void AddContinent(Continent newCont)
    {
        ContiList.Add(newCont);
    }

    //To Notify all other buttons that one has been pressed
    public static void ResetAllButtons()
    {
        foreach (Continent c in ContiList){
            c.ResetAll();
        } 
    }

    public static void ResetRedButtons()
    {
        foreach (Continent c in ContiList){
            c.ResetRed();
        } 
    }

    public static void DisableButtons(){
        foreach (Continent c in ContiList){
            c.Disable();
        } 
    }

    public static void EnableButtons(){
        foreach (Continent c in ContiList){
            c.Enable();
        } 
    }
}
