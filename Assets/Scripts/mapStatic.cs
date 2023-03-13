using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapStatic : MonoBehaviour
{
	public class Discoveries {
		public int diaries;
		public int interactables;
		public int dialogues;

		public string areaName;

		public Discoveries(string name)
		{
			diaries = 0;
			interactables = 0;
			dialogues = 0;
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
		{"Large Rooms", new Discoveries("Center Chamber") }
	};



}
