using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStatic : MonoBehaviour
{
    public static Dictionary<int, string> NPCnames = new Dictionary<int, string>()
    {
        {1, "Earl Thomas" },
        {2, "Ambassador Dajjal" },
        {3, "Honorable Cobra" },
        {4, "Honorable Neferiti" },
        {5, "Sir Alexandre" },
        {6, "Sir Charles" },
        {7, "Sir Edgar" },
        {8, "Sir Benjamin" },
        {9, "Sir David" },
        {10, "Sir Ferrante" },
        {11, "Lady Oliva Armstrong" },
        {12, "Lady Elanor" },
        {13, "Lady Balthazar" },
        {14, "Lord Alex Louis Armstrong" },
        {15, "Lord Balthazar" },
        {16, "Lord Andre" },
    };


    public static string chooseCulprit()
	{
        return NPCnames[Random.Range(0, NPCnames.Count)];
	}







}
