using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class NPCStatic : MonoBehaviour
{

    public class NPC
    {
        public string name;
        public string trait1;
        public string trait2;
        public string trait3;
        public string gender;
        
        public NPC(string inputName, string t1, string t2, string t3, string g)
		{
            name = inputName;
            trait1 = t1;
            trait2 = t2;
            trait3 = t3;
            gender = g;
            
		}
    }
    
	public static Dictionary<int, NPC> NPCnames = new Dictionary<int, NPC>()
    {
        {0, new NPC("Earl Thomas", "Lazy", "Prideful", "Shameless", "Male") },
        {1, new NPC("Sir Alexandre", "Calculative", "Resourceful", "Greedy", "Male") },
        {2, new NPC("Sir Charles", "Pompous", "Strong-Willed", "Lackadaisical", "Male") },
        {3, new NPC("Sir Edgar", "Irritable", "Timid", "Lustful", "Male") },
        {4, new NPC("Sir Benjamin", "Honorable", "Attentive", "Resourceful", "Male") },
        {5, new NPC("Sir David", "Strong-Willed", "Open-Minded", "Lackadaisical", "Male") },
        {6, new NPC("Sir Ferrante", "Lazy", "Shameless", "Brash", "Male") },
        {7, new NPC("Ambassador Dajjal", "Resourceful", "Understanding", "Adaptive", "Male") },
        {8, new NPC("Honorable Cobra", "Intimidating", "Overprotective", "Pessimistic", "Male") },
        {9, new NPC("Honorable Neferiti", "Serious", "Rude", "Direct", "Male") },
        {10, new NPC("Lady Oliva Armstrong", "Calculative", "Attentive", "Charitable", "Female") },
        {11, new NPC("Lady Elanor", "Irritable", "Strong-Willed", "Attentive", "Female") },
        {12, new NPC("Lady Balthazar", "Pretentious", "Reserved", "Carefree", "Female") },
        {13, new NPC("Lord Alex Louis Armstrong", "Ruthless", "Greedy", "Loyal", "Male") },
        {14, new NPC("Lord Balthazar", "Reserved", "Dogmatic", "Prideful", "Male") },
        {15, new NPC("Lord Andre", "Lackadaisical", "Rude", "Prideful", "Male") }
    };

    //keeps track of who we have already given out clues for so that we dont give duplicate clues
    public static List<int> trait1Clues = new List<int>();
    public static string chooseCulprit()
	{
        return NPCnames[UnityEngine.Random.Range(0, NPCnames.Count)].name;
	}

    

	public static List<Tuple<string, string, string>> diaryTransitions = new List<Tuple<string, string, string>>() {
        {new Tuple<string, string, string>("\"I' come to realize that ", " is ", ".\"" ) },
        {new Tuple<string, string, string>("\"Turns out ", " is very ", ".\"") },
        {new Tuple<string, string, string>("\"", " is kind of ", " once you get to know him.\"") },
        {new Tuple<string, string, string>("\"I've found that ", " can be quite ", ".\"") },
        {new Tuple<string, string, string>("\"In my opinion ", " embodies the word ", ".\"") },
        {new Tuple<string, string, string>("\"Recently I've realized that ", " is so ", " sometimes.\"") }
    };
    public static List<string> diary = generateDiary("Lady Balthazar");
    public static List<string> generateDiary(string characterName)
	{
        List<string> diary = new List<string>();
        string addOn = "";
        int randomPerson = UnityEngine.Random.Range(0, NPCnames.Count);
        for(int i = 0; i < 3; i++)
		{//Get 3 clues
            while(trait1Clues.Contains(randomPerson) || NPCnames[randomPerson].name == characterName) 
			{//get a character that isnt the person writing the diary and doesnt have a diary clue for them yet
                randomPerson = UnityEngine.Random.Range(0, NPCnames.Count);
			}
            //make a sentence for that character, add it to the diary and add that character to the list of characters that have clues already
            int randomTransitions = UnityEngine.Random.Range(0, diaryTransitions.Count);
            addOn += diaryTransitions[randomTransitions].Item1;
            addOn += NPCnames[randomPerson].name;
            addOn += diaryTransitions[randomTransitions].Item2;
            addOn += NPCnames[randomPerson].trait1;
            addOn += diaryTransitions[randomTransitions].Item3;
            trait1Clues.Add(randomPerson);
            diary.Add(addOn);
            addOn = "";
        }
        return diary;
	}

    public static Dictionary<string, List<string>> diaryDict = new Dictionary<string, List<string>>()
    {
        {"Lady Balthazar", diary }
    };







}
