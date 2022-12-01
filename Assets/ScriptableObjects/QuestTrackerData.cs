using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class QuestTrackerData : ScriptableObject, IResetOnExitPlay {
    public int questTracker = 0;
    public int interactions = 0;
    //public int [] totalInter = new int[2]{3,6};
    public string Antagonist = "Dajjal";

    public int minInteracts = 0;
    public int maxInteracts = 2;

    public Dictionary<string, int> interact = new Dictionary<string, int>(){
        {"Alexandre", 0},
        {"Edgar", 0},
        {"Cobra", 0},
        {"Balthazar", 0},
        {"Olivia", 0},
        {"Elanor", 0},
        {"Dajjal",0}
    };

    public void ResetOnExitPlay(){
        questTracker = 0;
        interactions = 0;

        interact["Alexandre"] = 0; 
        interact["Edgar"] = 0; 
        interact["Cobra"] = 0; 
        interact["Balthazar"] = 0; 
        interact["Olivia"] = 0; 
        interact["Elanor"] = 0;
        interact["Dajjal"] = 0; 
    }

    public int npcTalked(string name){
        int var = -1;
        if(interact.ContainsKey(name)){
            var = interact[name];
        }
        return var;
    }  

    public void nextSet(){
        if(interactions == 5){
            questTracker++;
        }
    }


}
