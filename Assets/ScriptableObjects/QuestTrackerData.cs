using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class QuestTrackerData : ScriptableObject, IResetOnExitPlay {
    public int questTracker = 0;
    public int interactions = 0;
    public int [] totalInter = new int[2]{3,6};
    public string Antagonist = "Dajjal";

    public int minInteracts = 0;
    public int maxInteracts = 2;

    // public Dictionary<string, bool> inter = new Dictionary<string, bool>(){
    //     {"FKL", false},
    //     {"FKR", false},
    //     {"Impa", false},
    //     {"Aveil", false},
    //     {"Poe", false},
    //     {"Earl", false},
    //     {"Dajjal",false}
    // };

    public Dictionary<string, int> interact = new Dictionary<string, int>(){
        {"FKL", 0},
        {"FKR", 0},
        {"Impa", 0},
        {"Aveil", 0},
        {"Poe", 0},
        {"Earl", 0},
        {"Dajjal",0}
    };

    public void ResetOnExitPlay(){
        questTracker = 0;
        interactions = 0;
        // inter["FKL"] = false; 
        // inter["FKR"] = false; 
        // inter["Impa"] = false; 
        // inter["Aveil"] = false; 
        // inter["Poe"] = false; 
        // inter["Earl"] = false;
        // inter["Dajjal"] = false; 
        interact["FKL"] = 0; 
        interact["FKR"] = 0; 
        interact["Impa"] = 0; 
        interact["Aveil"] = 0; 
        interact["Poe"] = 0; 
        interact["Earl"] = 0;
        interact["Dajjal"] = 0; 
    }

    public int npcTalked(string name){
        int var = -1;
        if(interact.ContainsKey(name)){
            var = interact[name];
        }
        return var;
    }  

    // public bool dicExists(string name){
        
    //     if (inter.ContainsKey(name))
    //     {
    //         // var = inter[name]; 
    //         Debug.Log(name);
    //         return true;
            
    //     }
    //     return false;
    // } 

    // public bool dicValue(string name){
    //     bool talkedTo = false;
    //     bool var = inter.TryGetValue(name, out talkedTo);
    //     return talkedTo;
    // }
}
