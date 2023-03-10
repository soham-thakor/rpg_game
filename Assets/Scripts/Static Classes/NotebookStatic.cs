using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookStatic : MonoBehaviour
{
    public static List<string> playerNotes = emptyNotes(); 

    public static List<string> emptyNotes()
	{
        List<string> list = new List<string>();
        for(int i = 0; i < NPCStatic.NPCnames.Count + 1; ++i)
		{
            list.Add("");
		}

        return list;
	}
    public static NPCStatic.NPC culpritNPC = new NPCStatic.NPC( "Culprit", 
        NPCStatic.getTrait(NPCStatic.culpritKey, 1),
        NPCStatic.getTrait(NPCStatic.culpritKey, 2),
        NPCStatic.getTrait(NPCStatic.culpritKey, 3),
        NPCStatic.NPCnames[NPCStatic.culpritKey].gender );

    public static int currentPage = 0;
}
