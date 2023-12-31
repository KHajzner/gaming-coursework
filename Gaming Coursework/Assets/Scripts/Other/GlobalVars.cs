using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVars
{
    //Difficulty of the game
    public static string difficulty { get; set; }

    //How much crew left in total game
    public static int crewScore { get; set; }

    //How much crew left on bandit level
    public static int crewOnBandit { get; set; }
    public static int currentResolution { get; set; }
}