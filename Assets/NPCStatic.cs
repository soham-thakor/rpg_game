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

    public static string getTrait(int npcKey, int trait)
    {
        NPC npc = NPCnames[npcKey];
        switch (trait) {
            case 1:
                return npc.trait1;
            case 2:
                return npc.trait2;
            case 3:
                return npc.trait3;
            default:
                return "Error trait";
        }
    }
    //keeps track of who we have already given out clues for so that we dont give duplicate clues
    public static List<int> trait1Clues = new List<int>();

    //Just to get the key of the person who is the culprit
    public static int culpritKey;
    public static string chooseCulprit()
	{
        culpritKey = UnityEngine.Random.Range(0, NPCnames.Count);
        return NPCnames[culpritKey].name;
	}

    
    //START: Generation of diaries that tell you "This NPC IS..."
	public static List<Tuple<string, string, string>> diaryTransitions = new List<Tuple<string, string, string>>() {
        {new Tuple<string, string, string>("\"I've come to realize that ", " is ", ".\"" ) },
        {new Tuple<string, string, string>("\"Turns out ", " is very ", ".\"") },
        {new Tuple<string, string, string>("\"", " is kind of ", " once you get to know him.\"") },
        {new Tuple<string, string, string>("\"I've found that ", " can be quite ", ".\"") },
        {new Tuple<string, string, string>("\"In my opinion ", " embodies the word ", ".\"") },
        {new Tuple<string, string, string>("\"Recently I've realized that ", " is so ", " sometimes.\"") }
    };
    public static List<string> ladyBalthazarDiary = generateDiary("Lady Balthazar");
    public static List<string> lordBalthazarDiary = generateDiary("Lord Balthazar");
    public static List<string> lordAndreDiary = generateDiary("Lord Andre");
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
    //This is just a function to call when using GenerateWorld()
    public static void generateDiaries()
	{
        trait1Clues.Clear();
        ladyBalthazarDiary = generateDiary("Lady Balthazar");
        lordBalthazarDiary = generateDiary("Lord Balthazar");
        lordAndreDiary = generateDiary("Lord Andre");
	}
    public static Dictionary<string, List<string>> diaryDict = new Dictionary<string, List<string>>()
    {
        {"Lady Balthazar", ladyBalthazarDiary },
        {"Lord Balthazar", lordBalthazarDiary },
        {"Lord Andre", lordAndreDiary }
	};
    
    //START: Generation of ghost clues of the structure "The culprit IS..."
    public static List<Tuple<string, string>> ghostClueTransitions = new List<Tuple<string, string>>() {
        new Tuple<string, string>("The one who did this to me was ", "..."),
        new Tuple<string, string>("I can't believe the person who did this was so ", "...")
    };

    public static string ghostClue1 = generateGhostClue(1);
    public static string ghostClue2 = generateGhostClue(2);
    public static string ghostClue3 = generateGhostClue(3);

    public static string generateGhostClue(int trait)
	{
        string addOn = "";
        int randomTransition = UnityEngine.Random.Range(0, ghostClueTransitions.Count);
        addOn += ghostClueTransitions[randomTransition].Item1;
        addOn += getTrait(culpritKey, trait);
        addOn += ghostClueTransitions[randomTransition].Item2;
        return addOn;
    }
    public static void generateGhostClues()
	{
        ghostClue1 = generateGhostClue(1);
        ghostClue2 = generateGhostClue(2);
        ghostClue3 = generateGhostClue(3);
	}

    //START: Generation of clues that tell the player "The culprit ISN'T..."

    //Keep track of what traits we have given clues out for
    public static List<string> antiCluesGiven = new List<string>();
    //List of traits that the culprit has
    public static List<string> culpritTraits = getCulpritTraits();
    public static List<string> getCulpritTraits()
	{
        List<string> traits = new List<string>();
        traits.Add(NPCnames[culpritKey].trait1);
        traits.Add(NPCnames[culpritKey].trait2);
        traits.Add(NPCnames[culpritKey].trait3);
        return traits;
    }
    //Start Generation
    public static List<Tuple<string, string>> antiClueTransitions = new List<Tuple<string, string>>()
    {
        new Tuple<string, string>("At least the person who did this to me wasn't ", "..."),
        new Tuple<string, string>("Knowing that the culprit of this disaster wasn't ", " puts me at peace...")
    };
    public static string antiClue1 = generateAntiClue();
    public static string antiClue2 = generateAntiClue();
    public static string antiClue3 = generateAntiClue();
    public static string generateAntiClue()
	{
        int randomNPC = UnityEngine.Random.Range(0, NPCnames.Count);
        string randomTrait = getTrait(randomNPC, UnityEngine.Random.Range(0, 4));

        while(antiCluesGiven.Contains(randomTrait) || culpritTraits.Contains(randomTrait))
		{//keep picking traits until we get one that we havent reveled, and that the culprit doesnt have
            randomNPC = UnityEngine.Random.Range(0, NPCnames.Count);
            randomTrait = getTrait(randomNPC, UnityEngine.Random.Range(0, 4));
        }

        string addOn = "";
        int randomTransition = UnityEngine.Random.Range(0, antiClueTransitions.Count);

        addOn += antiClueTransitions[randomTransition].Item1;
        addOn += randomTrait;
        addOn += antiClueTransitions[randomTransition].Item2;

        return addOn;
	}
    public static void generateAntiClues()
	{
        antiClue1 = generateAntiClue();
        antiClue2 = generateAntiClue();
        antiClue3 = generateAntiClue();
    }

    //Dictionary so that scripts can find what clues they should be using
    public static Dictionary<string, string> clues = new Dictionary<string, string>()
    {
        {"Alexandre", ghostClue1 },
        {"Edgar", ghostClue2 },
        {"Cobra", ghostClue3 },
        {"Balthazar", antiClue1 },
        {"Elanor", antiClue2 },
        {"Dajjal", antiClue3 }
    };







}
