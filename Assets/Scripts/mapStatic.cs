using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapStatic : MonoBehaviour
{
	public class Discoveries {
		
		public List<GameObject> diaries;
		public List<GameObject> interactables;
		public List<GameObject> dialogues;

		public string areaName;

		public Discoveries(string name)
		{
			diaries = new List<GameObject>();
			interactables = new List<GameObject>();
			dialogues = new List<GameObject>();
			areaName = name;
		}
	}

	public static Dictionary<string, Discoveries> mapData = new Dictionary<string, Discoveries>()
	{
		{"Halls Left", new Discoveries("West Halls") },
		{"Halls Right", new Discoveries("East Halls") },
		{"NPC Rooms", new Discoveries("Bedrooms") },
		{"Throne Room v2", new Discoveries("Throne Room") },
		{"Barracks", new Discoveries("Barracks") },
		{"Garden", new Discoveries("Garden") },
		{"Garden Maze", new Discoveries("Hedge Maze") },
		{"Dungeon Maze", new Discoveries("Deungeon") },
		{"Halls Bottom", new Discoveries("South Halls") },
		{"Secret Room", new Discoveries("Secret Room") },
		{"ThroneBoss", new Discoveries("Throne Room") },
		{"Large Rooms", new Discoveries("Center Chamber") }
	};



}
