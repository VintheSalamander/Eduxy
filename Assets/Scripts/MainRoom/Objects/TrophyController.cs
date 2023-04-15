using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class TrophyController
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoad()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    static void OnActiveSceneChanged(Scene previousScene, Scene newScene){
        int previousSceneBuildIndex = UIController.GetPreviousSceneIndex();
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.buildIndex == 1 | currentScene.buildIndex == 0)
        {
            if(previousSceneBuildIndex == 2){
                Globe = ProgressBar.GetCurrentState();
            }else if(previousSceneBuildIndex == 3){
                HumanBody = ProgressBar.GetCurrentState();
            }else if(previousSceneBuildIndex == 4){
                SolarSystem = ProgressBar.GetCurrentState();
            }
        }
    }
    private static ProgressBar.ProgState Globe = ProgressBar.ProgState.Prog1;
    private static ProgressBar.ProgState HumanBody = ProgressBar.ProgState.Prog1;
    private static ProgressBar.ProgState SolarSystem = ProgressBar.ProgState.Prog1;

    public static ProgressBar.ProgState GetTrophyStatus(string checkT){
        if(checkT == "Globe"){
            return Globe;
        }else if(checkT == "HumanBody"){
            return HumanBody;
        }else if(checkT == "SolarSystem"){
            return SolarSystem;
        }
        return ProgressBar.ProgState.Prog1;
    }
}
