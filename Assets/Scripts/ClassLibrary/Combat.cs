using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Combat
{
    public static void Fight(Creature a, Creature b)
    {
        Debug.Log(a.gameObject.name + " damaged " + b.gameObject.name + " by " + a.Strength + ". " + b.gameObject.name + "'s HP is now " + (b.HP - a.Strength));
    }
}
