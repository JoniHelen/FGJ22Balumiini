using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Combat
{
    public static void Fight(Creature a, Creature b)
    {
        if (a.IsSpiritual) b.TakeDamage(a.SpiritualProwess - b.SpiritualDefense);
        else b.TakeDamage(a.Strength - b.PhysicalDefense);

        if (b.IsSpiritual) a.TakeDamage(b.SpiritualProwess - a.SpiritualDefense);
        else a.TakeDamage(b.Strength - a.PhysicalDefense);
    }
}
