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

		public Vector2 iconLocation;
		public Discoveries(string name, Vector2 iconLoc)
		{
			diaries = new List<GameObject>();
			interactables = new List<GameObject>();
			dialogues = new List<GameObject>();
			areaName = name;
			iconLocation = iconLoc;
		}
	}

	public static Dictionary<string, Discoveries> mapData = new Dictionary<string, Discoveries>()
	{
		{"Halls Left", new Discoveries("West Halls", new Vector2(-54.2f, 17.3f)) },
		{"Halls Right", new Discoveries("East Halls", new Vector2(103.1f, 6.9f)) },
		{"NPC Rooms", new Discoveries("Bedrooms", new Vector2(-24.1f, 88.1f)) },
		{"Throne Room v2", new Discoveries("Throne Room", new Vector2(27.8f, 77.6f)) },
		{"Barracks", new Discoveries("Barracks", new Vector2(69.2f, 82.8f)) },
		{"Garden", new Discoveries("Garden", new Vector2(-98.6f, 81.3f)) },
		{"Garden Maze", new Discoveries("Hedge Maze", new Vector2(118.3f, -65f)) },
		{"Dungeon Maze", new Discoveries("Dungeon", new Vector2(-88.3f, -67.1f)) },
		{"Halls Bottom", new Discoveries("South Halls", new Vector2(12.4f, -31.4f)) },
		{"Secret Room", new Discoveries("Secret Room", new Vector2(13f, -96f)) },
		{"ThroneBoss", new Discoveries("Throne Room", new Vector2(27.8f, 77.6f)) },
		{"Large Rooms", new Discoveries("Center Chamber", new Vector2(38.8f, 24.7f)) },
		{"Tutorial", new Discoveries("Outside Castle", new Vector2(13f, -96f)) }
	};



}
