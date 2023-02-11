using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class NPCStatic : MonoBehaviour
{
    public static Dictionary<int, Tuple<string, string, string, string>> NPCnames = new Dictionary<int, Tuple<string, string, string, string>>()
    {
        {1, new Tuple<string, string, string, string>("Earl Thomas", "Lazy", "Prideful", "Shameless") },
        {2, new Tuple<string, string, string, string>("Sir Alexandre", "Calculative", "Resourceful", "Greedy") },
        {3, new Tuple<string, string, string, string>("Sir Charles", "Pompous", "Strong-Willed", "Lackadaisical") },
        {4, new Tuple<string, string, string, string>("Sir Edgar", "Irritable", "Timid", "Lustful") },
        {5, new Tuple<string, string, string, string>("Sir Benjamin", "Honorable", "Attentive", "Resourceful") },
        {6, new Tuple<string, string, string, string>("Sir David", "Strong-Willed", "Open-Minded", "Lackadaisical") },
        {7, new Tuple<string, string, string, string>("Sir Ferrante", "Lazy", "Shameless", "Brash") },
        {8, new Tuple<string, string, string, string>("Ambassador Dajjal", "Resourceful", "Understanding", "Adaptive") },
        {9, new Tuple<string, string, string, string>("Honorable Cobra", "Intimidating", "Overprotective", "Pessimistic") },
        {10, new Tuple<string, string, string, string>("Honorable Neferiti", "Serious", "Rude", "Direct") },
        {11, new Tuple<string, string, string, string>("Lady Oliva Armstrong", "Calculative", "Attentive", "Charitable") },
        {12, new Tuple<string, string, string, string>("Lady Elanor", "Irritable", "Strong-Willed", "Attentive") },
        {13, new Tuple<string, string, string, string>("Lady Balthazar", "Pretentious", "Reserved", "Carefree") },
        {14, new Tuple<string, string, string, string>("Lord Alex Louis Armstrong", "Ruthless", "Greedy", "Loyal") },
        {15, new Tuple<string, string, string, string>("Lord Balthazar", "Reserved", "Dogmatic", "Prideful") },
        {16, new Tuple<string, string, string, string>("Lord Andre", "Lackadaisical", "Rude", "Prideful") }
    };


    public static string chooseCulprit()
	{
        return NPCnames[UnityEngine.Random.Range(0, NPCnames.Count)].Item1;
	}







}
