using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class TrophyController
{
    private static bool Globe = false;
    private static bool HumanBody = false;
    private static bool SolarSystem = false;

    public static void ShowTrophy(string name){
        if(name == "Globe"){
            Globe = true;
        }else if(name == "HumanBody"){
            HumanBody = true;
        }else if(name == "SolarSystem"){
            SolarSystem = true;
        }
    }

    public static bool GetTrophyStatus(string checkT){
        if(checkT == "Globe"){
            return Globe;
        }else if(checkT == "HumanBody"){
            return HumanBody;
        }else if(checkT == "SolarSystem"){
            return SolarSystem;
        }
        return false;
    }
}
